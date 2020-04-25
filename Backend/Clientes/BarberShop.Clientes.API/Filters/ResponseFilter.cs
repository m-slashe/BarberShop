using BarberShop.Clientes.Dominio.Models.Response;
using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BarberShop.Clientes.API.Filters
{
    public class ResponseFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) 
        {
            var objectContent = context.Result as ObjectResult;
            if (objectContent != null)
            {
                context.Result = new OkObjectResult(new RequestResponse<object>(objectContent.Value));
            }
            if(context.Exception != null)
            {
                context.Result = new ObjectResult(new RequestResponse<object>(new List<string>() { context.Exception.Message }, "Um erro inesperado ocorreu!!!"));
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
