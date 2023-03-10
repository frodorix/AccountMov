using CORE.Account.Domain.Enum;

namespace AccountMovAPI.DTO
{
    public class DClienteBase
    {
        public DClienteBase() { }
        public string Nombre { get; set; }
        public EGenero Genero { get; set; }
        public short Edad { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
