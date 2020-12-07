using System.Collections.Generic;
using AdobeSign.UserManagement.Core.ResourceModels.Base;

namespace AdobeSign.UserManagement.Core.ResourceModels.Users
{
    public class UserListResourceModel
    {
        public List<UserDetailResourceModel> userInfoList { get; set; }
        public PageResourceModel page { get; set; }
    }
}


/*
{
"page": {
    "nextCursor": ""
},
"userInfoList": [
{
    "email": "",
    "id": "",
    "isAccountAdmin": false,
    "accountId": "",
    "company": "",
    "firstName": "",
    "lastName": ""
}
]
}*/