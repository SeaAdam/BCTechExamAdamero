using System.ComponentModel.DataAnnotations;

namespace BCTechExamAdamero.Dto
{
    public class CreateEmployeesDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
