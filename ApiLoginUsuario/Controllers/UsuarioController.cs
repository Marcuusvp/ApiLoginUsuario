using Aplicacao.Dtos;
using Aplicacao.Interfaces;
using Dominio.Mensagem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoginUsuario.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UsuarioController : BaseController
    {
        private readonly IUserService _userService;

        public UsuarioController(IUserService userService, IMensagem mensagem) : base(mensagem)
        {
            _userService = userService;
            _mensagem = mensagem;
        }

        [Authorize(Roles = "GERENTE")]
        [HttpPost]
        [Route("/cadastrar")]        
        public async Task<IActionResult> CreateNewUser([FromBody]NovoUsuarioDto usuario)
        {
            var resultado = await _userService.CreateUser(usuario);
            return GerarRetorno(resultado);
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody]UsuarioDto usuario)
        {
            var resultado = await _userService.UserLogin(usuario);
            return GerarRetorno(resultado);
        }

        [AllowAnonymous]
        [HttpPost("/alterar-senha")]
        public async Task<IActionResult> AlterarSenha([FromBody]AlteraSenhaUsuarioDto usuario)
        {
            var resultado = await _userService.AlterarSenha(usuario);
            return GerarRetorno(resultado);
        }
    }
}
