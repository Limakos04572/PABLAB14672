using System.Net;
using PabLab.Domain.Exceptions.Abstractions;

namespace PabLab.Application.Identity.Exceptions;

public class UserNotExistsException : BaseException
{
    public UserNotExistsException(string email)
        : base($"User with email {email} doesnt exists")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}