using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PermissionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
