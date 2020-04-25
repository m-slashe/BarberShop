using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using BarberShop.Clientes.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Clientes.Infra.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : Entity
    {
        protected readonly BarberShopContext _context;
        protected readonly DbSet<T> DbSet;
        public Repositorio(BarberShopContext context) 
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public Task<T> Atualizar(T dado)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Obter()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> Obter(int id)
        {
            return await DbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public T Salvar(T dado)
        {
            if (dado.IsNew)
                DbSet.Add(dado);
            else
                DbSet.Update(dado);

            return dado;
        }
    }
}
