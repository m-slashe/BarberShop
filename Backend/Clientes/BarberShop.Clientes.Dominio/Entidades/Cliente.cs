using System;
using System.Collections.Generic;

namespace BarberShop.Clientes.Dominio.Entidades
{
    public class Cliente: Entity
    {
        public string Nome { get; protected set; }
        public IEnumerable<Horario> Horarios { get; set; }

        public Cliente() { }

        public Cliente(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                throw new Exception("Nome é obrigatório");

            Nome = nome;
        }
    }
}
