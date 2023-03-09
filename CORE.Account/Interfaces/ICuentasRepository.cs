using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Interfaces
{
    public interface ICuentasRepository
    {
        #region ABM         
        Task<MCuenta> Crear(MCuenta cuenta);
        Task<int> Eliminar(int clienteId);
        Task<int> Modificar(int id, EEstadoCuenta estado);
        #endregion
        Task<MCuenta> ObtenerCuenta(int numeroCuenta);
        Task<MCuenta> ObtenerPorId(int numeroCuenta);
        Task<decimal> ObtenerSaldoCuenta(int numeroCuenta);

      
    }
}
