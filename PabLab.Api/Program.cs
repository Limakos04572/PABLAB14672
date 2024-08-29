using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PabLab.Application.Behaviors;
using PabLab.Application.Commands.Course.AddCourse;
using PabLab.Application.Commands.Course.UpdateCourse;
using PabLab.Application.Commands.Enrollment.AddEnrollment;
using PabLab.Application.Commands.Enrollment.UpdateEnrollment;
using PabLab.Application.Commands.Identity.ResetPassword;
using PabLab.Application.Commands.Student.AddStudent;
using PabLab.Application.Commands.Student.UpdateStudent;
using PabLab.Application.Identity;
using PabLab.Application.Identity.Services;
using PabLab.Application.Mappings;
using PabLab.Application.Middlewares;
using PabLab.Domain.Abstractions;
using PabLab.Infrastructure;
using PabLab.Infrastructure.Context;
using PabLab.Infrastructure.Repositories;
using PabLab.WebAPI.gRPCModule;
using ProductsApp.Application.Commands.Identity.ChangePassword;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastructure
builder.Services.AddDbContext<PabLabContext>(options => options.UseInMemoryDatabase("SchoolDB"));
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Application
// Add Mediator
var applicationAssembly = AppDomain.CurrentDomain.GetAssemblies().Single(assembly => assembly.GetName().Name == "PabLab.Application");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));

// Add AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<CourseProfile>();
    cfg.AddProfile<StudentProfile>();
    cfg.AddProfile<EnrollmentProfile>();
}, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IValidator<AddCourseCommand>, AddCourseCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateCourseCommand>, UpdateCourseCommandValidator>();

builder.Services.AddScoped<IValidator<AddStudentCommand>, AddStudentCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateStudentCommand>, UpdateStudentCommandValidator>();

builder.Services.AddScoped<IValidator<AddEnrollmentCommand>, AddEnrollmentCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateEnrollmentCommand>, UpdateEnrollmentCommandValidator>();

builder.Services.AddScoped<IValidator<ResetPasswordCommand>, ResetPasswordCommandValidator>();
builder.Services.AddScoped<IValidator<ChangePasswordCommand>, ChangePasswordCommandValidator>();


builder.Services.AddGrpc();
builder.Services.AddSingleton<CounterClass>();

// Add Identity
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<PabLabContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opts =>
    opts.TokenLifespan = TimeSpan.FromHours(10));

builder.Services.AddTransient<UserService>(); 


// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger => 
{
    swagger.EnableAnnotations();

    // add JWT Authentication
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    swagger.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement 
    {
        { securityScheme, new string[] { } }
    });
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CounterMiddleware>();

app.MapGrpcService<StatisticsService>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
