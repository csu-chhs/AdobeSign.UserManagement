using System;

namespace AdobeSign.UserManagement.Core.ResourceModels.Groups
{
    public class GroupDetailResourceModel
    {
        public string groupId { get; set; }
        public string groupName { get; set; }
        public DateTime? createdDate { get; set; }
        public bool isDefaultGroup { get; set; }
    }
}
