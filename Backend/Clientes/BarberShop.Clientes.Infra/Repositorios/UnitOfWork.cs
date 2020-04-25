using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using BarberShop.Clientes.Infra.Database.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Clientes.Infra.Repositorios
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BarberShopContext _context;
        public UnitOfWork(BarberShopContext context) 
        {
            _context = context;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
