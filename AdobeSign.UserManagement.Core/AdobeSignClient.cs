using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AdobeSign.UserManagement.Core.Clients;
using AdobeSign.UserManagement.Core.Exceptions;
using AdobeSign.UserManagement.Core.Interfaces;
using AdobeSign.UserManagement.Core.ResourceModels;
using RestSharp;

namespace AdobeSign.UserManagement.Core
{
    public class AdobeSignClient : IAdobeSignClient
    {
        private readonly string _adobeIntegrationKey;
        
        public AdobeSignClient(string adobeIntegrationKey)
        {
            _adobeIntegrationKey = adobeIntegrationKey;
            RestClient client = new RestClient($"{_GetAdobeBaseUri()}api/rest/v6/");
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", _adobeIntegrationKey));

            User = new UserClient(client);
            Group = new GroupClient(client);
        }

        private string _GetAdobeBaseUri()
        {
            RestClient client = new RestClient("https://api.echosign.com/api/rest/v6/");
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", _adobeIntegrationKey));
            var request = new RestRequest($"baseUris");
            var response = client.Execute<BaseUrlResourceModel>(request);
            if (response.IsSuccessful)
            {
                return response.Data.apiAccessPoint;
            }
            throw new AdobeSignFailedToFetchException("Failed to fetch adobe base URIs");
        }

        public IUserClient User { get; }
        public IGroupClient Group { get; }
    }
}
