using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Enums;
using Task = TaskManager.Domain.Entities.Task;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Infrastructure.Configurations
{
    // Entity Configuration = mapeamento detalhado da entidade para o banco
    // Aqui definimos: tipos de coluna, constraints, nomes de tabela, etc
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        #region Método Configure

        public void Configure(EntityTypeBuilder<Task> builder)
        {
            // Configurar tabela
            builder.ToTable("Tasks");

            // Configurar Id (GUID, primary key)
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .HasColumnType("uuid")
                .IsRequired();

            // Configurar Name (string, max 100 chars, required)
            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            // Configurar Description (string opcional, max 500 chars)
            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnType("varchar(500)")
                .HasMaxLength(500)
                .IsRequired(false);

            // Configurar Priority (enum → string)
            builder.Property(t => t.Priority)
                .HasColumnName("Priority")
                .HasConversion(
                    p => p.ToString().ToLower(),
                    p => Enum.Parse<Priority>(p, true))
                .HasMaxLength(20)
                .IsRequired();

            // Configurar DueDate (DateTime, required)
            builder.Property(t => t.DueDate)
                .HasColumnName("DueDate")
                .HasColumnType("datetime")
                .IsRequired();

            // Configurar Status (enum → string)
            builder.Property(t => t.Status)
                 .HasColumnName("Status")
                 .HasConversion(
                     s => s.ToString().ToLower(),
                     s => Enum.Parse<TaskStatus>(s, true))
                 .HasMaxLength(20)
                 .IsRequired();
        }

        #endregion Método Configure
    }
}