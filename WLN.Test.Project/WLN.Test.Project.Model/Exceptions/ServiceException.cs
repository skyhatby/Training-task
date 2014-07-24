using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Model.Exceptions
{
    public class ServiceException : Exception
    {
        protected ServiceException()
        {
        }

        public ServiceException(string message)
            : base(message)
        {
        }

        public ServiceException(Exception innerException)
            : base("See inner exception.", innerException)
        {
        }

        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
