﻿using AutoMapper;
using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
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
    internal class MovimientosRepository : Repository<Movimiento>, IMovimientosRepository
    {
        public MovimientosRepository(MyContext contex) : base(contex)
        {
        }
        #region ABM - CRUD
        public async Task<MMovimiento> Crear(MMovimiento movimiento)
        {
            MapperConfiguration config;
            config = new MapperConfiguration(cfg => {

                cfg.CreateMap<MMovimiento, Movimiento>()
                    .ForMember(dest => dest.Cuenta, opt => opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    ;
            });
            var mapper = new Mapper(config);
            var entidad = mapper.Map<Movimiento>(movimiento);
            
           
            DB.Movimientos.Add(entidad);
            _ = await DB.SaveChangesAsync();
            movimiento.Id = entidad.Id;
            return movimiento;
           
        }

        public async Task<int> Eliminar(int movimientoId)
        {
            var entidad = await this.FindAsync(movimientoId);
            if (entidad == null)
            {
                throw new NotFoundException($"No existe la cuenta {movimientoId}");
            }
            this.Remove(entidad);
            var eliminados = await DB.SaveChangesAsync();
            return eliminados;
        }

        public async Task<int> Modificar(MMovimiento movimiento)
        {
            var entidad = await this.FindAsync(movimiento.Id);
            if (entidad == null)
                throw new NotFoundException($"No existe el movimiento {movimiento.Id}");

            entidad.Tipo = movimiento.Tipo;
            entidad.Saldo = movimiento.Saldo;
            entidad.Valor = movimiento.Valor;
            entidad.Fecha = movimiento.Fecha;
            entidad.NumeroCuenta = movimiento.NumeroCuenta;

            var modificados = await DB.SaveChangesAsync();
            return modificados;
        }

        public async Task<MMovimiento> ObtenerPorId(int id)
        {
            var res = await this.DB.Movimientos
                         .Where(x => x.Id == id)
                         .Select(x => new MMovimiento(x.Id,x.Fecha, x.Tipo, x.Valor, x.Saldo))
                         .FirstOrDefaultAsync();
            return res;
        }
        #endregion
        public async Task<decimal> ObtenerTotalRetiros(int clienteId, DateTime fecha)
        {
            decimal total = await  DB.Movimientos
                        .Where(m => m.Fecha.Date == fecha.Date)
                        .SumAsync(m => m.Valor);
            return total;
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
