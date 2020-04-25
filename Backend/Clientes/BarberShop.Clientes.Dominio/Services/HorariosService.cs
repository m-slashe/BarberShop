using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Models.Request;
using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using BarberShop.Clientes.Dominio.Services.Interfaces;
using System.Threading.Tasks;

namespace BarberShop.Clientes.Dominio.Services
{
    public class HorariosService : IHorariosService
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IHorarioRepositorio _horarioRepositorio;
        private readonly IServicoRepositorio _servicoRepositorio;
        public HorariosService(IClienteRepositorio clienteRepositorio, 
                               IHorarioRepositorio horarioRepositorio,
                               IServicoRepositorio servicoRepositorio) 
        {
            _clienteRepositorio = clienteRepositorio;
            _horarioRepositorio = horarioRepositorio;
            _servicoRepositorio = servicoRepositorio;
        }
        public async Task<Horario> MarcarHorario(MarcarHorarioRequest request)
        {
            Cliente cliente = await _clienteRepositorio.Obter(request.ClientId);
            Servico servico = await _servicoRepositorio.Obter(request.ServicoId);
            Horario horarioMarcado = new Horario(cliente, servico, request.HorarioMarcado);
            return _horarioRepositorio.Salvar(horarioMarcado);
        }
    }
}
