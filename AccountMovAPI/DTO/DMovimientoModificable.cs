using CORE.Account.Domain.Model;

namespace AccountMovAPI.DTO
{
    public class DMovimientoModificable: DNuevoMovimiento
    {
        public DMovimientoModificable() : base() { }
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Saldo { get; set; }
        public override MMovimiento toMovimiento()
        {
            return new MMovimiento(NumeroCuenta,Id,Fecha,Tipo,Valor,Saldo);
        }
    }
}
