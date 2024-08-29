using System.Net;
using PabLab.Domain.Exceptions.Abstractions;

namespace PabLab.Domain.Exceptions;

public class AlreadyExistsException : BaseException
{
    public AlreadyExistsException(string name) : base($"Product with name {name} is already exists.")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}