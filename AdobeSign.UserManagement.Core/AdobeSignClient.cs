using System;
using System.Collections.Generic;
using System.Text;
using AdobeSign.UserManagement.Core.Clients;
using AdobeSign.UserManagement.Core.Interfaces;
using RestSharp;

namespace AdobeSign.UserManagement.Core
{
    class AdobeSignClient : IAdobeSignClient
    {
        public AdobeSignClient()
        {
            RestClient client = new RestClient();
            User = new UserClient();
        }

        public IUserClient User { get; }
    }
}
