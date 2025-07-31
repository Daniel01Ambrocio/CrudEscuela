using SysEscolar.DLL;
using SysEscolar.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SysEscolar.BLL
{
    public class RolBLL
    {
        RolDLL rolDLL = new RolDLL();
        public RolEnt ObtenerRolPorID(int idrol)
        {
            return rolDLL.ObtenerRolPorID(idrol);
        }
        // Obtiene todos los roles
        public DataTable ObtenerRoles()
        {
            return rolDLL.ObtenerRoles();
        }
    }
}