using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mensagem
{
    public interface IMensagem
    {
        bool PossuiErros { get; }
        void AdicionaErro(string mensagem);
        List<string> BuscarErros();
    }
}
