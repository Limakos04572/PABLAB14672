using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PabLab.Application.Dtos;
using PabLab.Application.Identity;
using ProductsApp.Application.Commands.Identity.Login;

namespace PabLab.Application.Commands.Identity.Login;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
{

    private readonly UserManager<ApplicationUser> _userManger;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(UserManager<ApplicationUser> userManger, IConfiguration configuration)
    {
        _userManger = userManger;
        _configuration = configuration;
    }

    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManger.FindByNameAsync(request.Username);
        if (user == null) return null;

        bool passwordIsCorrect = await _userManger.CheckPasswordAsync(user, request.Password);
        if (!passwordIsCorrect) return null;

        var validTo = DateTime.Now.AddHours(2);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(validTo).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var roles = await _userManger.GetRolesAsync(user);
        foreach (var role in roles)
            authClaims.Add(new Claim(ClaimTypes.Role, role));

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            expires: validTo,
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return new LoginDto()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        };
    }
}
