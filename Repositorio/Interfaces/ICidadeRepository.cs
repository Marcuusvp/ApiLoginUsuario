using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Interfaces
{
    public interface ICidadeRepository
    {
        Task<bool> AddCidade(Cidade cidade);
        Task<IEnumerable<Cidade>> GetCidades();
        Task<Cidade> GetCidadeById(int id);
        Task<bool> UpdateCidade(int id, Cidade cidade);
        Task<bool> DeletarCidadeId(int id);

    }
}
