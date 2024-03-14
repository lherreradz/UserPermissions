using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
        Permission GetPermissionById(Guid guid);
        IEnumerable<Permission> GetPermissions();
    }
}
