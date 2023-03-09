using CORE.Account.Domain.Model;
using CORE.Account.DTO;

namespace CORE.Account.Application.Interfaces
{
    public interface IClientesService
    {
        Task<DCliente> ObtenerEstadoCuenta(int clienteId, DateTime inicio, DateTime fin);
        Task<IEnumerable<DCliente>> ObtenerClientes(string nombre);
    }
}