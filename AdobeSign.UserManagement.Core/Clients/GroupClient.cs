using System.Linq;
using System.Threading.Tasks;
using AdobeSign.UserManagement.Core.Exceptions;
using AdobeSign.UserManagement.Core.Interfaces;
using AdobeSign.UserManagement.Core.ResourceModels.Groups;
using AdobeSign.UserManagement.Core.ResourceModels.Users;
using RestSharp;

namespace AdobeSign.UserManagement.Core.Clients
{
    public class GroupClient : IGroupClient
    {
        private readonly RestClient _client;

        public GroupClient(RestClient client)
        {
            _client = client;
        }

        public async Task<GroupListResourceModel> GetGroupsAsync()
        {
            var request = new RestRequest($"groups");
            var response = await _client.ExecuteAsync<GroupListResourceModel>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            throw new AdobeSignFailedToFetchException("Failed to fetch Adobe Sign Groups.");
        }

        public async Task<GroupListResourceModel> GetGroupsAsync(string cursor, int? pageSize)
        {
            var request = new RestRequest($"groups");

            if (pageSize != null)
            {
                request.AddParameter("pageSize", pageSize);
            }

            if (!string.IsNullOrEmpty(cursor))
            {
                request.AddParameter("cursor", cursor);
            }

            var response = await _client.ExecuteAsync<GroupListResourceModel>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            throw new AdobeSignFailedToFetchException("Failed to fetch Adobe Sign Groups.");
        }

        public async Task<GroupSingleResourceModel> GetGroupAsync(string id)
        {
            var request = new RestRequest($"groups/{id}");
            var response = await _client.ExecuteAsync<GroupSingleResourceModel>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            throw new AdobeSignFailedToFetchException($"Failed to fetch group {id}");
        }

        public async Task<GroupDetailResourceModel> GetGroupByNameAsync(string groupName)
        {
            bool found = false;
            string cursor = "";
            int timeout = 50; // Tries
            int counter = 0;
            while (!found && counter < timeout)
            {
                var groupsList = await GetGroupsAsync(cursor, 1000);
                GroupDetailResourceModel group = groupsList.groupInfoList.FirstOrDefault(s => s.groupName == groupName);

                if (group != null)
                {
                    return group;
                }

                cursor = groupsList.page.nextCursor;
                counter++;
            }
            throw new AdobeSignFailedToFetchException($"Could not find group with name {groupName}.  Attempted {counter} times.");
        }

        public async Task<UserListResourceModel> GetUsersInGroupAsync(string id)
        {
            var request = new RestRequest($"groups/{id}/users");
            request.AddParameter("pageSize", 50000);
            var response = await _client.ExecuteAsync<UserListResourceModel>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            throw new AdobeSignFailedToFetchException($"Could not get users for group {id}");
        }
    }
}
