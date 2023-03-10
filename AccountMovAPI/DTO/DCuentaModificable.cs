using CORE.Account.Domain.Enum;

namespace AccountMovAPI.DTO
{
    public class DCuentaModificable
    {
        public DCuentaModificable() { }
        public EEstadoCuenta Estado { get; internal set; }
    }
}
