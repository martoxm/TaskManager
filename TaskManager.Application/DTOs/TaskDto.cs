using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.DTOs
{
    // DTO para retornar tarefa ao cliente
    // Converte entidade Domain → DTO para API
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Priority { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;

        // Método para converter Entity → DTO
        public static TaskDto FromEntity(Task task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Priority = task.Priority.ToString().ToLower(),
                DueDate = task.DueDate,
                Status = task.Status.ToString().ToLower()
            };
        }
    }
}