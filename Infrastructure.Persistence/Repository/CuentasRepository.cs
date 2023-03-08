using com.frodorix.bank;
using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using CORE.Account.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    internal class CuentasRepository : Repository<Cuenta>, ICuentasRepository
    {
        public CuentasRepository(MyContext contex) : base(contex)
        {
        }

        public async Task<bool> IsLimiteRetiroExedido(int numeroCuenta, DateTime now)
        {  
            
            
        }

        public async Task<MCuenta> ObtenerCuenta(int numeroCuenta)
        {
            return await DB.Cuenta
                .Where(x => x.NumeroCuenta == numeroCuenta && x.Cliente.Estado==EEstadoCliente.Activo)
                .Select(x => new MCuenta(x.NumeroCuenta, x.ClienteId, x.SaldoInicial, x.Estado))
                .FirstAsync();
        }

        public async Task<decimal> ObtenerSaldoCuenta(int numeroCuenta)
        {
            var saldo = await DB.Movimientos
                .Where(x => x.NumeroCuenta == numeroCuenta)
                .SumAsync(x => x.Valor);
            return saldo;

        }

        
    }
}
