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
        public MMovimiento(int id, DateTime fecha, ETipoMovimiento tipo, decimal valor, decimal saldo)
        {
            this.Id= id;
            this.Tipo = tipo;
            this.Fecha = fecha;
            this.Saldo = saldo;
            this.Valor = valor;
        }
        public MMovimiento(DateTime fecha, ETipoMovimiento tipo, decimal valor, decimal saldo)
        {
            this.Tipo= tipo;
            this.Fecha= fecha;
            this.Saldo= saldo;
            this.Valor = valor;
        }

        public MMovimiento(ETipoMovimiento tipo, decimal valor)
        {            
            Tipo = tipo;
            Valor = valor;

        }

        public int Id { get;  set; }
        public DateTime Fecha { get; internal set; }
        public ETipoMovimiento Tipo{ get; internal set; }
        public decimal Valor { get; internal set; }
        public decimal Saldo { get; internal set; }

        internal bool isValid()
        {
            return (Tipo == ETipoMovimiento.Credito && Valor > 0)
                && (Tipo == ETipoMovimiento.Debito && Valor < 0)
                && (Valor != 0);
        }
    }
}
