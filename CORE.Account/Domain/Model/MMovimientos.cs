using CORE.Account.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Domain.Model
{
    public class MMovimientos
    {
        public DateTime Fecha { get; internal set; }
        public ETipoMovimiento Tipo{ get; internal set; }
        public decimal valor { get; internal set; }
        public decimal saldo { get; internal set; } 
    }
}
