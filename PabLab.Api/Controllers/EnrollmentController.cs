using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PabLab.Application.Commands.Enrollment.AddEnrollment;
using PabLab.Application.Commands.Enrollment.RemoveEnrollment;
using PabLab.Application.Commands.Enrollment.UpdateEnrollment;
using PabLab.Application.Identity.Classes;
using PabLab.Application.Queries.Enrollment.GetEnrollmentById;
using PabLab.Application.Queries.Enrollment.GetEnrollments;
using Swashbuckle.AspNetCore.Annotations;

namespace PabLab.WebAPI.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class EnrollmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnrollmentController(IMediator mediator)
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
    [SwaggerOperation("Get enrollment by ID")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetEnrollmentByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet]
    [SwaggerOperation("Get all enrollments")]
    public async Task<ActionResult> Get()
    {
        var result = await _mediator.Send(new GetEnrollmentsQuery());
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation("Add new enrollment")]
    public async Task<ActionResult> Post([FromBody] AddEnrollmentCommand command)
    {
        await _mediator.Send(command);
        return Created(string.Empty, null);
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
    [SwaggerOperation("Delete enrollment")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new RemoveEnrollmentCommand(id));
        return NoContent();
    }

    [HttpPut]
    [SwaggerOperation("Update enrollment")]
    public async Task<ActionResult> Put([FromBody] UpdateEnrollmentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}