namespace PabLab.Application.Identity.Classes;

public static class UserRoles
{
    public const string User = "USER";
    public const string Admin = "ADMIN";
    public const string UserOrAdmin = $"{User},{Admin}";
}