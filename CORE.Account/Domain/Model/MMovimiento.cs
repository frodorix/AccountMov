using CORE.Account.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Domain.Model
{
    public class MMovimiento
    {
        public MMovimiento(DateTime fecha, ETipoMovimiento tipo, decimal valor, decimal saldo)
        {
            this.Tipo= tipo;
            this.Fecha= fecha;
            this.Saldo= saldo;
            this.Valor = valor;
        }
        public DateTime Fecha { get; internal set; }
        public ETipoMovimiento Tipo{ get; internal set; }
        public decimal Valor { get; internal set; }
        public decimal Saldo { get; internal set; } 
    }
}
