using MediatR;
using Microsoft.AspNetCore.Identity;
using PabLab.Application.Identity;
using PabLab.Application.Identity.Exceptions;

namespace PabLab.Application.Commands.Identity.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly UserManager<ApplicationUser> _userManger;
    private readonly RoleManager<IdentityRole> _roleManger;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManger, RoleManager<IdentityRole> roleManger)
    {
        _userManger = userManger;
        _roleManger = roleManger;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userByNameExists = await _userManger.FindByNameAsync(request.Username);
        if (userByNameExists != null)
            throw new UserAlreadyExistsByNameException(request.Username);

        var userByEmailExists = await _userManger.FindByEmailAsync(request.Email);
        if (userByEmailExists != null)
            throw new UserAlreadyExistsByEmailException(request.Email);

        ApplicationUser user = new ApplicationUser()
        {
            UserName = request.Username,
            Email = request.Email,
            Age = request.Age,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManger.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new UserCreationFaildException(result.Errors);
        }

        var role = request.Role.ToUpper();
        var roleExists = await _roleManger.RoleExistsAsync(role);
        if (!roleExists)
            await _roleManger.CreateAsync(new IdentityRole(role));
        
        await _userManger.AddToRoleAsync(user, role);
    }
}
