using CORE.Account.Domain.Enum;

namespace CORE.Account.DTO
{
    public class DCliente
    {
        public DCliente(int id, string nombre, EEstadoCliente estado, DEstadoCuenta[] estadoCuenta)
        {
            Id = id;
            Nombre = nombre;
            Estado = estado;
            EstadoCuenta = estadoCuenta;
        }

        public int Id { get; }
        public string Nombre { get; }
        public EEstadoCliente Estado { get; }
        public DEstadoCuenta[] EstadoCuenta { get; }
    }
}