using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    [Table("CLIENTES")]
    public class Pessoas
    {
        [Description("ID")]
        public int Id { get; set; }
        [Description("EMAIL")]
        public string? Email { get; set; }
        [Description("NOME_COMPLETO")]
        public string? NomeCompleto { get; set; }
        [Description("CIDADE_ID")]
        public int CidadeId { get; set; }
    }
}
