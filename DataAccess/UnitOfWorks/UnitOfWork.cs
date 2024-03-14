using DataAccess.Repositories;
using DataAccess.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;

            Employees = new EmployeeRepository(_context);
            Permissions = new PermissionRepository(_context);
            PermissionTypes = new PermissionTypeRepository(_context);
        }
        public IEmployeeRepository Employees { get; private set; }
        public IPermissionRepository Permissions { get; private set; }
        public IPermissionTypeRepository PermissionTypes { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
