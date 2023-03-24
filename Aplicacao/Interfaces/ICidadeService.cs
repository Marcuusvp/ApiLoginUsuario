using Aplicacao.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface ICidadeService
    {
        Task<bool> CreateCidade(NovaCidadeDto cidade);
        Task<(IEnumerable<CidadeDto>Data , int TotalCount)> ListarCidades(int pageIndex, int pageSize, string filter);
        Task<CidadeDto> ListarCidadePorId(int id);
        Task<bool> AtualizarCidade(int id, NovaCidadeDto pessoa);
        Task<bool> DeletarCidadeId(int id);
    }
}
