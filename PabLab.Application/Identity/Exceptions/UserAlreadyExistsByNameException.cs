using System.Net;
using PabLab.Domain.Exceptions.Abstractions;

namespace PabLab.Application.Identity.Exceptions;

public class UserAlreadyExistsByNameException : BaseException
{
    public UserAlreadyExistsByNameException(string name) 
        : base($"User with name {name} already exists.")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}