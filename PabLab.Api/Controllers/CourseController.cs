using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PabLab.Application.Commands.Course.AddCourse;
using PabLab.Application.Commands.Course.RemoveCourse;
using PabLab.Application.Commands.Course.UpdateCourse;
using PabLab.Application.Identity.Classes;
using PabLab.Application.Queries.Course.GetCourseById;
using PabLab.Application.Queries.Course.GetCourseByName;
using PabLab.Application.Queries.Course.GetCourses;
using Swashbuckle.AspNetCore.Annotations;

namespace PabLab.WebAPI.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
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

    [HttpGet("[action]/{name}")]
    [SwaggerOperation("Get course by title")]
    public async Task<ActionResult> GetByName([FromRoute] string title)
    {
        var result = await _mediator.Send(new GetCourseByNameQuery(title));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet]
    [SwaggerOperation("Get all courses")]
    public async Task<ActionResult> Get()
    {
        var result = await _mediator.Send(new GetCoursesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get course by ID")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetCourseByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [SwaggerOperation("Add new course")]
    public async Task<ActionResult> Post([FromBody] AddCourseCommand command)
    {
        await _mediator.Send(command);
        return Created(string.Empty, null);
    }

    [HttpPut]
    [SwaggerOperation("Update course")]
    public async Task<ActionResult> Put([FromBody] UpdateCourseCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
    [SwaggerOperation("Delete course")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new RemoveCourseCommand(id));
        return NoContent();
    }
}