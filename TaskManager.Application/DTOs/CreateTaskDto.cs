using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.DTOs
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        [RegularExpression("^(high|medium|low)$", ErrorMessage = "Priority must be high, medium, or low.")]
        public string Priority { get; set; } = string.Empty;

        [Required(ErrorMessage = "Due date is required.")]
        public DateTime DueDate { get; set; }
    }
}