using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Clientes.Dominio.Services.Interfaces
{
    public interface IHorariosService
    {
        Task<Horario> MarcarHorario(MarcarHorarioRequest request);
    }
}
