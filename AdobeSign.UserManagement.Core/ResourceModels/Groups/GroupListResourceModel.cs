using System;
using System.Collections.Generic;
using System.Text;
using AdobeSign.UserManagement.Core.ResourceModels.Base;

namespace AdobeSign.UserManagement.Core.ResourceModels.Groups
{
    public class GroupListResourceModel
    {
        public List<GroupDetailResourceModel> groupInfoList { get; set; }
        public PageResourceModel page { get; set; }
    }
}
