using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;

namespace AccountMovAPI.DTO
{
    public class DClienteNuevo : DClienteBase
    {
        public DClienteNuevo() { }
       
        public string Contrasena { get; set; }
        public MCliente toMCliente() {
            return new MCliente(Nombre, Genero, Edad, Identificacion, Direccion, Telefono, Contrasena);
        }

    }
}
