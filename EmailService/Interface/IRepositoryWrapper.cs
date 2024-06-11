using GaragesStructure.Interface;

namespace GaragesStructure.Repository
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IPermissionRepository Permission { get; }

        IRoleRepository Role { get; }

        // here to add

        
        INotificationRepository Notification { get; }
        
    }
}
