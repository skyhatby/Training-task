using System;

namespace WLN.Test.Project.Model.Exceptions
{
    [Serializable]
    public class ConnectionException : RepositoryException
    {
        public ConnectionException(string message)
            : base(message)
        {
        }

        public ConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
