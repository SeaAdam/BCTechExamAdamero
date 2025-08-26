using System.ComponentModel.DataAnnotations;

namespace BCTechExamAdamero.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
    }
}
