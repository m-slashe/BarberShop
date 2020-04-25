using BarberShop.Clientes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Clientes.Dominio.Repositorios.Interfaces
{
    public interface IHorarioRepositorio : IRepositorio<Horario>
    {
        Task<IEnumerable<Horario>> ObterAgenda(DateTime dataInicio, DateTime dataFim);
    }
}
