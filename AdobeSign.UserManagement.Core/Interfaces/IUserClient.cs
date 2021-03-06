﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AdobeSign.UserManagement.Core.ResourceModels.Users;

namespace AdobeSign.UserManagement.Core.Interfaces
{
    public interface IUserClient
    {
        /// <summary>
        /// Fetch all Adobe Sign users in the organization.  Will
        /// return the first page of results with the page size determined
        /// by the Adobe API.
        /// </summary>
        /// <returns></returns>
        Task<UserListResourceModel> GetAllAdobeUsersAsync();

        /// <summary>
        /// Fetch all Adobe Sign users in the organization while specifying
        /// the cursor (page) and size of pages.  
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<UserListResourceModel> GetAllAdobeUsersAsync(string cursor, int? pageSize);

        /// <summary>
        /// Returns the full set of users within the organization.  Pages
        /// through all results until the end.  This method
        /// typically does require a longer load time.
        /// </summary>
        /// <returns></returns>
        Task<List<UserDetailResourceModel>> GetAllAdobeUsersFullListAsync();

        /// <summary>
        /// Fetch a single user via the ID assigned by Adobe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDetailResourceModel> GetAdobeUserAsync(string id);

        /// <summary>
        /// Fetch all the groups that a given user is in.  ID is
        /// the Adobe assigned User ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UsersGroupsResourceModel> GetAdobeUsersGroupsAsync(string id);

        /// <summary>
        /// Attempt to search for an Adobe User via an Email Address.  Adobe
        /// only supports fetching a user via their assigned ID.  This method
        /// will paginate through results of the above GetAllAdobeUsersAsync() call
        /// and then use LINQ to attempt to find a result.  This method is typically very
        /// slow to return results and is not ideal.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserDetailResourceModel> GetAdobeUserViaEmailAsync(string email);

        /// <summary>
        /// Updates a given user's groups.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userGroups"></param>
        /// <returns></returns>
        Task UpdateUserGroupsAsync(string id, UsersGroupsResourceModel userGroups);
    }
}
