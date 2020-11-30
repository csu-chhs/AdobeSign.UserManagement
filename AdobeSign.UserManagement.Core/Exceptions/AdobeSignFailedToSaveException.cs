using System;
using System.Collections.Generic;
using System.Text;

namespace AdobeSign.UserManagement.Core.Exceptions
{
    public class AdobeSignFailedToSaveException : Exception
    {
        public AdobeSignFailedToSaveException() : base()
        {

        }

        public AdobeSignFailedToSaveException(string message) : base(message)
        {

        }

        public AdobeSignFailedToSaveException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
