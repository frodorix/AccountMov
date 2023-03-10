using AutoMapper;
using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using CORE.Account.DTO;
using CORE.Account.Exception;
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

        #region ABM - CRUD
        public async Task<MCuenta> Crear(MCuenta cuenta)
        {
            MapperConfiguration config;
            config = new MapperConfiguration(cfg => {

                cfg.CreateMap<MCuenta, Cuenta>()
                    .ForMember(dest => dest.NumeroCuenta, opt => opt.Ignore()); ;
            });
            var mapper = new Mapper(config);
            var entidad = mapper.Map<Cuenta>(cuenta);

            DB.Cuenta.Add(entidad);
            _ = await DB.SaveChangesAsync();
            cuenta.NumeroCuenta = entidad.NumeroCuenta;
            return cuenta;
        }

        public async Task<int> Eliminar(int id)
        {
            var entidad = await this.FindAsync(id);
            if(entidad == null)
            {
                throw new NotFoundException($"No existe la cuenta {id}");
            }
            this.Remove(entidad);
            var eliminados = await DB.SaveChangesAsync();
            return eliminados;
        }

        public async Task<int> Modificar(int numeroCuenta, EEstadoCuenta estado)
        {
            var entidad = await this.FindAsync(numeroCuenta);
            if (entidad == null)
                throw new NotFoundException($"No existe la cuenta {numeroCuenta}");

            entidad.Estado=estado;

            var modificados = await DB.SaveChangesAsync();
            return modificados;
        }

        #endregion
        public async Task<MCuenta> ObtenerCuenta(int numeroCuenta)
        {
            return await DB.Cuenta
                .Where(x => x.NumeroCuenta == numeroCuenta && x.Cliente.Estado==EEstadoCliente.Activo)
                .Select(x => new MCuenta(x.NumeroCuenta, x.ClienteId, x.SaldoInicial, x.Estado))
                .FirstAsync();
        }

        public async Task<MCuenta> ObtenerPorId(int numeroCuenta)
        {
            var res = await this.DB.Cuenta
                          .Where(x => x.NumeroCuenta== numeroCuenta)
                          .Select(x => new MCuenta(x.NumeroCuenta,x.ClienteId,x.SaldoInicial,x.Estado))
                          .FirstOrDefaultAsync();
            return res;
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
