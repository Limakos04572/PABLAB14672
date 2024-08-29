using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PabLab.Application.Commands.Identity.Register;
using PabLab.Application.Commands.Identity.ResetPassword;
using ProductsApp.Application.Commands.Identity.ChangePassword;
using ProductsApp.Application.Commands.Identity.Login;
using Swashbuckle.AspNetCore.Annotations;

namespace PabLab.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    [SwaggerOperation("Login user")]
    public async Task<ActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return result != null ? Ok(result) : Unauthorized();
    }

    [HttpPost("[action]")]
    [SwaggerOperation("Register user")]
    public async Task<ActionResult> Register(RegisterCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [SwaggerOperation("Change password")]
    public async Task<ActionResult> ChangePassword(ChangePasswordCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpPost("[action]")]
    [SwaggerOperation("Reset password")]
    public async Task<ActionResult> ResetPassword(ResetPasswordCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}