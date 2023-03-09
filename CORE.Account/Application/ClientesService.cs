using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Model;
using CORE.Account.DTO;
using CORE.Account.Helpers;
using CORE.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Application
{
    internal class ClientesService : IClientesService
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IClientesRepository clientesRespository;
        public ClientesService(IClientesRepository clientesRespository, IDateTimeProvider dateTimeProvider)
        {
            this.clientesRespository = clientesRespository;
            this.dateTimeProvider = dateTimeProvider;   
        }

        public async Task<IEnumerable<DCliente>> ObtenerClientes(string nombre)
        {
            return await this.clientesRespository.ObtenerClientes(nombre);
        }

        public async Task<DCliente> ObtenerEstadoCuenta(int clienteId, DateTime inicio, DateTime fin)
        {
            MCliente cliente = await this.clientesRespository.ObtenerCliente(clienteId);
            DEstadoCuenta[] estadoCuenta = await this.clientesRespository.ObtenerEstadoCuenta(clienteId, inicio, fin);
            var dto = new DCliente(clienteId: cliente.Id, nombre : cliente.Nombre, estado : cliente.Estado, identificacion : cliente.Identificacion,estadoCuenta : estadoCuenta  );
            return dto;
        }

       
    }
}
