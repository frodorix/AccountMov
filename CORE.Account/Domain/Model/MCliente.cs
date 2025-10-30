using CORE.Account.Domain.Enum;

namespace CORE.Account.Domain.Model
{
    public class MCliente : MPersona
    {
        public MCliente(string nombre, EGenero genero, int edad, string identificacion, string direccion, string telefono) : base(nombre, genero, edad, identificacion, direccion, telefono)
        {
            this.Estado = EEstadoCliente.Activo;
            this.Contrasena = string.Empty;
        }

        public MCliente(string nombre, EGenero genero, int edad, string identificacion, string direccion, string telefono, string contrasena) : this(nombre, genero, edad, identificacion, direccion, telefono)
        {
            this.Contrasena = contrasena;
        }

        public MCliente(int id, string nombre, EGenero genero, int edad, string identificacion, string direccion, string telefono, string contrasena, EEstadoCliente estado) : base(nombre, genero, edad, identificacion, direccion, telefono)
        {
            this.Id = id;
            this.Contrasena = contrasena;
            this.Estado = estado;
        }

        public int Id { get; set; }
        public string Contrasena { get; set; }
        public EEstadoCliente Estado { get; internal set; }

        public bool isValid()
        {
            var valido = (this.Identificacion.Length > 5)
                && (this.Nombre.Length > 10)
                && (!string.IsNullOrWhiteSpace(this.Contrasena) && this.Contrasena.Length >= 4);

            return valido;
        }

        public decimal LimiteDiario { get; internal set; } = 1000;
    }
}
