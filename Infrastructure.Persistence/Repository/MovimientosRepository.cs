using AutoMapper;
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
    internal class MovimientosRepository : Repository<Movimiento>, IMovimientosRepository
    {
        public MovimientosRepository(MyContext contex) : base(contex)
        {
        }

        public Task<decimal> ObtenerTotalRetiros(int clienteId, DateTime now)
        {
            throw new NotImplementedException();
        }

        public async Task<MMovimiento> RegistrarMovimiento(int numeroCuenta, DateTime fecha, ETipoMovimiento tipo, decimal valor, decimal saldo)
        {
            var movimiento = new Movimiento(numeroCuenta, fecha, tipo, valor, saldo);
            this.Add(movimiento);
            await DB.SaveChangesAsync();

            MapperConfiguration config;
            config = new MapperConfiguration(cfg =>
            {
            });

            var mapper = new Mapper(config);
            var result = mapper.Map<MMovimiento>(movimiento);

            return result;
        }
    }
}
