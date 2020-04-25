using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BarberShop.Clientes.Dominio.Models.Request
{
    public class MarcarHorarioRequest
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ServicoId { get; set; }
        [Required]
        public DateTime HorarioMarcado { get; set; }
    }
}
