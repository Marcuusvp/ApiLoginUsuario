﻿using Dapper;
using Dominio.Models;
using Repositorio.Interfaces;
using System.Data;

namespace Repositorio.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConexaoBanco _conexao;
        public UsuarioRepository(ConexaoBanco conexao)
        {
            _conexao = conexao;
        }
        public async Task<bool> AddNewUser(Usuario user)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@USERNAME", user.UserName, DbType.String);
            param.Add("@EMAIL", user.Email, DbType.String);
            param.Add("@PASSWORDHASH", user.PasswordHash, DbType.String);
            var query = @"INSERT INTO USUARIOS (USERNAME, EMAIL, PASSWORDHASH) VALUES (@USERNAME, @EMAIL, @PASSWORDHASH)";
            var resultado = await sql.ExecuteAsync(query, param);
            return resultado > 0;
        }
        public async Task<Usuario> GetUser(Usuario user)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@EMAIL", user.Email, DbType.String);
            var query = @"SELECT ID, USERNAME, EMAIL, PASSWORDHASH FROM USUARIOS WHERE EMAIL = @EMAIL";
            return await sql.QueryFirstOrDefaultAsync<Usuario>(query, param);            
        }
        public async Task<IEnumerable<Permissoes>> GetRoles(Usuario user)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@EMAIL", user.Email, DbType.String);
            var query = @"SELECT p.NOME, p.CODIGO FROM USUARIOS u 
                            JOIN USUARIO_PERMISSOES up ON u.id = up.user_id
                            JOIN PERMISSOES p ON up.permission_id = p.id
                            WHERE u.email = @EMAIL
                            ";
            return await sql.QueryAsync<Permissoes>(query, param);
        }
        public async Task<bool> SetNewPassword(string novaSenha, string email)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@NOVASENHA", novaSenha, DbType.String);
            param.Add("@EMAIL", email, DbType.String);
            var query = @"UPDATE USUARIOS
                            SET PASSWORDHASH = @NOVASENHA
                            WHERE EMAIL = @EMAIL";
            var resultado = await sql.ExecuteAsync(query, param);
            return resultado > 0 ? true : false;

        }
    }
}
