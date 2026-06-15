using Microsoft.EntityFrameworkCore;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data
{
    // DbContext = ponto de acesso ao banco de dados (EF Core)
    // Configura entidades e connection string
    public class TaskDbContext : DbContext
    {
        // Construtor — recebe Configuration do Program.cs
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        // DbSet = tabela no banco de dados
        public DbSet<Task> Tasks => Set<Task>();

        #region Método OnModelCreating

        // Configura mapeamento de entidades (opcional, mas recomendado)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar configurações da entidade Task
            modelBuilder.Entity<Task>();

            base.OnModelCreating(modelBuilder);
        }

        #endregion Método OnModelCreating

        #region Método OnConfiguring

        // Configura opções do DbContext (opcional)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Opcional: adicionar logging
            if (!optionsBuilder.IsConfigured)
            {
                // optionsBuilder.LogTo(Console.WriteLine); // para debug
            }

            base.OnConfiguring(optionsBuilder);
        }

        #endregion Método OnConfiguring
    }
}