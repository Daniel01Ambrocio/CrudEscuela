using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysEscolar.Entities
{
    public class UsuarioEnt
    {
        public int IDUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int RolID { get; set; }
        public string Status { get; set; }
    }
}