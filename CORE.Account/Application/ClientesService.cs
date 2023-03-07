using CORE.Account.Domain.Model;
using CORE.Account.DTO;
using CORE.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Application
{
    internal class ClientesService
    {
        private readonly IClientesRepository clientesRespository;
        public ClientesService(IClientesRepository clientesRespository) { 
            this.clientesRespository = clientesRespository;
        }
        public async Task<DCliente> ObtenerEstadoCuenta(int clienteId, DateTime inicio, DateTime fin)
        {
            MCliente cliente = await this.clientesRespository.ObtenerCliente(clienteId);
            DEstadoCuenta[] estadoCuenta = await this.clientesRespository.ObtenerEstadoCuenta(clienteId, inicio, fin);
            return new DCliente(cliente.Id, cliente.Nombre, cliente.Estado, estadoCuenta);
        }
    }
}
