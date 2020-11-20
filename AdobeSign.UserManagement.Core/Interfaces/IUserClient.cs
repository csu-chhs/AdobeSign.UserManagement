using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdobeSign.UserManagement.Core.ResourceModels;

namespace AdobeSign.UserManagement.Core.Interfaces
{
    interface IUserClient
    {
        Task<UserInfoResourceModel> GetAllAdobeUsersAsync();
        Task<UserDetailResourceModel> GetAdobeUserAsync(int id);
        Task<UsersGroupsResourceModel> GetAdobeUsersGroupsAsync(int id);
        //Task<bool> UpdateAdobeGroupsForUser();
    }
}
