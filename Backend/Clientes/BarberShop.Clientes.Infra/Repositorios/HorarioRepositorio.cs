using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using BarberShop.Clientes.Infra.Database.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Clientes.Infra.Repositorios
{
    public class HorarioRepositorio : Repositorio<Horario>, IHorarioRepositorio
    {
        public HorarioRepositorio(BarberShopContext context) : base(context) { }

        public async Task<IEnumerable<Horario>> ObterAgenda(DateTime dataInicio, DateTime dataFim)
        {
            return await DbSet.AsNoTracking().Where(x => x.HorarioMarcado >= dataInicio && x.HorarioMarcado <= dataFim).ToListAsync();
        }
    }
}
