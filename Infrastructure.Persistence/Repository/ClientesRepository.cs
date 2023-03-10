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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    internal class ClientesRepository : Repository<Cliente>, IClientesRepository
    {
        public ClientesRepository(MyContext contex) : base(contex)
        {
        }
        #region ABM
        public async Task<MCliente> Crear(MCliente cliente)
        {
            MapperConfiguration config;
            config = new MapperConfiguration(cfg =>{
            
                cfg.CreateMap<MCliente, Cliente>()
                    .ForMember(dest => dest.ClienteId, opt => opt.Ignore()); ;
            });
            var mapper = new Mapper(config);
            var entidad = mapper.Map<Cliente>(cliente);
            DB.Clientes.Add(entidad);
            _ = await DB.SaveChangesAsync();
            cliente.Id = entidad.ClienteId;
            return cliente;
        }

        public async Task<int> Eliminar(int clienteId)
        {
            var entidad = await this.FindAsync(clienteId);
            if(entidad == null)
            {
                throw new NotFoundException($"No existe el cliente {clienteId}");
            }
            this.Remove(entidad);
            var eliminados = await DB.SaveChangesAsync();
            return eliminados;

        }

        public async Task<int> Modificar(MCliente cliente)
        {
            var entidad = await this.FindAsync(cliente.Id);
            if (entidad==null)
                throw new NotFoundException($"CLiente {cliente.Id} no enconrtado");
            
            entidad.Nombre=cliente.Nombre;
            entidad.Identificacion=cliente.Identificacion;
            entidad.Edad = cliente.Edad;
            entidad.Direccion = cliente.Direccion;
            entidad.Telefono = cliente.Telefono;
            
            var modificados = await DB.SaveChangesAsync();
            return modificados;
        }
        #endregion
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
                .Where(x => x.Nombre.Contains(nombre))
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

        public async Task<MCliente> ObtenerPorId(int id)
        {
             var res = await this.DB.Clientes
                .Where(x => x.ClienteId == id)
                .Select(x=> new MCliente(x.ClienteId,x.Nombre,x.Genero,x.Edad,x.Identificacion,x.Direccion,x.Telefono,"",x.Estado))
                .FirstOrDefaultAsync();
            return res;
        }
    }
}
