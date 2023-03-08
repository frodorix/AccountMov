using com.frodorix.bank;
using CORE.Account.Domain.Model;
using CORE.Account.DTO;
using CORE.Account.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    internal class ClientesRepository : IClientesRepository
    {
        protected readonly MyContext _dbContext;
        public ClientesRepository(DbContext contex)
        {
            _dbContext = (MyContext)contex;
        }
        public Task<MCliente> ObtenerCliente(int clienteId)
        {
            throw new NotImplementedException();
        }

        public Task<DEstadoCuenta[]> ObtenerEstadoCuenta(int clienteId, DateTime inicio, DateTime fin)
        {
            throw new NotImplementedException();
        }
    }
}
