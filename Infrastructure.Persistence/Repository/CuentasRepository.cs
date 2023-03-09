using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using CORE.Account.Interfaces;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Entity.Accounts;
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
