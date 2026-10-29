using System;

namespace AlmOps.Common.Infrastructure.RestApi;

/// <summary>
/// Raised when a REST API call fails or returns content that cannot be read.
/// </summary>
public class ConnectivityException : Exception
{
    public ConnectivityException(string message)
        : base(message)
    {
    }

    public ConnectivityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
