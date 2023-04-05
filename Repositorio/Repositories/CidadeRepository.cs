using Dapper;
using Dominio.Models;
using Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly ConexaoBanco _conexao;
        public CidadeRepository(ConexaoBanco conexao)
        {
            _conexao = conexao;
        }

        public async Task<bool> AddCidade(Cidade cidade)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@Nome", cidade.Nome, DbType.String);
            param.Add("@ImagemUrl", cidade.ImagemUrl, DbType.String);
            var query = @"INSERT INTO CIDADES (NOME, IMAGEM_URL) VALUES (@Nome, @ImagemUrl)";
            var resultado = await sql.ExecuteAsync(query, param);
            return resultado > 0;
        }

        public async Task<IEnumerable<Cidade>> GetCidades()
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            var query = @"SELECT ID as ""Id""
                        , NOME as ""Nome""  
                        , IMAGEM_URL as ""ImagemUrl""
                        FROM CIDADES";
            var resultado = await sql.QueryAsync<Cidade>(query, param);
            return resultado;
        }

        public async Task<Cidade> GetCidadeById(int id)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Int32);
            var query = @"SELECT ID as ""Id""
                        , NOME as ""Nome""
                        , IMAGEM_URL as ""ImagemUrl""
                        FROM CIDADES
                        WHERE ID = @Id";
            var resultado = await sql.QueryFirstOrDefaultAsync<Cidade>(query, param);
            return resultado;
        }

        public async Task<bool> UpdateCidade(int id, Cidade cidade)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Int32);
            param.Add("@Nome", cidade.Nome, DbType.String);
            param.Add("@ImagemUrl", cidade.ImagemUrl, DbType.String);
            var query = @"UPDATE CIDADES
                            SET NOME = @Nome,
                            IMAGEM_URL = @ImagemUrl
                            WHERE ID = @Id";
            var resultado = await sql.ExecuteAsync(query, param);
            return resultado > 0;
        }

        public async Task<bool> DeletarCidadeId(int id)
        {
            using IDbConnection sql = _conexao.Conectar;
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Int32);
            var query = @"DELETE FROM CIDADES
                            WHERE ID = @Id";
            var resultado = await sql.ExecuteAsync(query, param);
            return resultado > 0;
        }
    }
}
