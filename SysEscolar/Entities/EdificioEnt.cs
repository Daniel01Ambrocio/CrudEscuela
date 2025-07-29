using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysEscolar.Entities
{
    public class EdificioEnt
    {
        public int IDEdificio { get; set; }
        public string NombreEdificio { get; set; }
        public string DescripcionEdif { get; set; }
        public int? DivisionID { get; set; } // Nullable porque puede ser null
    }
}