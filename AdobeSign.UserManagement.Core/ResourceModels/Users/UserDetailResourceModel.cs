namespace AdobeSign.UserManagement.Core.ResourceModels.Users
{
    public class UserDetailResourceModel
    {
        public string email { get; set; }
        public string id { get; set; }
        public bool isAccountAdmin { get; set; }
        public string company { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
