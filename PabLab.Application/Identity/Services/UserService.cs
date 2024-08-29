using Microsoft.AspNetCore.Http;

namespace PabLab.Application.Identity.Services;

public class UserService
{
    private readonly IHttpContextAccessor _context;

    public UserService(IHttpContextAccessor context)
    {
        _context = context;
    }

    public string GetUserName()
    {
        return _context.HttpContext.User.Identity.Name;
    }
}