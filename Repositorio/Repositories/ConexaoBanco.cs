using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Repositorio.Repositories
{
    public class ConexaoBanco
    {
        private readonly string _conexaoB;
        private readonly IConfiguration _configuration;

        public ConexaoBanco(IConfiguration configuration)
        {
            _configuration = configuration;
            _conexaoB = _configuration.GetConnectionString("ConexaoApi");
        }

        public IDbConnection Conectar
        {
            get { return new SqlConnection(_conexaoB); }
        }
    }
}
