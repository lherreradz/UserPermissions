using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
        Permission GetPermissionById(int id);
        IEnumerable<Permission> GetPermissions();
    }
}
