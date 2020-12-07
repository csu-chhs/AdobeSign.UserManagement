using AdobeSign.UserManagement.Core.Interfaces;

namespace AdobeSign.UserManagement.Core
{
    public interface IAdobeSignClient
    {
        /// <summary>
        /// Access Adobe Sign's User Client API
        /// </summary>
        IUserClient User { get; }

        /// <summary>
        /// Access Adobe Sign's Group API
        /// </summary>
        IGroupClient Group { get; }
    }
}
