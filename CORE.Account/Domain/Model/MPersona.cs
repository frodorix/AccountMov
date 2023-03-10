using CORE.Account.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Domain.Model
{
    public class MPersona
    {
        public MPersona(string nombre, EGenero genero, int edad, string identificacion, string direccion, string telefono)
        {
            this.Nombre = nombre;
            this.Genero = genero;
            this.Edad = edad;
            this.Identificacion = identificacion;
            this.Direccion = direccion;
            this.Telefono = telefono;
        }
        public string Nombre { get; internal set; }
        public EGenero Genero { get;  set; }
        public int Edad { get;  set; }
        public string Identificacion { get; internal set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

    }
}
