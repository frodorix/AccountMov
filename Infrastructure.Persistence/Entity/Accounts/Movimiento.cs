using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CORE.Account.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Entity.Accounts
{
    public partial class Movimiento
    {
        public Movimiento() { }
        public Movimiento(int numeroCuenta, DateTime fecha, ETipoMovimiento tipo, decimal valor, decimal saldo)
        {
            NumeroCuenta = numeroCuenta;
            Fecha = fecha;
            Tipo = tipo;
            Valor = valor;
            Saldo = saldo;
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }
        [Column("tipo")]
        public ETipoMovimiento Tipo { get; set; }
        [Column("valor", TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
        [Column("saldo", TypeName = "decimal(18, 2)")]
        public decimal Saldo { get; set; }
        [Column("numeroCuenta")]
        public int? NumeroCuenta { get; set; }

        [ForeignKey("NumeroCuenta")]
        [InverseProperty("Movimientos")]
        public virtual Cuenta Cuenta { get; set; }
    }
}
