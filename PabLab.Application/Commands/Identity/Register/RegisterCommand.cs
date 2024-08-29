using MediatR;

namespace PabLab.Application.Commands.Identity.Register;

public class RegisterCommand : IRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public string Role { get; set; }
}
