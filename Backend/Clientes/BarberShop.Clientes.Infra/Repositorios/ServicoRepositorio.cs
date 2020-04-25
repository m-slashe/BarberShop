using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using BarberShop.Clientes.Infra.Database.Context;

namespace BarberShop.Clientes.Infra.Repositorios
{
    public class ServicoRepositorio : Repositorio<Servico>, IServicoRepositorio
    {
        public ServicoRepositorio(BarberShopContext context) : base(context) { }
    }
}
