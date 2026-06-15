using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController(ITaskService taskService) : ControllerBase
    {
        #region Dependência do serviço

        private readonly ITaskService _taskService = taskService;

        #endregion Dependência do serviço

        #region POST /api/tasks

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            try
            {
                // Cria a tarefa usando a camada de Application.
                var task = await _taskService.Create(dto);

                // Retorna 201 Created com rota para buscar pelo Id.
                return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
            }
            catch (ValidationException ex)
            {
                // Retorna erro de validação de domínio.
                return BadRequest(new { error = ex.Message });
            }
        }

        #endregion POST /api/tasks

        #region GET /api/tasks

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Busca todas as tarefas.
            var tasks = await _taskService.GetAll();

            // Retorna 200 OK com a lista.
            return Ok(tasks);
        }

        #endregion GET /api/tasks

        #region GET /api/tasks/{id}

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Busca a tarefa pelo Id.
            var task = await _taskService.GetById(id);

            // Se não existir, retorna 404.
            if (task == null)
                return NotFound(new { error = "Tarefa não encontrada." });

            // Se existir, retorna 200 OK.
            return Ok(task);
        }

        #endregion GET /api/tasks/{id}

        #region PUT /api/tasks/{id}

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto dto)
        {
            try
            {
                // Atualiza a tarefa.
                var task = await _taskService.Update(id, dto);

                // Retorna 200 OK com a tarefa atualizada.
                return Ok(task);
            }
            catch (ValidationException ex)
            {
                // Se a tarefa não existir, retorna 404.
                if (ex.Message == "Tarefa não encontrada.")
                    return NotFound(new { error = ex.Message });

                // Para outros erros de regra, retorna 400.
                return BadRequest(new { error = ex.Message });
            }
        }

        #endregion PUT /api/tasks/{id}

        #region DELETE /api/tasks/{id}

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                // Remove a tarefa.
                var deleted = await _taskService.Delete(id);

                // Se não encontrou, retorna 404.
                if (!deleted)
                    return NotFound(new { error = "Tarefa não encontrada." });

                // Exclusão concluída sem conteúdo.
                return NoContent();
            }
            catch (ValidationException ex)
            {
                // Mantém resposta de erro padronizada.
                return NotFound(new { error = ex.Message });
            }
        }

        #endregion DELETE /api/tasks/{id}
    }
}