﻿using System.Collections.Generic;
using System.Collections.Specialized;
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
                request.AddParameter("pageSize", (int)pageSize);
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

        public async Task<List<UserDetailResourceModel>> GetUsersInGroupAsync(string id)
        {
            List<UserDetailResourceModel> userList = new List<UserDetailResourceModel>();
            bool paging = true;
            string cursor = "";
            int pageSize = 10000;
            while (paging)
            {
                var fetchUsers = await _GetUsersInGroupAsync(id, cursor, pageSize);
                userList = userList.Union(fetchUsers.userInfoList).ToList();
                if (!string.IsNullOrEmpty(fetchUsers.page.nextCursor))
                {
                    cursor = fetchUsers.page.nextCursor;
                }
                else
                {
                    paging = false;
                }
            }

            return userList;
        }

        private async Task<UserListResourceModel> _GetUsersInGroupAsync(string id, string cursor, int pageSize)
        {
            var request = new RestRequest($"groups/{id}/users");

            if (!string.IsNullOrEmpty(cursor))
            {
                request.AddParameter("cursor", cursor);
            }

            if (pageSize != null)
            {
                request.AddParameter("pageSize", pageSize);
            }
            
            var response = await _client.ExecuteAsync<UserListResourceModel>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            throw new AdobeSignFailedToFetchException($"Could not fetch membership of group {id}", response.ErrorException);
        }
    }
}
