using Aplicacao.Dtos;

namespace Aplicacao.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(NovoUsuarioDto usuario);
        Task<LoginDto> UserLogin(UsuarioDto usuario);
        Task<bool> AlterarSenha(AlteraSenhaUsuarioDto usuario);
    }
}
 