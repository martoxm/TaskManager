using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;
using Task = TaskManager.Domain.Entities.Task;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Application.Services
{
    // Serviço de tarefas — aplica regras de negócio
    // Segue SOLID:
    //   - SRP: só faz uma coisa (gerenciar tarefas)
    //   - DIP: depende de interface ITaskRepository (não implementação concreta)
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        // Injeção de dependência via construtor
        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        #region Método Create

        // Criar nova tarefa
        // 1. Converter DTO → Entity
        // 2. Validar entidade
        // 3. Salvar no repositório
        // 4. Converter Entity → DTO e retornar
        public async Task<TaskDto> Create(CreateTaskDto dto)
        {
            // Converter string → enum
            var priority = ParsePriority(dto.Priority);

            // Criar entidade
            var task = new Task();
            task.Update(dto.Name, dto.Description, priority, dto.DueDate, TaskStatus.Pending);

            // Salvar no repositório
            var createdTask = await _repository.Create(task);

            // Retornar DTO
            return TaskDto.FromEntity(createdTask);
        }

        #endregion Método Create

        #region Método GetById

        // Buscar tarefa por ID
        // 1. Buscar no repositório
        // 2. Se não existir, retornar null
        // 3. Converter Entity → DTO
        public async Task<TaskDto?> GetById(Guid id)
        {
            var task = await _repository.GetById(id);

            if (task == null)
                return null;

            return TaskDto.FromEntity(task);
        }

        #endregion Método GetById

        #region Método GetAll

        // Buscar todas as tarefas
        // 1. Buscar no repositório
        // 2. Converter lista Entity → DTO
        public async Task<IEnumerable<TaskDto>> GetAll()
        {
            var tasks = await _repository.GetAll();

            return tasks.Select(TaskDto.FromEntity);
        }

        #endregion Método GetAll

        #region Método Update

        // Atualizar tarefa existente
        // 1. Buscar tarefa pelo ID
        // 2. Se não existir, throw exception
        // 3. Converter DTO → enum
        // 4.调用 entity.Update()
        // 5. Salvar no repositório
        // 6. Retornar DTO
        public async Task<TaskDto> Update(Guid id, UpdateTaskDto dto)
        {
            var task = await _repository.GetById(id);

            if (task == null)
                throw new ValidationException("Tarefa não encontrada.");

            // Converter strings → enums
            var priority = ParsePriority(dto.Priority);
            var status = ParseTaskStatus(dto.Status);

            // atualizar entidade
            task.Update(dto.Name, dto.Description, priority, dto.DueDate, status);

            // Salvar
            var updatedTask = await _repository.Update(task);

            return TaskDto.FromEntity(updatedTask);
        }

        #endregion Método Update

        #region Método Delete

        // Excluir tarefa
        // 1. Buscar tarefa pelo ID
        // 2. Se não existir, throw exception
        // 3. Excluir no repositório
        public async Task<bool> Delete(Guid id)
        {
            var task = await _repository.GetById(id);

            if (task == null)
                throw new ValidationException("Tarefa não encontrada.");

            return await _repository.Delete(id);
        }

        #endregion Método Delete

        #region Métodos auxiliares (Parse enums)

        // Converter string → enum Priority
        private Priority ParsePriority(string priorityString)
        {
            if (string.IsNullOrWhiteSpace(priorityString))
                throw new ValidationException("Prioridade é obrigatória.");

            var normalized = priorityString.ToLower().Trim();

            switch (normalized)
            {
                case "low":
                    return Priority.Low;

                case "medium":
                    return Priority.Medium;

                case "high":
                    return Priority.High;

                default:
                    throw new ValidationException("Prioridade inválida. Valores permitidos: high, medium, low.");
            }
        }

        // Converter string → enum TaskStatus
        private TaskStatus ParseTaskStatus(string statusString)
        {
            if (string.IsNullOrWhiteSpace(statusString))
                return TaskStatus.Pending; // default

            var normalized = statusString.ToLower().Trim();

            switch (normalized)
            {
                case "pending":
                    return TaskStatus.Pending;

                case "inprogress":
                    return TaskStatus.InProgress;

                case "completed":
                    return TaskStatus.Completed;

                default:
                    throw new ValidationException("Status inválido. Valores permitidos: pending, inProgress, completed.");
            }
        }

        #endregion Métodos auxiliares (Parse enums)
    }
}