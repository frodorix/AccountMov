using CORE.Account.Domain.Model;

namespace CORE.Account.Application.Interfaces
{
    public interface IMovimientosService
    {
        #region ABM
        Task<MMovimiento> Crear(MMovimiento obj);
        Task<int> Modificar(MMovimiento obj);
        Task<int> Eliminar(int id);
        Task<MMovimiento> ObtenerPorId(int id);
        #endregion
    }
}