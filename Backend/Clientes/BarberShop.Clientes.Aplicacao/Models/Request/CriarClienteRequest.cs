using System.ComponentModel.DataAnnotations;

namespace BarberShop.Clientes.Aplicacao.Models.Request
{
    public class CriarClienteRequest
    {
        [Required]
        public string Nome { get; set; }
    }
}
