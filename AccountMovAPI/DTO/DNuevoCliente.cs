using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;

namespace AccountMovAPI.DTO
{
    public class DNuevoCliente{
        public DNuevoCliente() { }
        public string Nombre { get;  set; }
        public EGenero Genero { get; set; }
        public short Edad { get; set; }
        public string Identificacion { get;  set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public MCliente toMCliente() {
            return new MCliente(Nombre, Genero, Edad, Identificacion, Direccion, Telefono, Contrasena);
        }

    }
}
