using System;

namespace AdobeSign.UserManagement.Core.ResourceModels.Groups
{
    public class GroupSingleResourceModel
    {
        public DateTime? created { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public bool isDefaultGroup { get; set; }
    }
}
