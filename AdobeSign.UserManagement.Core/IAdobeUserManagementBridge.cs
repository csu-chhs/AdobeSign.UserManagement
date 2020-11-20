namespace AdobeSign.UserManagement.Core
{
    interface IAdobeUserManagementBridge
    {
        void AddUserToAdobeGroup(string adobeEmail, string adobeGroupName);
        void RemoveUserFromAdobeGroup(string adobeEmail, string adobeGroupName);
    }
}
