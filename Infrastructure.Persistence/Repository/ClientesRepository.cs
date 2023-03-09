using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using CORE.Account.DTO;
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
    internal class ClientesRepository : Repository<Cliente>, IClientesRepository
    {
        public ClientesRepository(MyContext contex) : base(contex)
        {
        }

        public Task<MCliente> ObtenerCliente(int clienteId)
        {
            this.GetById(clienteId);
        }

        public async Task<IEnumerable<DCliente>> ObtenerClientes(string nombre)
        {
            var res = await this.DB
                .Clientes
                .Where(x => nombre.Contains(nombre))
                .Select(x => new DCliente(x.ClienteId, x.Nombre, x.Estado, x.Identificacion))
                .ToListAsync();
            return res;
        }

        public async Task<DEstadoCuenta[]> ObtenerEstadoCuenta(int clienteId, DateTime inicio, DateTime fin)
        {
            var resultado = await DB.Cuenta
                .Select(x => new DEstadoCuenta()
                {
                    NumeroCuenta = x.NumeroCuenta,
                    Saldo = x.Movimientos.Sum(m => m.Valor),
                    Debitos = x.Movimientos.Count(m => m.Tipo == ETipoMovimiento.Debito),
                    Creditos = x.Movimientos.Count(m => m.Tipo == ETipoMovimiento.Credito)
                })
                .ToArrayAsync();
            return resultado;
        }

        public async Task<decimal> ObtenerLimiteRetiro(int clienteId)
        {
            return await DB.Clientes
                .Where(x => x.ClienteId == clienteId)
                .Select(x => x.LimiteDiario)
                .FirstOrDefaultAsync();
        }
    }
}
