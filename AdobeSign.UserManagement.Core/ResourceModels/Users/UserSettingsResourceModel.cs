using System;
using System.Collections.Generic;
using System.Text;

namespace AdobeSign.UserManagement.Core.ResourceModels.Users
{
    public class UserSettingsResourceModel
    {
        public UserSettingsValuesResourceModel libaryDocumentCreationVisible { get; set; }
        public UserSettingsValuesResourceModel userCanSend { get; set; }
    }
}
