using System.Net;
using Microsoft.AspNetCore.Identity;
using PabLab.Domain.Exceptions.Abstractions;

namespace PabLab.Application.Identity.Exceptions;

public class ChangePasswordFaildException : BaseException
{
    public ChangePasswordFaildException(IEnumerable<IdentityError> errors)
        : base($"Change password faild: {string.Join(",", errors.Select(x => x.Description))}")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}