using Dominio.Mensagem;

namespace Dominio.RetornoApi
{
    public class RetornoApi<T>
    {
        public T Retorno { get; private set; }
        public IEnumerable<string> Erros { get; set; }
        public bool ContemErro { get; }

        public RetornoApi(T retorno, IMensagem mensagem)
        {
            Retorno = retorno;
            Erros = mensagem.BuscarErros();
            ContemErro = mensagem.PossuiErros;
        }
    }
}
