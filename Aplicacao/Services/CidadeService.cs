using Aplicacao.Dtos;
using Aplicacao.Interfaces;
using AutoMapper;
using Dominio.Mensagem;
using Dominio.Models;
using Repositorio.Interfaces;

namespace Aplicacao.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IMapper _mapper;
        private readonly IMensagem _mensagem;
        private readonly IImagemService _imagemService;
        public CidadeService(IMapper mapper, IMensagem mensagem, ICidadeRepository cidadeRepository, IImagemService imagemService)
        {
            _mapper = mapper;
            _mensagem = mensagem;
            _cidadeRepository = cidadeRepository;
            _imagemService = imagemService;
        }

        public async Task<bool> CreateCidade (NovaCidadeDto cidade)
        {
            if (cidade.Imagem != null)
            {
                cidade.ImagemUrl = await _imagemService.UploadImageAsync(cidade.Imagem);
            }
            var cidadeNova = _mapper.Map<Cidade>(cidade);
            return await _cidadeRepository.AddCidade(cidadeNova);
        }

        public async Task<(IEnumerable<CidadeDto>Data, int TotalCount)> ListarCidades(int pageIndex, int pageSize, string filter)
        {
            var resultado = await _cidadeRepository.GetCidades();
            // Aplique o filtro se existir
            if (!string.IsNullOrEmpty(filter))
            {
                resultado = resultado.Where(c => c.Nome.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }
            int totalCount = resultado.Count();
            resultado = resultado
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
            var mappedResult = _mapper.Map<IEnumerable<CidadeDto>>(resultado);
            return (mappedResult, totalCount);
        }

        public async Task<CidadeDto> ListarCidadePorId(int id)
        {
            var resultado = await _cidadeRepository.GetCidadeById(id);
            if (resultado == null)
            {
                _mensagem.AdicionaErro("Cidade não encontrada");
                return null;
            }
            return _mapper.Map<CidadeDto>(resultado);
        }

        public async Task<bool> AtualizarCidade(int id, NovaCidadeDto cidadeDto)
        {
            var cidadeExistente = await _cidadeRepository.GetCidadeById(id);

            if (cidadeExistente == null)
            {
                _mensagem.AdicionaErro("Cidade não encontrada");
                return false;
            }

            if (cidadeDto.Imagem != null)
            {
                cidadeDto.ImagemUrl = await _imagemService.UploadImageAsync(cidadeDto.Imagem);
            }

            var cidadeAtualizada = _mapper.Map<Cidade>(cidadeDto);

            // Copie as propriedades de cidadeAtualizada para cidadeExistente
            cidadeExistente.Nome = cidadeAtualizada.Nome;
            cidadeExistente.ImagemUrl = cidadeAtualizada.ImagemUrl ?? cidadeExistente.ImagemUrl;

            return await _cidadeRepository.UpdateCidade(id, cidadeExistente);
        }


        public async Task<bool> DeletarCidadeId(int id)
        {
            return await _cidadeRepository.DeletarCidadeId(id);
        }
    }
}
