using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IImagemService
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}
