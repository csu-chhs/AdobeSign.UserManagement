using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<UserDetailResourceModel>> GetAllAdobeUsersFullListAsync()
        {
            return await _WalkPrimaryUserList();
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
        /// Fetches and returns a list in memory of
        /// all account users.
        ///
        /// In our testing attempting to fetch large numbers of records at a time
        /// had a very limited impact on response times from the Adobe API.  For example
        /// requesting 100 users took ~ 27 seconds, and requesting 2000 users took ~ 31 seconds.
        ///
        /// Therefore, it is likely more efficient *for us* to do smaller numbers of large
        /// page sizes rather than more frequent small dumps.  In the future, Adobe
        /// may limit that pageSize which would require us to revisit this.
        /// </summary>
        /// <returns></returns>
        private async Task<List<UserDetailResourceModel>> _WalkPrimaryUserList()
        {
            List<UserDetailResourceModel> userList = new List<UserDetailResourceModel>();
            bool paging = true;
            string cursor = "";
            int pageSize = 20000;
            while (paging)
            {
                var fetchUsers = await GetAllAdobeUsersAsync(cursor, pageSize);
                userList = userList.Union(fetchUsers.userInfoList).ToList();

                if (!string.IsNullOrEmpty(fetchUsers.page.nextCursor))
                {
                    cursor = fetchUsers.page.nextCursor;
                }
                else
                {
                    // When next cursor value is empty, we are on the last page.
                    paging = false;
                }
            }

            return userList;
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
            var users = await _WalkPrimaryUserList();

            var user = users.FirstOrDefault(s => s.email == email);

            if (user == null)
            {
                throw new AdobeSignFailedToFetchException($"Failed to find user with email {email}.");
            }

            return user;
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
