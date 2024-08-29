using System.Net;
using PabLab.Domain.Exceptions.Abstractions;

namespace PabLab.Application.Identity.Exceptions;

public class UserAlreadyExistsByEmailException : BaseException
{
    public UserAlreadyExistsByEmailException(string email) 
        : base($"User with email {email} already exists.")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}