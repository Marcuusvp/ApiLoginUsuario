using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Interfaces
{
    public interface IPessoaRepository
    {
        Task<bool> AddPessoa(Pessoas pessoa);
        Task<Pessoas> GetPessoaById(int id);
        Task<IEnumerable<Pessoas>> GetPessoas();
        Task<bool> UpdatePessoa(int id, Pessoas pessoa);
        Task<bool> DeletePessoaId(int id);
    }
}
