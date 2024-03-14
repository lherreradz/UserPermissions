using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IPermissionRepository Permissions { get; }
        IPermissionTypeRepository PermissionTypes { get; }
        int Complete();
    }
}
