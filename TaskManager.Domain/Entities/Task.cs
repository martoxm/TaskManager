using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Domain.Entities
{
    public class Task
    {
        #region Propriedades da entidade

        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Priority Priority { get; private set; }
        public DateTime DueDate { get; private set; }
        public TaskStatus Status { get; private set; }

        #endregion Propriedades da entidade

        #region Construtor — gera GUID automaticamente

        public Task()
        {
            Id = Guid.NewGuid();
        }

        #endregion Construtor — gera GUID automaticamente

        #region Método Update

        // Altera múltiplas propriedades da tarefa
        // Aplica validações antes de alterar
        public void Update(string name, string? description, Priority priority, DateTime dueDate, TaskStatus status)
        {
            ValidateName(name);
            ValidateDueDate(dueDate);

            Name = name;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            Status = status;
        }

        #endregion Método Update

        #region Métodos para mudar status

        public void MarkAsPending() => Status = TaskStatus.Pending;

        public void MarkAsInProgress() => Status = TaskStatus.InProgress;

        public void MarkAsCompleted() => Status = TaskStatus.Completed;

        #endregion Métodos para mudar status

        #region Validação de Name

        // Regra 1: não pode ser vazio
        // Regra 2: não pode ter mais de 100 caracteres
        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Nome não pode ser vazio.");

            if (name.Length > 100)
                throw new ValidationException("Nome não pode ter mais de 100 caracteres.");
        }

        #endregion Validação de Name

        #region Validação de DueDate

        // Regra: data não pode estar no passado
        private void ValidateDueDate(DateTime dueDate)
        {
            if (dueDate < DateTime.Now)
                throw new ValidationException("Data limite não pode estar no passado.");
        }

        #endregion Validação de DueDate
    }
}