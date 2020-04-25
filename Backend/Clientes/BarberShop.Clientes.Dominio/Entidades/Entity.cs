using System;
using System.Collections.Generic;
using System.Text;

namespace BarberShop.Clientes.Dominio.Entidades
{
    public class Entity
    {
        public int Id { get; set; }
        public bool IsNew => Id == 0;
    }
}
