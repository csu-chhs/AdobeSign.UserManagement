using System;
using System.Collections.Generic;
using System.Text;

namespace AdobeSign.UserManagement.Core.Exceptions
{
    public class AdobeSignFailedToFetchException : Exception
    {
        public AdobeSignFailedToFetchException() : base()
        {

        }

        public AdobeSignFailedToFetchException(string message) : base(message)
        {

        }

        public AdobeSignFailedToFetchException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
