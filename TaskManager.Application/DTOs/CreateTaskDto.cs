namespace TaskManager.Application.DTOs
{
    // DTO para criar nova tarefa
    // Recebe os dados do cliente (via POST)
    public class CreateTaskDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Priority { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}