using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Permission
    {
        [Key]
        public Guid Guid { get; set; }

        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }

        [ForeignKey("PermissionType")]
        public Guid PermissionTypeId { get; set; }
    }
}
