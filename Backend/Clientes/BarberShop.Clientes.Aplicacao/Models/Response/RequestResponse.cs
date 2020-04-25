using System;
using System.Collections.Generic;
using System.Text;

namespace BarberShop.Clientes.Dominio.Models.Response
{
    public class RequestResponse<T>
    {
        public T Dado { get; set; }
        public string Mensagem { get; set; }
        public IEnumerable<string> Erros { get; set; }
        public RequestResponse() { }
        public RequestResponse(T result)
        {
            Dado = result;
            Erros = new List<string>();
        }

        public RequestResponse(IEnumerable<string> erros)
        {
            Erros = erros;
        }

        public RequestResponse(IEnumerable<string> erros, string mensagem)
        {
            Erros = erros;
            Mensagem = mensagem;
        }
    }

    public class RequestResponse: RequestResponse<object> 
    {
        public RequestResponse(IEnumerable<string> erros): base(erros) { }
    }
}
