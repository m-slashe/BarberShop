using BarberShop.Clientes.Aplicacao.Models.Request;
using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberShop.Clientes.API.Controllers
{
    [Route("api/servicos")]
    [ApiController]
    public class ServicosController
    {
        private readonly IServicoRepositorio _servicoRepositorio;
        private readonly IUnitOfWork _unitOfWork;
        public ServicosController(IServicoRepositorio servicoRepositorio, IUnitOfWork unitOfWork)
        {
            _servicoRepositorio = servicoRepositorio;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<Servico> CriarServico(CriarServicoRequest request)
        {
            Servico servico = new Servico(request.Nome, request.Valor);
            _servicoRepositorio.Salvar(servico);
            await _unitOfWork.SaveChanges();
            return servico;
        }

        [HttpGet]
        public async Task<IEnumerable<Servico>> ObterTodos()
        {
            return await _servicoRepositorio.Obter();
        }
    }
}
