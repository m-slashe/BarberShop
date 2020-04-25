using BarberShop.Clientes.Infra.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Clientes.Infra.Database.Context
{
    public class BarberShopContext: DbContext
    {
        public BarberShopContext(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new HorarioConfiguration());
            modelBuilder.ApplyConfiguration(new ServicoConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
