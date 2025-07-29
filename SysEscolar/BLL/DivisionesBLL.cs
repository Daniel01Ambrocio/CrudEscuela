using SysEscolar.DLL;
using SysEscolar.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SysEscolar.BLL
{
    public class DivisionesBLL
    {
        private DivisionesDLL divisionesDLL = new DivisionesDLL();

        // Obtiene todas las divisiones
        public DataTable ObtenerDivisiones()
        {
            return divisionesDLL.ObtenerDivisiones();
        }
        //Agrega un nuevo registro
        public bool AgregarDivision(DivisionEnt division)
        {
            return divisionesDLL.AgregarDivision(division);
        }
        // Elimina una división por ID
        public bool EliminarDivision(int idDivision)
        {
            return divisionesDLL.EliminarDivision(idDivision);
        }
        //Obtiene la informacion de una division por id
        public DivisionEnt ObtenerDivisionPorID(int idDivision)
        {
            return divisionesDLL.ObtenerDivisionPorID(idDivision);
        }
        //metodo para actualizar una division
        public bool ActualizarDivision(DivisionEnt division)
        {
            return divisionesDLL.ActualizarDivision(division);
        }
    }
}