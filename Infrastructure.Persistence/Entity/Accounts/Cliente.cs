using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace com.frodorix.bank
{
    [Table("Cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        [Key]
        public int ClienteId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;
        public short Genero { get; set; }
        public short Edad { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string Identificacion { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Direccion { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string Telefono { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Contrasena { get; set; } = null!;
        public int Estado { get; set; }
        [Column("LimiteDiario", TypeName = "decimal(18, 2)")]
        public decimal LimiteDiario { get; set; }
        [InverseProperty("Cliente")]
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
