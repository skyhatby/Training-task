using System;

namespace WLN.Test.Project.Model.Exceptions
{
    [Serializable]
    public class ConcurrencyException : RepositoryException
    {
        public string EntityName { get; private set; }
        public object Identifier { get; private set; }

        public ConcurrencyException(string message, string entityName, object identifier)
            : base(message)
        {
            EntityName = entityName;
            Identifier = identifier;
        }
    }
}
