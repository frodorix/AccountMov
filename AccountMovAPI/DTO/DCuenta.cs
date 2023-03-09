using CORE.Account.Domain.Enum;

namespace WebAPI.DTO
{
    internal class DCuenta
    {
        public DCuenta() { }
        public int id { get; internal set; }
        public EEstadoCuenta estado { get; internal set; }
    }
}