using System;

namespace AdobeSign.UserManagement.Core.ResourceModels.Users
{
    public class UserGroupInfoResourceModel
    {
        public string id { get; set; }
        public bool isGroupAdmin { get; set; }
        public bool isPrimaryGroup { get; set; }
        public string status { get; set; }
        public DateTime? createdDate { get; set; }
        public string name { get; set; }
        public UserSettingsResourceModel settings { get; set; }
    }
}
