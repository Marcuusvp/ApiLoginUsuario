using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Dtos
{
    public class NovaCidadeDto
    {
        public string Nome { get; set; }
        public string? ImagemUrl { get; set; }
        public IFormFile? Imagem { get; set; }
    }
}
