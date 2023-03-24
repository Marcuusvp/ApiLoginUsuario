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
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;
        private readonly IMensagem _mensagem;
        public PessoaService(IMapper mapper, IMensagem mensagem, IPessoaRepository pessoaRepository)
        {
            _mapper = mapper;
            _mensagem = mensagem;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<bool> CreatePessoa(NovaPessoaDto pessoa)
        {
            var cliente = _mapper.Map<Pessoas>(pessoa);
            return await _pessoaRepository.AddPessoa(cliente);
        }

        public async Task<(IEnumerable<PessoaDto> Data, int TotalCount)> ListarPessoas(int pageIndex, int pageSize, string filter)
        {
            var resultado = await _pessoaRepository.GetPessoas();

            // Aplique o filtro se existir
            if (!string.IsNullOrEmpty(filter))
            {
                resultado = resultado.Where(c => c.NomeCompleto.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }
            int totalCount = resultado.Count();
            // Aplicar paginação
            resultado = resultado
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            var mappedResult = _mapper.Map<IEnumerable<PessoaDto>>(resultado);

            return (mappedResult, totalCount);
        }




        public async Task<PessoaDto> ListarPessoaPorId(int id)
        {
            var resultado = await _pessoaRepository.GetPessoaById(id);
            if(resultado == null)
            {
                _mensagem.AdicionaErro("Pessoa não encontrada");
                return null;
            }
            return _mapper.Map<PessoaDto>(resultado);
        }

        public async Task<bool> AtualizarPessoa(int id, NovaPessoaDto pessoa)
        {
            var pessoaMapeada = _mapper.Map<Pessoas>(pessoa);
            return await _pessoaRepository.UpdatePessoa(id, pessoaMapeada);
        }

        public async Task<bool> DeletarPessoaId(int id)
        {
            return await _pessoaRepository.DeletePessoaId(id);
        }
    }
}
