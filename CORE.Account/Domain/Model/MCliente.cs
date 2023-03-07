using CORE.Account.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Domain.Model
{
    public class MCliente : MPersona
    {
        public MCliente(string nombre, EGenero genero, int edad, string identificacion, string direccion, string telefono) : base(nombre, genero, edad, identificacion, direccion, telefono)
        {
        }
        public MCliente(int id, string nombre, EGenero genero, int edad, string identificacion, string direccion, string telefono, string contrasena, EEstadoCliente estado) : base(nombre, genero, edad, identificacion, direccion, telefono)
        {
            this.Id= id;
            this.Contrasena = contrasena;
            this.Estado = estado;
        }
        public int Id { get; internal set; }
        public string Contrasena { get; internal set; }
        public EEstadoCliente Estado { get; internal set; }
    }
}
