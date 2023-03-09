using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using CORE.Account.Exception;
using CORE.Account.Interfaces;

namespace CORE.Account.Application
{
    internal class MovimientosService : IMovimientosService
    {
        public readonly IMovimientosRepository movimientosRepository;
        public MovimientosService(IMovimientosRepository movimientosRepository)
        {
            this.movimientosRepository = movimientosRepository;
        }

        #region ABM
     
        public async Task<MMovimiento> Crear(MMovimiento movimiento)
        {
            if (!movimiento.isValid())
                throw new MovimientoException("Datos invalidos");

            var nuevo = await movimientosRepository.Crear(movimiento);

            return nuevo;
        }

        public async Task<int> Eliminar(int movimientoId)
        {
            int eliminados = await this.movimientosRepository.Eliminar(movimientoId);
            return eliminados;
        }

        public async Task<int> Modificar(MMovimiento movimiento)
        {
            return await this.movimientosRepository.Modificar(movimiento);
        }

        public async Task<MMovimiento> ObtenerPorId(int id)
        {
            return await this.movimientosRepository.ObtenerPorId(id);
        }
        #endregion

    }
}
