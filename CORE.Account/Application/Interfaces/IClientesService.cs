using CORE.Account.Domain.Model;
using CORE.Account.DTO;

namespace CORE.Account.Application.Interfaces
{
    public interface IClientesService
    {
        Task<DEstadoCuenta[]> ObtenerEstadoCuenta(int clienteId, DateTime inicio, DateTime fin);
        Task<IEnumerable<DCliente>> ObtenerClientes(string nombre);

        #region ABM
        Task<MCliente> Crear(MCliente cliente);
        Task<int> Modificar(MCliente cliente);
        Task<int> Eliminar(int id);
        Task<MCliente?> ObtenerPorId(int id);
        #endregion
    }
}