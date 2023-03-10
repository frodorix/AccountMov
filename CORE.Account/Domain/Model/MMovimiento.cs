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
        public MMovimiento(int numeroCuenta,int id, DateTime fecha, ETipoMovimiento tipo, decimal valor, decimal saldo)
        {
            this.NumeroCuenta = numeroCuenta;
            this.Id= id;
            this.Tipo = tipo;
            this.Fecha = fecha;
            this.Saldo = saldo;
            this.Valor = valor;
        }
        public MMovimiento(int numeroCuenta, DateTime fecha, ETipoMovimiento tipo, decimal valor, decimal saldo)
        {
            this.NumeroCuenta = numeroCuenta;
            this.Tipo= tipo;
            this.Fecha= fecha;
            this.Saldo= saldo;
            this.Valor = valor;
        }

        public MMovimiento(int numeroCuenta, ETipoMovimiento tipo, decimal valor)
        {            
            Tipo = tipo;
            Valor = valor;
            this.NumeroCuenta = numeroCuenta;
        }
        public int NumeroCuenta { get; internal set; }
        public int Id { get;  set; }
        public DateTime Fecha { get;  set; }
        public ETipoMovimiento Tipo{ get; internal set; }
        public decimal Valor { get; internal set; }
        public decimal Saldo { get; internal set; }

        internal bool isValid()
        {
            return   (Valor != 0)
                && NumeroCuenta!=0
                ;
        }
    }
}
