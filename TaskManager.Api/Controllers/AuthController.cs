using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.Api.Controllers
{
    #region AuthController

    /// <summary>
    /// Controller responsável pelo registro e autenticação de usuários
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    [Produces("application/json")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Registra um novo usuário na aplicação
        /// </summary>
        /// <param name="dto">Dados do usuário a ser registrado</param>
        /// <returns>Dados do usuário criado</returns>
        /// <response code="200">Usuário registrado com sucesso</response>
        /// <response code="400">Dados inválidos ou usuário já existente</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Registrar usuário", Description = "Cria um novo usuário e retorna os dados cadastrados")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            var result = await _authService.Register(dto);
            return Ok(result);
        }

        /// <summary>
        /// Realiza o login e retorna o token JWT
        /// </summary>
        /// <param name="dto">Credenciais do usuário (email e senha)</param>
        /// <returns>Token JWT para autenticação</returns>
        /// <response code="200">Login realizado com sucesso, token JWT retornado</response>
        /// <response code="401">Credenciais inválidas</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Login", Description = "Autentica o usuário e retorna um Bearer token JWT")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var result = await _authService.Login(dto);
            return Ok(result);
        }
    }

    #endregion AuthController
}