using CORE.Account.Domain.Model;
using CORE.Account.DTO;

namespace CORE.Account.Interfaces
{
    public interface IClientesRepository
    {
        Task<MCliente> ObtenerCliente(int clienteId);
        Task<DEstadoCuenta[]> ObtenerEstadoCuenta(int clienteId, DateTime inicio, DateTime fin);
        Task<decimal> ObtenerLimiteRetiro(int clienteId);
        Task<IEnumerable<DCliente>> ObtenerClientes(string nombre);

        #region ABM
        Task<MCliente> Crear(MCliente cliente);
        Task<int> Modificar(MCliente cliente);
        Task<int> Eliminar(int clienteId);
        Task<MCliente?> ObtenerPorId(int id);
        #endregion
    }
}
