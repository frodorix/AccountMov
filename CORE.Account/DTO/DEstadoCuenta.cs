namespace CORE.Account.DTO
{
    public class DEstadoCuenta
    {
        public DEstadoCuenta() { }
        public int NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public int Debitos { get; set; }
        public int Creditos { get; set; }
    }
}