using AutoMapper;
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

        public async Task<MCliente> ObtenerCliente(int clienteId)
        {
            var cliente= await this.GetById(clienteId);
            MapperConfiguration config;
            config = new MapperConfiguration(cfg =>
            {
            });

            var mapper = new Mapper(config);
            var result = mapper.Map<MCliente>(cliente);
            return result;
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
            var movimientos = await DB.Movimientos
                .Where(m => m.Cuenta.ClienteId == clienteId && m.Fecha >= inicio && m.Fecha <= fin)
                .GroupBy(m => m.Cuenta.NumeroCuenta)
                .Select(g => new DEstadoCuenta()
                {
                    NumeroCuenta = g.Key,
                    Saldo = g.Sum(m => m.Valor),
                    Debitos = g.Count(m => m.Tipo == ETipoMovimiento.Debito),
                    Creditos = g.Count(m => m.Tipo == ETipoMovimiento.Credito)
                })
                .ToArrayAsync();
            return movimientos;
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
