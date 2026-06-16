using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Repositories
{
    // Implementação concreta de ITaskRepository
    // Usa EF Core para acessar o banco MySQL
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        // Injeção de dependência via construtor
        public TaskRepository(TaskDbContext context) => _context = context;

        #region Método GetById

        public async Task<Task?> GetById(Guid id)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        #endregion Método GetById

        #region Método GetAll

        public async Task<IEnumerable<Task>> GetAll()
        {
            return await _context.Tasks
                .ToListAsync();
        }

        #endregion Método GetAll

        #region Método Create

        public async Task<Task> Create(Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        #endregion Método Create

        #region Método Update

        public async Task<Task> Update(Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }

        #endregion Método Update

        #region Método Delete

        public async Task<bool> Delete(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }

        #endregion Método Delete
    }
}