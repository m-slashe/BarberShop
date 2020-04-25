using BarberShop.Clientes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarberShop.Clientes.IntegrationTestes.Mocks
{
    public class HorarioMock
    {
        public static IEnumerable<Horario> ObterAgendaMock()
        {
            IEnumerable<Cliente> clientes = ClienteMock.ObterClientesMock();
            IEnumerable<Servico> servicos = ServicosMock.ObterServicosMock();
            return new List<Horario>()
            {
                new Horario(clientes.First(), servicos.First(), new DateTime(2019, 1, 1, 12, 30, 0)),
                new Horario(clientes.First(), servicos.First(), new DateTime(2019, 1, 1, 13, 00, 0)),
                new Horario(clientes.First(), servicos.First(), new DateTime(2019, 1, 1, 13, 30, 0)),
                new Horario(clientes.First(), servicos.First(), new DateTime(2019, 1, 1, 14, 00, 0)),
                new Horario(clientes.First(), servicos.First(), new DateTime(2019, 1, 1, 14, 30, 0))
            };
        }
    }
}
