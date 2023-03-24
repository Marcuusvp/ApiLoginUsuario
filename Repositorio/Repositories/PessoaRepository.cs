using Repositorio.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Models;
using System.Data;

namespace Repositorio.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ConexaoBanco _conexao;
        public PessoaRepository(ConexaoBanco conexao)
        {
            _conexao = conexao;
        }

        public async Task<bool> AddPessoa(Pessoas pessoa)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@Email", pessoa.Email, DbType.String);
            param.Add("@NomeCompleto", pessoa.NomeCompleto, DbType.String);
            param.Add("@CidadeId", pessoa.CidadeId, DbType.Int32);
            var query = @"INSERT INTO CLIENTES (EMAIL, NOME_COMPLETO, CIDADE_ID) VALUES (@Email, @NomeCompleto, @CidadeId)";
            var resultado = await sql.ExecuteAsync(query, param);
            return resultado > 0;
        }

        public async Task<IEnumerable<Pessoas>> GetPessoas()
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            var query = @"SELECT ID as ""Id""
                        , EMAIL as ""Email""
                        , NOME_COMPLETO as ""NomeCompleto""
                        , CIDADE_ID as ""CidadeId"" 
                        FROM CLIENTES";
            var resultado = await sql.QueryAsync<Pessoas>(query, param);
            return resultado;
        }

        public async Task<Pessoas> GetPessoaById(int id)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Int32);
            var query = @"SELECT ID as ""Id""
                        , EMAIL as ""Email""
                        , NOME_COMPLETO as ""NomeCompleto""
                        , CIDADE_ID as ""CidadeId"" 
                        FROM CLIENTES
                        WHERE ID = @Id";
            var resultado = await sql.QueryFirstOrDefaultAsync<Pessoas>(query, param);
            return resultado;
        }

        public async Task<bool> UpdatePessoa(int id, Pessoas pessoa)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Int32);
            param.Add("@Email", pessoa.Email, DbType.String);
            param.Add("@Nome", pessoa.NomeCompleto, DbType.String);
            param.Add("@CidadeId", pessoa.CidadeId, DbType.Int32);
            var query = @"UPDATE CLIENTES
                            SET EMAIL = @Email,
                                NOME_COMPLETO = @Nome,
                                CIDADE_ID = @CidadeId
                                WHERE ID = @Id";
            var resultado =  await sql.ExecuteAsync(query, param);
            return resultado > 0;
        }

        public async Task<bool> DeletePessoaId(int id)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Int32);
            var query = @"DELETE FROM CLIENTES
                            WHERE ID = @Id";
            var resultado = await sql.ExecuteAsync(query, param);
            return resultado > 0;
        }
    }
}
