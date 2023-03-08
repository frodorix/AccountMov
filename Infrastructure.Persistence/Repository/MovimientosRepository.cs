using AutoMapper;
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
    internal class MovimientosRepository : Repository<Movimiento>, IMovimientosRepository
    {
        public MovimientosRepository(MyContext contex) : base(contex)
        {
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
