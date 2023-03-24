using Aplicacao.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IPessoaService
    {
        Task<bool> CreatePessoa(NovaPessoaDto pessoa);
        Task<(IEnumerable<PessoaDto> Data, int TotalCount)> ListarPessoas(int pageIndex, int pageSize, string filter);
        Task<PessoaDto> ListarPessoaPorId(int id);
        Task<bool> AtualizarPessoa(int id, NovaPessoaDto pessoa);
        Task<bool> DeletarPessoaId(int id);
    }
}
