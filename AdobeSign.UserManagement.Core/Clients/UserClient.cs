using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdobeSign.UserManagement.Core.Interfaces;
using AdobeSign.UserManagement.Core.ResourceModels;
using RestSharp;

namespace AdobeSign.UserManagement.Core.Clients
{
    class UserClient : AdobeSignBaseClient, IUserClient
    {
        private readonly RestClient _client;

        public UserClient()
        {
            _client = new RestClient(GetAdobeBaseUrl());
        }

        public async Task<UserInfoResourceModel> GetAllAdobeUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDetailResourceModel> GetAdobeUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UsersGroupsResourceModel> GetAdobeUsersGroupsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
