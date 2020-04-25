using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Clientes.Dominio.Repositorios.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
