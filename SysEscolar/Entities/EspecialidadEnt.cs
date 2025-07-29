using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysEscolar.Entities
{
    public class EspecialidadEnt
    {
        public int IDEspecialidad { get; set; }
        public string NombresEspecialidad { get; set; }
        public string DescripcionEsp { get; set; }
        public int? DivisionID { get; set; } // Nullable porque puede ser null
    }
}