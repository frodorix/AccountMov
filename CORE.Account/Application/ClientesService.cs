using CORE.Account.Application.Interfaces;
using CORE.Account.Application.Services;
using CORE.Account.Domain.Model;
using CORE.Account.DTO;
using CORE.Account.Exception;
using CORE.Account.Helpers;
using CORE.Account.Interfaces;

namespace CORE.Account.Application
{
    internal class ClientesService : IClientesService 
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IClientesRepository clientesRespository;
        private readonly IPasswordHashingService passwordHashingService;

        public ClientesService(
            IClientesRepository clientesRespository, 
            IDateTimeProvider dateTimeProvider,
            IPasswordHashingService passwordHashingService)
        {
            this.clientesRespository = clientesRespository;
            this.dateTimeProvider = dateTimeProvider;
            this.passwordHashingService = passwordHashingService;
        }

        #region ABM
        /// <summary>
        /// Creates a new client with hashed password.
        /// </summary>
        /// <param name="cliente">Client data</param>
        /// <returns>Created client</returns>
        /// <exception cref="ClienteException">Thrown when client data is invalid</exception>
        public async Task<MCliente> Crear(MCliente cliente)
        {
            if (!cliente.isValid())
                throw new ClienteException("Datos invalidos");

            cliente.Contrasena = passwordHashingService.HashPassword(cliente.Contrasena);

            var nuevo = await clientesRespository.Crear(cliente);

            return nuevo;
        }

        public async Task<int> Eliminar(int clienteId)
        {
            int eliminados = await this.clientesRespository.Eliminar(clienteId);
            return eliminados;
        }

        public async Task<int> Modificar(MCliente obj)
        {
            return await this.clientesRespository.Modificar(obj);
        }
        #endregion

        public async Task<IEnumerable<DCliente>> ObtenerClientes(string nombre)
        {
            return await this.clientesRespository.ObtenerClientes(nombre);
        }

        public async Task<DEstadoCuenta[]> ObtenerEstadoCuenta(int clienteId, DateTime inicio, DateTime fin)
        {                        
            DEstadoCuenta[] estadoCuenta = await this.clientesRespository.ObtenerEstadoCuenta(clienteId, inicio, fin);
            return estadoCuenta;
        }

        public async Task<MCliente?> ObtenerPorId(int id)
        {
            return await this.clientesRespository.ObtenerPorId(id);
        }
    }
}
