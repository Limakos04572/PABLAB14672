namespace PabLab.Application.Dtos;

public class LoginDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }

}