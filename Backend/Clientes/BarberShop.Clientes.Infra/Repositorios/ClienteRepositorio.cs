using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using BarberShop.Clientes.Infra.Database.Context;
using System;
using System.Collections.Generic;

namespace BarberShop.Clientes.Infra.Repositorios
{
    public class ClienteRepositorio: Repositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(BarberShopContext context): base(context) { }

    }
}
