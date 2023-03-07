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
        Task<MCuenta> ObtenerCuenta(int numeroCuenta);
        Task<decimal> ObtenerSaldoCuenta(int numeroCuenta);
        Task<bool> IsLimiteRetiroExedido(int numeroCuenta, DateTime now);
        Task<MMovimiento> RegistrarMovimiento(int numeroCuenta, DateTime fecha, ETipoMovimiento credito, decimal valorCredito, decimal saldo);
    }
}
