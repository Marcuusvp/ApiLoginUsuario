using Aplicacao.Dtos;
using Aplicacao.Interfaces;
using Dominio.Mensagem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoginUsuario.Controllers
{
    public class CidadeController : BaseController
    {
        private readonly ICidadeService _cidadeService;
        public CidadeController(IMensagem mensagem, ICidadeService cidadeService) : base(mensagem)
        {
            _mensagem = mensagem;
            _cidadeService = cidadeService;
        }

        [Authorize(Roles = "ATENDENTE")]
        [HttpPost]
        [Route("/cadastrar-cidade")]
        public async Task<IActionResult> CreateNovaCidade([FromForm] NovaCidadeDto cidade)
        {
            if (cidade.Imagem != null)
            {
                // Salve o arquivo temporariamente e obtenha o caminho local
                var tempPath = Path.GetTempFileName();
                using (var fileStream = System.IO.File.Create(tempPath))
                {
                    await cidade.Imagem.CopyToAsync(fileStream);
                }

                // Atualize a ImagemUrl no DTO
                cidade.ImagemUrl = tempPath;
            }
            var resultado = await _cidadeService.CreateCidade(cidade);

            return GerarRetorno(resultado);
        }

        [Authorize(Roles = "ATENDENTE")]
        [HttpGet]
        [Route("/listar-cidades")]
        public async Task<IActionResult> GetAllCidades(int _page = 1, int _limit = 10, string nome_like = "")
        {
            var resultado = await _cidadeService.ListarCidades(_page, _limit, nome_like);
            Response.Headers.Add("x-total-count", resultado.TotalCount.ToString());
            return GerarRetorno(resultado.Data);
        }

        [Authorize(Roles = "ATENDENTE")]
        [HttpGet]
        [Route("/cidade-por-id/{id}")]
        public async Task<IActionResult> GetCidadePorId(int id)
        {
            var resultado = await _cidadeService.ListarCidadePorId(id);
            return GerarRetorno(resultado);
        }

        [Authorize(Roles = "ATENDENTE")]
        [HttpPut]
        [Route("/atualizar-cidade/{id}")]
        public async Task<IActionResult> UpdateCidade(int id, [FromForm] NovaCidadeDto cidade)
        {
            var resultado = await _cidadeService.AtualizarCidade(id, cidade);
            return GerarRetorno(resultado);
        }

        [Authorize(Roles = "ATENDENTE")]
        [HttpDelete]
        [Route("/deletar-cidade/{id}")]
        public async Task<IActionResult> DeleteCidadePorId(int id)
        {
            var resultado = await _cidadeService.DeletarCidadeId(id);
            return GerarRetorno(resultado);
        }
    }
}
