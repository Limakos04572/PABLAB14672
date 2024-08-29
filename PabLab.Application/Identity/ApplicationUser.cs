

using Microsoft.AspNetCore.Identity;

namespace PabLab.Application.Identity;

public class ApplicationUser : IdentityUser
{
    public int Age { get; set; }
}