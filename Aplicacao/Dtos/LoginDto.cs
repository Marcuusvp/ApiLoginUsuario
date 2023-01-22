using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Dtos
{
    public class LoginDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public IEnumerable<Permissoes> Permissoes { get; set; }
    }
}
