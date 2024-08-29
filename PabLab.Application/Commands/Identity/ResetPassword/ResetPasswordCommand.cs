using MediatR;

namespace PabLab.Application.Commands.Identity.ResetPassword;

public record ResetPasswordCommand(string Token, string Email, string Password, string ConfirmPassword) : IRequest;
