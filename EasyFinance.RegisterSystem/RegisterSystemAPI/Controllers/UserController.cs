using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegisterSystem.Application.Services.UserServices.Commands.Register;
using RegisterSystem.Application.Services.UserServices.Queries.Login;
using RegisterSystem.Contracts.User.Requests;
using RegisterSystem.Contracts.User.Resonses;

namespace RegisterSystem.Api.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações de usuário.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="request">Dados de registro do usuário.</param>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST api/user/register
        ///     {
        ///        "name": "Nome do Usuário",
        ///        "email": "email@exemplo.com"
        ///        "password": "Senha1234"
        ///     }
        ///
        ///     * Senha entre 50 e 6 caracteres.
        ///
        /// </remarks>
        /// <response code="201">Retorna os detalhes do usuário recém-criado.</response>
        /// <response code="400">Se a requisição é inválida.</response>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest request)
        {
            var command = new RegisterCommand(request.name, request.email, request.password);

            ErrorOr<RegisterResult> result = await _mediator.Send(command);

            return result.Match(
                result => CreatedAtAction(nameof(RegisterUser),
                            new UserRegisterResponse(result.id, result.name, result.email, result.registerDate)),
                errors => GetErrorsResult(errors));
        }

        /// <summary>
        /// Autenticar usuário.
        /// </summary>
        /// <param name="request">Dados de login do usuário.</param>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST api/user/login
        ///     {
        ///        "email": "email@exemplo.com"
        ///        "password": "Senha1234"
        ///     }
        ///
        ///     * Senha entre 50 e 6 caracteres.
        ///
        /// </remarks>
        /// <response code="201">Retorna os detalhes do usuário autenticado.</response>
        /// <response code="400">Se a requisição é inválida.</response>
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequest request)
        {
            var query = new LoginQuery(request.email, request.password);

            ErrorOr<LoginResult> result = await _mediator.Send(query);

            return result.Match(
                result => CreatedAtAction(nameof(RegisterUser),
                            new UserLoginResponse(result.id, result.name, result.email, result.token)),
                errors => GetErrorsResult(errors));
        }
    }
}