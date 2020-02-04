using System;

namespace AlmOps.AzureDevOpsComponent.Domain.Exceptions
{
    public class ConnectivityException : Exception
    {
        public ConnectivityException()
        {
        }

        public ConnectivityException(string message) : base(message)
        {
        }

        public ConnectivityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
