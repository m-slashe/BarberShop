using BarberShop.Clientes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberShop.Clientes.Dominio.Repositorios.Interfaces
{
    public interface IRepositorio<T> where T: Entity
    {
        T Salvar(T dado);
        Task<T> Atualizar(T dado);
        Task Deletar(int id);
        Task<IEnumerable<T>> Obter();
        Task<T> Obter(int id);
    }
}
