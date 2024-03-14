using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Employee
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [MinLength(3)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
