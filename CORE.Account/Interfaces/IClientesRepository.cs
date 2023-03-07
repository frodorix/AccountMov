using CORE.Account.Domain.Model;
using CORE.Account.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Interfaces
{
    public interface IClientesRepository
    {
        Task<MCliente> ObtenerCliente(int clienteId);
        Task<DEstadoCuenta[]> ObtenerEstadoCuenta(int clienteId, DateTime inicio, DateTime fin);
    }
}
