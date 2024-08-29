using MediatR;

namespace ProductsApp.Application.Commands.Identity.ChangePassword;

public record ChangePasswordCommand(string Username, string CurrentPassword, string NewPassword) : IRequest;
