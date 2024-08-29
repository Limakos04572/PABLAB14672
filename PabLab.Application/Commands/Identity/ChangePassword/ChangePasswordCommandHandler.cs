using MediatR;
using Microsoft.AspNetCore.Identity;
using PabLab.Application.Identity;
using PabLab.Application.Identity.Exceptions;

namespace ProductsApp.Application.Commands.Identity.ChangePassword;

internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
            throw new UserNotExistsException(request.Username);

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            throw new ChangePasswordFaildException(result.Errors);
        }
    }
}
