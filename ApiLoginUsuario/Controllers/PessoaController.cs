using Aplicacao.Dtos;
using Aplicacao.Interfaces;
using Aplicacao.Services;
using Dominio.Mensagem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoginUsuario.Controllers
{
    [ApiController]
    public class PessoaController : BaseController
    {
        private readonly IPessoaService _pessoaService;
        public PessoaController(IMensagem mensagem, IPessoaService pessoaService) : base(mensagem)
        {
            _mensagem = mensagem;
            _pessoaService = pessoaService;
        }

        //[Authorize(Roles = "GERENTE")]
        [AllowAnonymous]
        [HttpPost]
        [Route("/cadastrar-pessoa")]
        public async Task<IActionResult> CreateNovaPessoa([FromBody] NovaPessoaDto pessoa)
        {
            var resultado = await _pessoaService.CreatePessoa(pessoa);
            return GerarRetorno(resultado);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/listar-pessoas")]
        public async Task<IActionResult> GetAllPessoas(int _page = 1, int _limit = 10, string nomeCompleto_like = "")
        {
            var resultado = await _pessoaService.ListarPessoas(_page, _limit, nomeCompleto_like);
            Response.Headers.Add("x-total-count", resultado.TotalCount.ToString());
            return GerarRetorno(resultado.Data);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/pessoa-por-id/{id}")]
        public async Task<IActionResult> GetPessoaById(int id)
        {
            var resultado = await _pessoaService.ListarPessoaPorId(id);
            return GerarRetorno(resultado);
        }
        [AllowAnonymous]
        [HttpPut]
        [Route("/atualizar-pessoa/{id}")]
        public async Task<IActionResult> UpdatePessoa(int id, [FromBody]NovaPessoaDto pessoa)
        {
            var resultado = await _pessoaService.AtualizarPessoa(id, pessoa);
            return GerarRetorno(resultado);
        }
        [AllowAnonymous]
        [HttpDelete]
        [Route("/deletar-pessoa/{id}")]
        public async Task<IActionResult> DeletePessoaPorId(int id)
        {
            var resultado = await _pessoaService.DeletarPessoaId(id);
            return GerarRetorno(resultado);
        }
    }
}
