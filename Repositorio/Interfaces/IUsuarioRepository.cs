using Dominio.Models;


namespace Repositorio.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> AddNewUser(Usuario user);
        Task<Usuario> GetUser(Usuario user);
        Task<IEnumerable<Permissoes>> GetRoles(Usuario user);
    }
}
