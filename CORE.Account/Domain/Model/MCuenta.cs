using CORE.Account.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Domain.Model
{
    public class MCuenta
    {
        private int? clienteId;
        private int estado;

        public MCuenta(int numeroCuenta, int? clienteId, decimal saldoInicial, int estado)
        {
            NumeroCuenta = numeroCuenta;
            this.clienteId = clienteId;
            SaldoInicial = saldoInicial;
            this.estado = estado;
        }

        public int NumeroCuenta { get;internal set; }
        public ETipoCuenta Tipo { get; internal set; }
        public decimal SaldoInicial { get; internal set; }
        public EstadoCuenta Estado { get; set; }
        public int ClienteId { get; set; }

    }
}
