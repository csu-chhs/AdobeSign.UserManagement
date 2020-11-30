using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdobeSign.UserManagement.Core.Exceptions;
using AdobeSign.UserManagement.Core.Interfaces;
using AdobeSign.UserManagement.Core.ResourceModels.Users;
using RestSharp;

namespace AdobeSign.UserManagement.Core.Clients
{
    public class UserClient : IUserClient
    {
        private readonly RestClient _client;

        public UserClient(RestClient client)
        {
            _client = client;
        }

        public async Task<UserListResourceModel> GetAllAdobeUsersAsync()
        {
            var request = new RestRequest($"users");

            var response = await _client.ExecuteAsync<UserListResourceModel>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            throw new AdobeSignFailedToFetchException("Failed to fetch all Adobe users", response.ErrorException);
        }

        public async Task<UserListResourceModel> GetAllAdobeUsersAsync(string cursor, int? pageSize)
        {
            var request = new RestRequest($"users");

            if (pageSize != null)
            {
                request.AddParameter("pageSize", pageSize);
            }

            if (!string.IsNullOrEmpty(cursor))
            {
                request.AddParameter("cursor", cursor);
            }

            var response = await _client.ExecuteAsync<UserListResourceModel>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            throw new AdobeSignFailedToFetchException("Failed to fetch all Adobe users", response.ErrorException);
        }

        public async Task<UserDetailResourceModel> GetAdobeUserAsync(string id)
        {
            var request = new RestRequest($"users/{id}");
            var response = await _client.ExecuteAsync<UserDetailResourceModel>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new AdobeSignFailedToFetchException($"Failed to fetch adobe user {id}");
        }


        public async Task<UsersGroupsResourceModel> GetAdobeUsersGroupsAsync(string id)
        {
            var request = new RestRequest($"users/{id}/groups");
            var response = await _client.ExecuteAsync<UsersGroupsResourceModel>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            
            throw new AdobeSignFailedToFetchException($"Failed to fetch groups for the Adobe sign user with id {id}", response.ErrorException);
        }

        /// <summary>
        /// There is no automatic way to search for a user through the
        /// Adobe API so we need to do it manually by retrieving pages of
        /// results and then searching through that list to find the user.
        ///
        /// This process will likely take a decent amount of time to accomplish depending on how
        /// many records we can obtain at a time.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserDetailResourceModel> GetAdobeUserViaEmailAsync(string email)
        {
            bool found = false;
            string cursor = "";
            int timeout = 50; // 40 Tries.
            int counter = 0;
            while (!found && counter < timeout)
            {
                // Start with 100 at a time
                var userList = await GetAllAdobeUsersAsync(cursor, 1000);
                UserDetailResourceModel user = userList.userInfoList.FirstOrDefault(s => s.email == email);

                if (user != null)
                {
                    return user;
                }

                cursor = userList.page.nextCursor;
                counter++;
            }

            throw new AdobeSignFailedToFetchException($"Could not find user with email -> {email}.  Attempted {counter} times.");
        }

        public async Task UpdateUserGroupsAsync(string id, UsersGroupsResourceModel userGroups)
        {
            var request = new RestRequest($"users/{id}/groups", Method.PUT);
            request.AddJsonBody(userGroups);
            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new AdobeSignFailedToSaveException($"Failed to save groups for user {id}");
            }
        }
    }
}
