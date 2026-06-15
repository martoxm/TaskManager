using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.DTOs
{
    public class UpdateTaskDto
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

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("^(pending|inProgress|completed)$", ErrorMessage = "Status must be pending, inProgress, or completed.")]
        public string Status { get; set; } = string.Empty;
    }
}