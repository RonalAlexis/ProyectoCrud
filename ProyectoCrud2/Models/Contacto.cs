using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCrud2.Models
{
    public class Contacto
    {
        public int Idcontacto { get; set; }
        public String Nombres { get; set; }
        public String Apellido { get; set; }
        public String Telefono { get; set; }
        public String Correo { get; set; }

    }
}
