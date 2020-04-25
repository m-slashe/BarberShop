using BarberShop.Clientes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Clientes.IntegrationTestes.Mocks
{
    public class ClienteMock
    {
        public static IEnumerable<Cliente> ObterClientesMock()
        {
            return new List<Cliente>()
            {
                new Cliente("Cliente 1"),
                new Cliente("Cliente 2"),
                new Cliente("Cliente 3")
            };
        }
    }
}
