using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PermissionType
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
