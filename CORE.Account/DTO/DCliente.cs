using CORE.Account.Domain.Enum;

namespace CORE.Account.DTO
{
    public class DCliente
    {
        public DCliente(int clienteId, string nombre, EEstadoCliente estado, string identificacion) { }

        public DCliente(int clienteId, string nombre, EEstadoCliente estado, string identificacion, DEstadoCuenta[] estadoCuenta) : this(clienteId, nombre, estado, identificacion)
        {
            this.EstadoCuenta = estadoCuenta;
        }

        public int Id { get;  }
        public string Nombre { get;  }
        public EEstadoCliente Estado { get;  }
        public DEstadoCuenta[] EstadoCuenta { get;  }
        public string Identificacion { get;  }
    }
}