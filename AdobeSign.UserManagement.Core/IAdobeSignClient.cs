using System;
using System.Collections.Generic;
using System.Text;
using AdobeSign.UserManagement.Core.Interfaces;

namespace AdobeSign.UserManagement.Core
{
    interface IAdobeSignClient
    {
        /// <summary>
        /// Access Adobe's User Client API
        /// </summary>
        IUserClient User { get; }
    }
}
