using BarberShop.Clientes.Dominio.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Clientes.API.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> erros = new List<string>();
                erros.AddRange(context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
                context.Result = new BadRequestObjectResult(new RequestResponse(erros));
            }

        }
    }
}
