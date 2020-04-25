using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Clientes.Aplicacao.Models.Request
{
    public class AgendarRequest
    {
        [Required]
        [RegularExpression(@"^\d{2}\/\d{2}\/\d{4}$")]
        public string DataInicio { get; set; }
        [Required]
        [RegularExpression(@"^\d{2}\/\d{2}\/\d{4}$")]
        public string DataFim { get; set; }
    }
}
