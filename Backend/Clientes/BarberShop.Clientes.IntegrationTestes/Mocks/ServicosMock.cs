using BarberShop.Clientes.Dominio.Entidades;
using System.Collections.Generic;

namespace BarberShop.Clientes.IntegrationTestes.Mocks
{
    public class ServicosMock
    {
        public static IEnumerable<Servico> ObterServicosMock()
        {
            return new List<Servico>()
            {
                new Servico("Serviço 1", 10),
                new Servico("Serviço 2", 15),
                new Servico("Serviço 3", 20),
            };
        }
    }
}
