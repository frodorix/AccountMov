﻿using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;

namespace AccountMovAPI.DTO
{
    public class DNuevoMovimiento
    {
        public DNuevoMovimiento() { }
        public ETipoMovimiento Tipo { get; set; }
        public int NumeroCuenta { get; set; }
        public decimal Valor{get;set; }

        public virtual MMovimiento toMovimiento()
        {
            return new MMovimiento(NumeroCuenta, Tipo, Valor);
        }
    }
}
