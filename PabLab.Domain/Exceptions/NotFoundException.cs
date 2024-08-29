using System.Net;
using PabLab.Domain.Exceptions.Abstractions;

namespace PabLab.Domain.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(int id) : base($"Product with ID {id} was not found.")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}