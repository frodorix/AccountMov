using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using CORE.Account.Exception;
using CORE.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Application
{
    public  class CuentasService : ICuentasService
    {

        private readonly ICuentasRepository cuentasRespository;
        private readonly IMovimientosRepository movimientosRespository;
        private readonly IClientesRepository clientesRespository;
        public CuentasService(ICuentasRepository cuentasRespositor, IMovimientosRepository movimientosRespository, IClientesRepository clientesRespository)
        {
            this.cuentasRespository = cuentasRespositor;
            this.movimientosRespository = movimientosRespository;   
            this.clientesRespository = clientesRespository;
        }
        /// <summary>
        /// valida el estado de una cuenta
        /// </summary>
        /// <param name="numeroCuenta"></param>
        /// <returns></returns>
        /// <exception cref="CuentaException"></exception>
        private async Task<MCuenta?> ValidarEstadoCuenta(int numeroCuenta)
        {
            MCuenta cuenta = await this.cuentasRespository.ObtenerCuenta(numeroCuenta);
            #region validacion de estado de cuenta
            if (cuenta == null)
            {
                throw new CuentaException($"Cuenta {numeroCuenta} no existe");
            }
            else if (cuenta.Estado == EEstadoCuenta.Inactivo)
            {
                throw new CuentaException($"Cuenta {numeroCuenta} Inactiva");
            }
            #endregion
            return cuenta;
        }

        /// <summary>
        /// Al ser un CREDITO, el signo del valor es procesado internamente, siempre como POSITIVO.
        /// </summary>
        /// <param name="numeroCuenta"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        /// <exception cref="CuentaException"></exception>
        public async Task<MMovimiento> RegistrarCredito(int numeroCuenta, decimal valor)
        {
            var cuenta = await this.ValidarEstadoCuenta(numeroCuenta);
            var valorCredito = Math.Abs(valor);
            decimal saldoActual = await this.cuentasRespository.ObtenerSaldoCuenta(numeroCuenta);
            MMovimiento movimiento = await this.movimientosRespository.RegistrarMovimiento(cuenta.NumeroCuenta, fecha: DateTime.Now, ETipoMovimiento.Credito, valorCredito, saldoActual + valorCredito);
            return movimiento;
        }

        /// <summary>
        /// Si el saldo es 0 genera una excepcion de "Saldo no disponible".
        /// Al ser un DEBITO, el signo del valor es procesado internamente, siempre como negativo.
        /// </summary>
        /// <param name="numeroCuenta"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        /// <exception cref="CuentaException"></exception>
        public async Task<MMovimiento> RegistrarDebito(int numeroCuenta, decimal valor)
        {
            var cuenta = await this.ValidarEstadoCuenta(numeroCuenta);
            var valorDebito = Math.Abs(valor) * -1;
            decimal saldoActual = await this.cuentasRespository.ObtenerSaldoCuenta(cuenta.NumeroCuenta);
            #region validacion de saldo
            if (saldoActual - valorDebito < 0)//no se debe permitr debito en saldo 0
            {
                throw new CuentaException("Saldo no disponible");
            }
            decimal limite = await this.clientesRespository.ObtenerLimiteRetiro(cuenta.ClienteId);
            decimal totalRetiros = await this.movimientosRespository.ObtenerTotalRetiros(cuenta.ClienteId, DateTime.Now);
            if (totalRetiros + valorDebito > limite)
            {
                throw new CuentaException("Cupo diario Excedido");
            }
            #endregion
            MMovimiento movimiento = await this.movimientosRespository.RegistrarMovimiento(cuenta.NumeroCuenta, fecha: DateTime.Now, ETipoMovimiento.Debito, valorDebito, saldoActual - valorDebito);
            return movimiento;
        }
    }
}
