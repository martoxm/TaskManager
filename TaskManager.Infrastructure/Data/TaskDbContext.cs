using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data
{
    #region TaskDbContext

    public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
    {
        public DbSet<Task> Tasks => Set<Task>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

    #endregion
}