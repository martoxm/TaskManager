namespace TaskManager.Application.DTOs
{
    // DTO para atualizar tarefa existente
    // Recebe os dados do cliente (via PUT)
    public class UpdateTaskDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Priority { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}