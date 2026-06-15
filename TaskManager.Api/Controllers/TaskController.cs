using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks")]
    public class TaskController(ITaskService taskService) : ControllerBase
    {
        private readonly ITaskService _taskService = taskService;

        #region POST /api/tasks

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            var task = await _taskService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        #endregion POST /api/tasks

        #region GET /api/tasks

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAll();
            return Ok(tasks);
        }

        #endregion GET /api/tasks

        #region GET /api/tasks/{id}

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskService.GetById(id);

            if (task == null)
                return NotFound(new { error = "Tarefa não encontrada." });

            return Ok(task);
        }

        #endregion GET /api/tasks/{id}

        #region PUT /api/tasks/{id}

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto dto)
        {
            var task = await _taskService.Update(id, dto);
            return Ok(task);
        }

        #endregion PUT /api/tasks/{id}

        #region DELETE /api/tasks/{id}

        [HttpDelete("{id:guid}")]
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