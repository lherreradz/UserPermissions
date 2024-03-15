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
        public Permission GetPermissionById(int id)
        {
            return _context.Permission.First(d => d.Id == id);
        }
        public IEnumerable<Permission> GetPermissions()
        {
            return _context.Permission.ToList();
        }
    }
}
