using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PabLab.Application.Commands.Student.AddStudent;
using PabLab.Application.Commands.Student.RemoveStudent;
using PabLab.Application.Commands.Student.UpdateStudent;
using PabLab.Application.Identity.Classes;
using PabLab.Application.Queries.Student.GetStudentById;
using PabLab.Application.Queries.Student.GetStudentByName;
using PabLab.Application.Queries.Student.GetStudents;
using Swashbuckle.AspNetCore.Annotations;

namespace PabLab.WebAPI.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [SwaggerOperation("Test")]
    public async Task<ActionResult> Test()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name = User.FindFirstValue(ClaimTypes.Name);

        return Ok("Test");
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get student by ID")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetStudentByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }
    
    [HttpGet("[action]/{name}")]
    [SwaggerOperation("Get student by name and lastname")]
    public async Task<ActionResult> GetByName([FromRoute] string firstName, string lastName)
    {
        var result = await _mediator.Send(new GetStudentByNameQuery(FirstName: firstName, LastName: lastName));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet]
    [SwaggerOperation("Get all students")]
    public async Task<ActionResult> Get()
    {
        var result = await _mediator.Send(new GetStudentsQuery());
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation("Add student")]
    public async Task<ActionResult> Post([FromBody] AddStudentCommand command)
    {
        await _mediator.Send(command);
        return Created(string.Empty, null);
    }
    
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = UserRoles.Admin)]
    [SwaggerOperation("Delete student")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new RemoveStudentCommand(id));
        return NoContent();
    }

    [HttpPut]
    [SwaggerOperation("Update student")]
    public async Task<ActionResult> Put([FromBody] UpdateStudentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}