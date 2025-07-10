using GerencialClube.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeCliente.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AutenticacaoController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            var token = _authService.GerarToken();
            return Ok(new { token });
        }
    }
}
