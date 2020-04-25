using BarberShop.Clientes.Aplicacao.Models.Request;
using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Models.Request;
using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using BarberShop.Clientes.Dominio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberShop.Clientes.API.Controllers
{
    [Route("api/horarios")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly IHorariosService _horarioService;
        private readonly IHorarioRepositorio _horarioRepositorio;
        private readonly IUnitOfWork _unitOfWork;
        public HorariosController(IHorariosService horarioService, 
                                  IUnitOfWork unitOfWork,
                                  IHorarioRepositorio horarioRepositorio)
        {
            _horarioService = horarioService;
            _horarioRepositorio = horarioRepositorio;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("marcar")]
        public async Task<Horario> MarcarHorario([FromBody] MarcarHorarioRequest request)
        {
            Horario horario = await _horarioService.MarcarHorario(request);
            await _unitOfWork.SaveChanges();
            return horario;
        }

        [HttpGet("agenda")]
        public Task<IEnumerable<Horario>> ObterAgenda([FromQuery] AgendarRequest request)
        {
            string[] dataInicioSplit = request.DataInicio.Split("/");
            string[] dataFimSplit = request.DataFim.Split("/");
            DateTime dateTimeInicio = new DateTime(Convert.ToInt16(dataInicioSplit[2]), Convert.ToInt16(dataInicioSplit[1]), Convert.ToInt16(dataInicioSplit[0]), 0, 0, 0);
            DateTime dateTimeFim = new DateTime(Convert.ToInt16(dataFimSplit[2]), Convert.ToInt16(dataFimSplit[1]), Convert.ToInt16(dataFimSplit[0]), 23, 59, 59);
            return _horarioRepositorio.ObterAgenda(dateTimeInicio, dateTimeFim);
        }
    }
}
