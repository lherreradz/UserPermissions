using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            try {                 
                Database.EnsureCreated();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionType> PermissionType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                               new Employee
                               {
                                   Id = 1,
                                   FirstName = "John",
                                   LastName = "Doe",
                                   Email = "jdoe@fdab.com"
                               },
                               new Employee
                               {
                                   Id = 2,
                                    FirstName = "Karen",
                                    LastName = "Doe",
                                    Email = "kd@fdab.com"
                               },
                               new Employee
                               {
                                   Id = 3,
                                   FirstName = "Mike",
                                   LastName = "Smith",
                                   Email = "ms@fdab.com"
                               },
                               new Employee
                               {
                                   Id = 4,
                                   FirstName = "Nate",
                                   LastName = "Rodriguez",
                                   Email = "nr@fdab.com"
                               }
            );

            modelBuilder.Entity<PermissionType>().HasData(
                               new PermissionType
                               {
                                   Id = 1,
                                    Description = "Admin"
                                },
                                new PermissionType
                                {
                                    Id = 2,
                                    Description = "Read"
                                }
           );

            modelBuilder.Entity<Permission>().HasData(
                           new Permission
                           {
                               Id = 1,
                               EmployeeId = 1,
                               PermissionTypeId = 1
                            },
                           new Permission
                           {
                               Id = 2,
                               EmployeeId = 2,
                               PermissionTypeId = 1
                            }
           );

        }
    }
}
