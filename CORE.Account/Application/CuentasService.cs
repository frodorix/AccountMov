using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using CORE.Account.Exception;
using CORE.Account.Helpers;
using CORE.Account.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
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
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IDbContextTransaction transaction;

        public CuentasService(ICuentasRepository cuentasRespositor, IMovimientosRepository movimientosRespository, IClientesRepository clientesRespository, IDateTimeProvider dateTimeProvider,  IDbContextTransaction transaction)
        {
            this.cuentasRespository = cuentasRespositor;
            this.movimientosRespository = movimientosRespository;   
            this.clientesRespository = clientesRespository;
            this.dateTimeProvider = dateTimeProvider;
            this.transaction=transaction;
    }

        #region ABM
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        /// <exception cref="ClienteException"></exception>
        public async Task<MCuenta> Crear(MCuenta cuenta)
        {
            if (!cuenta.isValid())
                throw new CuentaException("Datos invalidos");            

            var nuevo = await cuentasRespository.Crear(cuenta);

            return nuevo;
        }

        public async Task<int> Eliminar(int clienteId)
        {
            int eliminados = await this.cuentasRespository.Eliminar(clienteId);
            return eliminados;
        }

        public async Task<int> Modificar(int id  , EEstadoCuenta estado)
        {
            return await this.cuentasRespository.Modificar(id, estado);
        }
        #endregion


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
 
            MMovimiento movimiento = await this.movimientosRespository.RegistrarMovimiento(cuenta.NumeroCuenta, fecha: dateTimeProvider.GetCurrentTime(), ETipoMovimiento.Credito, valorCredito, saldoActual + valorCredito);
            await this.transaction.CommitAsync();
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
            var valorDebito = Math.Abs(valor) ;
            decimal saldoActual = await this.cuentasRespository.ObtenerSaldoCuenta(cuenta.NumeroCuenta);
            #region validacion de saldo
            if (saldoActual - valorDebito < 0)//no se debe permitr debito en saldo 0
            {
                throw new CuentaException("Saldo no disponible");
            }
            decimal limite = await this.clientesRespository.ObtenerLimiteRetiro(cuenta.ClienteId);
            decimal totalRetiros = await this.movimientosRespository.ObtenerTotalRetiros(cuenta.ClienteId, dateTimeProvider.GetCurrentTime());
            if (totalRetiros + valorDebito > limite)
            {
                throw new CuentaException("Cupo diario Excedido");
            }
            #endregion
            MMovimiento movimiento = await this.movimientosRespository.RegistrarMovimiento(cuenta.NumeroCuenta, fecha: dateTimeProvider.GetCurrentTime(), ETipoMovimiento.Debito, valorDebito*-1, saldoActual - valorDebito);
            await this.transaction.CommitAsync();
            return movimiento;
        }

        public async Task<MCuenta> ObtenerPorId(int id)
        {
            return await this.cuentasRespository.ObtenerPorId(id);
        }
    }
}
