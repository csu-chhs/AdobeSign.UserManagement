using System.Collections.Generic;

namespace AdobeSign.UserManagement.Core.ResourceModels.Users
{
    public class UsersGroupsResourceModel
    {
        public UsersGroupsResourceModel()
        {
            groupInfoList = new List<UserGroupInfoResourceModel>();
        }

        public List<UserGroupInfoResourceModel> groupInfoList { get; set; }
    }
}