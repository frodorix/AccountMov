using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using CORE.Account.Exception;
using CORE.Account.Interfaces;

namespace CORE.Account.Application.Interfaces
{
    public interface ICuentasService
    {
        Task<MMovimiento> RegistrarCredito(int numeroCuenta, decimal valor);
        Task<MMovimiento> RegistrarDebito(int numeroCuenta, decimal valor);

        #region ABM
        Task<MCuenta> Crear(MCuenta cuenta);
        Task<int> Eliminar(int clienteId);
        Task<int> Modificar(int id , EEstadoCuenta estado);
        Task<MCuenta> ObtenerPorId(int id);
        #endregion
    }


}