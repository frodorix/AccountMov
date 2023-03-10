using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;

namespace AccountMovAPI.DTO
{
    public class DNuevaCuenta
    {
        public DNuevaCuenta() { }
        public int ClienteId { get; set; }
        public ETipoCuenta Tipo { get; set; }
        public decimal SaldoInicial { get; set; }

        internal MCuenta ToMCuenta()
        {
            return new MCuenta(this.ClienteId, this.SaldoInicial, Tipo);
        }
    }
}
