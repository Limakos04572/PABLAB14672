using System.Net;
using Microsoft.AspNetCore.Identity;
using PabLab.Domain.Exceptions.Abstractions;

namespace PabLab.Application.Identity.Exceptions;

public class UserCreationFaildException : BaseException
{
    public UserCreationFaildException(IEnumerable<IdentityError> errors)
        : base($"User creation faild: {string.Join(",", errors.Select(x => x.Description))}")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}