using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de tarefas do usuário autenticado
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/tasks")]
    [Produces("application/json")]
    public class TaskController(ITaskService taskService) : ControllerBase
    {
        private readonly ITaskService _taskService = taskService;

        #region POST /api/tasks

        /// <summary>
        /// Cria uma nova tarefa
        /// </summary>
        /// <param name="dto">Dados da tarefa a ser criada</param>
        /// <returns>Tarefa criada com seu ID gerado</returns>
        /// <response code="201">Tarefa criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado — token JWT ausente ou inválido</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Criar tarefa", Description = "Cria uma nova tarefa e retorna os dados com o ID gerado")]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            var task = await _taskService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        #endregion POST /api/tasks

        #region GET /api/tasks

        /// <summary>
        /// Retorna todas as tarefas cadastradas
        /// </summary>
        /// <returns>Lista de tarefas</returns>
        /// <response code="200">Lista retornada com sucesso</response>
        /// <response code="401">Não autorizado — token JWT ausente ou inválido</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Listar tarefas", Description = "Retorna todas as tarefas cadastradas na aplicação")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAll();
            return Ok(tasks);
        }

        #endregion GET /api/tasks

        #region GET /api/tasks/{id}

        /// <summary>
        /// Retorna uma tarefa pelo ID
        /// </summary>
        /// <param name="id">ID (GUID) da tarefa</param>
        /// <returns>Dados da tarefa encontrada</returns>
        /// <response code="200">Tarefa encontrada com sucesso</response>
        /// <response code="401">Não autorizado — token JWT ausente ou inválido</response>
        /// <response code="404">Tarefa não encontrada</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Buscar tarefa por ID", Description = "Retorna os dados de uma tarefa específica pelo seu GUID")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskService.GetById(id);

            if (task == null)
                return NotFound(new { error = "Tarefa não encontrada." });

            return Ok(task);
        }

        #endregion GET /api/tasks/{id}

        #region PUT /api/tasks/{id}

        /// <summary>
        /// Atualiza os dados de uma tarefa existente
        /// </summary>
        /// <param name="id">ID (GUID) da tarefa a ser atualizada</param>
        /// <param name="dto">Novos dados da tarefa</param>
        /// <returns>Tarefa atualizada</returns>
        /// <response code="200">Tarefa atualizada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado — token JWT ausente ou inválido</response>
        /// <response code="404">Tarefa não encontrada</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Atualizar tarefa", Description = "Atualiza os dados de uma tarefa existente pelo seu GUID")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto dto)
        {
            var task = await _taskService.Update(id, dto);
            return Ok(task);
        }

        #endregion PUT /api/tasks/{id}

        #region DELETE /api/tasks/{id}

        /// <summary>
        /// Remove uma tarefa pelo ID
        /// </summary>
        /// <param name="id">ID (GUID) da tarefa a ser removida</param>
        /// <response code="204">Tarefa removida com sucesso</response>
        /// <response code="401">Não autorizado — token JWT ausente ou inválido</response>
        /// <response code="404">Tarefa não encontrada</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Deletar tarefa", Description = "Remove permanentemente uma tarefa pelo seu GUID")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _taskService.Delete(id);

            if (!deleted)
                return NotFound(new { error = "Tarefa não encontrada." });

            return NoContent();
        }

        #endregion DELETE /api/tasks/{id}
    }
}