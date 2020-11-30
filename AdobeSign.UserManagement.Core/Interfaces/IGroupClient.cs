using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdobeSign.UserManagement.Core.ResourceModels.Groups;
using AdobeSign.UserManagement.Core.ResourceModels.Users;

namespace AdobeSign.UserManagement.Core.Interfaces
{
    public interface IGroupClient
    {
        /// <summary>
        /// Fetches all groups associated with your account.  This returns
        /// the first page and uses the standard page size set by the Adobe API.
        /// </summary>
        /// <returns></returns>
        Task<GroupListResourceModel> GetGroupsAsync();

        /// <summary>
        /// Fetches all groups associated with your account.  Allows
        /// the client to set their own cursor (page) as well as page size.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<GroupListResourceModel> GetGroupsAsync(string cursor, int? pageSize);

        /// <summary>
        /// Fetches a single group by its Adobe assigned ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GroupDetailResourceModel> GetGroupAsync(string id);

        /// <summary>
        /// Attempts to search for a group based upon its name.  Adobe does not
        /// provide a way for this to be done via the API so the entire groups
        /// list is loaded, and then searched using LINQ to find a match.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task<GroupDetailResourceModel> GetGroupByNameAsync(string groupName);

        /// <summary>
        /// Returns all users associated with a group.  ID is the
        /// Adobe assigned ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserListResourceModel> GetUsersInGroupAsync(string id);
    }
}
