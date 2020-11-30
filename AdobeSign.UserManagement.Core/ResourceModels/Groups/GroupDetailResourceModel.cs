using System;
using System.Collections.Generic;
using System.Text;

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
