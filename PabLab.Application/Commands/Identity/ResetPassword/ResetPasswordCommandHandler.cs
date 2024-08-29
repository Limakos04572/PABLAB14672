using MediatR;
using Microsoft.AspNetCore.Identity;
using PabLab.Application.Identity;
using PabLab.Application.Identity.Exceptions;

namespace PabLab.Application.Commands.Identity.ResetPassword;

internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new UserNotExistsException(request.Email);

        await _userManager.ResetPasswordAsync(user, request.Token, request.Password);   
    }
}
