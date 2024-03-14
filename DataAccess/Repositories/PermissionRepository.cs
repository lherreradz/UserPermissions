using DataAccess.Repositories;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.Repositories
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(ApplicationContext context) : base(context)
        {
        }
        public Permission GetPermissionById(Guid guid)
        {
            return _context.Permission.First(d => d.Guid == guid);
        }
        public IEnumerable<Permission> GetPermissions()
        {
            return _context.Permission.ToList();
        }
    }
}
