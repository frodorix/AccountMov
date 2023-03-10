using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;

namespace AccountMovAPI.DTO
{
    public class DClienteModificable:DClienteBase
    {
        public int Id { get; set; }
        public EEstadoCliente Estado { get; set; }
        public MCliente ToMcliente()
        {
            return new MCliente(this.Id, Nombre, Genero, Edad, Identificacion, Direccion, Telefono,"",Estado);

        }
    }
}
