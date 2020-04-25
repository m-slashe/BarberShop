using BarberShop.Clientes.Dominio.Enums;
using System;

namespace BarberShop.Clientes.Dominio.Entidades
{
    public class Horario: Entity
    {
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public DateTime HorarioMarcado { get; set; }
        public StatusHorario Status { get; set; }
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }
        public Horario() { }
        public Horario(Cliente cliente, Servico servico, DateTime horarioMarcado)
        {
            if(cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            if(servico == null)
            {
                throw new Exception("Serviço não encontrado");
            }

            ClienteId = cliente.Id;
            ServicoId = cliente.Id;
            HorarioMarcado = horarioMarcado;
            Status = StatusHorario.Agendado;
        }  
        
        public void Cancelar()
        {
            Status = StatusHorario.Cancelado;
        }
    }
}
