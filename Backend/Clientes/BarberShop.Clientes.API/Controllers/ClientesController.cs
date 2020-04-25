using BarberShop.Clientes.Aplicacao.Models.Request;
using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberShop.Clientes.API.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController: ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IUnitOfWork _unitOfWork;
        public ClientesController(IClienteRepositorio clienteRepositorio, IUnitOfWork unitOfWork)
        {
            _clienteRepositorio = clienteRepositorio;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            return await _clienteRepositorio.Obter();
        }

        [HttpPost]
        public Cliente CriarCliente(CriarClienteRequest request)
        {
            Cliente cliente = new Cliente(request.Nome);
            _clienteRepositorio.Salvar(cliente);
            _unitOfWork.SaveChanges();
            return cliente;
        }
    }
}
