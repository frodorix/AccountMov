using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace com.frodorix.bank
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        [Key]
        [Column("numeroCuenta")]
        public int NumeroCuenta { get; set; }
        [Column("tipo")]
        public int Tipo { get; set; }
        [Column("saldoInicial", TypeName = "decimal(18, 2)")]
        public decimal SaldoInicial { get; set; }
        [Column("clienteId")]
        public int? ClienteId { get; set; }
        public int Estado { get; set; }

        [ForeignKey("ClienteId")]
        [InverseProperty("Cuenta")]
        public virtual Cliente? Cliente { get; set; }
        [InverseProperty("NumeroCuentaNavigation")]
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
