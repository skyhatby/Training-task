using System;

namespace WLN.Test.Project.Model.Exceptions
{
    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException(string message)
            : base(message)
        {
        }

        public RepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
