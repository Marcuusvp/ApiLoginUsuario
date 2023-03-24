using Aplicacao.Dtos;
using Aplicacao.Interfaces;
using AutoMapper;
using Dominio.Mensagem;
using Dominio.Models;
using Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IMapper _mapper;
        private readonly IMensagem _mensagem;
        public CidadeService(IMapper mapper, IMensagem mensagem, ICidadeRepository cidadeRepository)
        {
            _mapper = mapper;
            _mensagem = mensagem;
            _cidadeRepository = cidadeRepository;
        }

        public async Task<bool> CreateCidade (NovaCidadeDto cidade)
        {
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

        public async Task<bool> AtualizarCidade(int id, NovaCidadeDto pessoa)
        {
            var pessoaMapeada = _mapper.Map<Cidade>(pessoa);
            return await _cidadeRepository.UpdateCidade(id, pessoaMapeada);
        }

        public async Task<bool> DeletarCidadeId(int id)
        {
            return await _cidadeRepository.DeletarCidadeId(id);
        }
    }
}
