﻿using CORE.Account.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Domain.Model
{
    public class MCuenta
    {

        public MCuenta(int clienteId, decimal saldoInicial, ETipoCuenta tipo)
        {            
            ClienteId = clienteId;
            SaldoInicial = saldoInicial;
            Estado = EEstadoCuenta.Activo;
            this.Tipo = tipo;
        }
        public MCuenta(int numeroCuenta, int clienteId, decimal saldoInicial, EEstadoCuenta estado)
        {
            NumeroCuenta = numeroCuenta;
            ClienteId = clienteId;
            SaldoInicial = saldoInicial;
            Estado = estado;
        }

        public int NumeroCuenta { get; set; }
        public ETipoCuenta Tipo { get; internal set; }
        public decimal SaldoInicial { get; internal set; }
        public EEstadoCuenta Estado { get; set; }
        public int ClienteId { get; set; }

        public bool isValid()
        {
            var valido = (this.ClienteId > 0);
            return valido;
        }
    }
}
