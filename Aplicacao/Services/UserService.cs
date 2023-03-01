using Aplicacao.Dtos;
using Aplicacao.Interfaces;
using AutoMapper;
using Dominio.Mensagem;
using Dominio.Models;
using Repositorio.Interfaces;
using SecureIdentity.Password;

namespace Aplicacao.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IMensagem _mensagem;
        private readonly IUsuarioRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public UserService(IMapper mapper, IMensagem mensagem, IUsuarioRepository userRepository, ITokenService tokenService, IEmailService emailService)
        {
            _mapper = mapper;
            _mensagem = mensagem;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<bool> CreateUser (NovoUsuarioDto usuario)
        {
            var user = _mapper.Map<Usuario>(usuario);
            user.PasswordHash = PasswordHasher.Hash(usuario.Password);
            var resultado = await _userRepository.AddNewUser(user);
            if (resultado)
            {
                _emailService.Send(
                    user.UserName, 
                    user.Email, 
                    "Bem vindo a minha API", 
                    $"Sua senha é <strong>{usuario.Password}</strong>");
            }
            return resultado;
        }
        
        public async Task<LoginDto> UserLogin(UsuarioDto usuario)
        {
            var user = _mapper.Map<Usuario>(usuario);
            var usuarioSolicitado = await _userRepository.GetUser(user);
            if(usuarioSolicitado == null)
            {
                _mensagem.AdicionaErro("Usuário inválido");
                return null;
            }
            var permissoes = await _userRepository.GetRoles(usuarioSolicitado);
            usuarioSolicitado.Permissoes = permissoes;
            if (PasswordHasher.Verify(usuarioSolicitado.PasswordHash, usuario.Password))
            {
                try
                {
                    var token = _tokenService.TokenGenerator(usuarioSolicitado);
                    var resultado = new LoginDto { Username = usuarioSolicitado.UserName, Permissoes = usuarioSolicitado.Permissoes, Token = token};
                    return resultado;
                }
                catch
                {
                    _mensagem.AdicionaErro("Impossível gerar token");
                    return null;
                }
            }
            else
            {
                _mensagem.AdicionaErro("Senha inválida");
                return null;
            }

        }
        public async Task<bool> AlterarSenha(AlteraSenhaUsuarioDto usuario)
        {
            var user = _mapper.Map<Usuario>(usuario);
            var usuarioSolicitado = await _userRepository.GetUser(user);
            if (usuarioSolicitado == null)
            {
                _mensagem.AdicionaErro("Usuário inválido");
                return false;
            }
            var newPassword = PasswordGenerator.Generate(25);
            var newHash = PasswordHasher.Hash(newPassword);
            await _userRepository.SetNewPassword(newHash, user.Email);
            _emailService.Send(
                    user.UserName,
                    user.Email,
                    "ALTERAÇÃO DE SENHA",
                    $"Sua nova senha é <strong>{newPassword}</strong>");
            return true;
        }
    }
}
