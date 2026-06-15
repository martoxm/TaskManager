using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfaces
{
    // Interface do serviço — define CONTRATO (DIP)
    // Controller vai injetar essa interface (não a classe concreta)
    public interface ITaskService
    {
        Task<TaskDto> Create(CreateTaskDto dto);
        Task<TaskDto?> GetById(Guid id);
        Task<IEnumerable<TaskDto>> GetAll();
        Task<TaskDto> Update(Guid id, UpdateTaskDto dto);
        Task<bool> Delete(Guid id);
    }
}