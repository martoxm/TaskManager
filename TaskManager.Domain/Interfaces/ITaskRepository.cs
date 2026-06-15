using Task = TaskManager.Domain.Entities.Task;


namespace TaskManager.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<Task?> GetById(Guid id);
        Task<IEnumerable<Task>> GetAll();
        Task<Task> Create(Task task);
        Task<Task> Update(Task task);
        Task<bool> Delete(Guid id);
    }
}