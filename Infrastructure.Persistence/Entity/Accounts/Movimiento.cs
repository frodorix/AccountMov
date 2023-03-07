using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace com.frodorix.bank
{
    public partial class Movimiento
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }
        [Column("tipo")]
        public int Tipo { get; set; }
        [Column("valor", TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
        [Column("saldo", TypeName = "decimal(18, 2)")]
        public decimal Saldo { get; set; }
        [Column("numeroCuenta")]
        public int? NumeroCuenta { get; set; }

        [ForeignKey("NumeroCuenta")]
        [InverseProperty("Movimientos")]
        public virtual Cuenta? NumeroCuentaNavigation { get; set; }
    }
}
