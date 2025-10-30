using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;

namespace CORE.Account.Interfaces
{
    public interface IMovimientosRepository
    {
        Task<MMovimiento> RegistrarMovimiento(int numeroCuenta, DateTime fecha, ETipoMovimiento tipo, decimal valor, decimal saldo);
        Task<decimal> ObtenerTotalRetiros(int clienteId, DateTime now);
        #region ABM - CRUD
        Task<MMovimiento> Crear(MMovimiento movimiento);
        Task<int> Eliminar(int movimientoId);
        Task<int> Modificar(MMovimiento movimiento);
        Task<MMovimiento?> ObtenerPorId(int id);
        #endregion
    }
}
