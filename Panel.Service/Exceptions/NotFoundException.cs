using Microsoft.Extensions.Logging;

namespace Panel.Service.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        Message = message;
    }

    public string Message { get; }
}