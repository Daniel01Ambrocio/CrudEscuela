using SysEscolar.DLL;
using SysEscolar.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SysEscolar.BLL
{
    public class EdificiosBLL
    {
        private EdificiosDLL edificiosDLL = new EdificiosDLL();

        // Obtiene todas las Edificios
        public DataTable ObtenerEdificios()
        {
            return edificiosDLL.ObtenerEdificios();
        }
        //Agrega un nuevo registro
        public bool AgregarEdificio(EdificioEnt edificio)
        {
            return edificiosDLL.AgregarEdificio(edificio);
        }
        // Elimina un Edificio por ID
        public bool EliminarEdificio(int idEdificios)
        {
            return edificiosDLL.EliminarEdificio(idEdificios);
        }
        //Obtiene la informacion de un edificio por id
        public EdificioEnt ObtenerEdificioPorID(int idEdificio)
        {
            return edificiosDLL.ObtenerEdificioPorID(idEdificio);
        }
        //metodo para actualizar un edificio
        public bool ActualizarEdificio(EdificioEnt edificio)
        {
            return edificiosDLL.ActualizarEdificio(edificio);
        }
        //Valida que el iddivision no esté referenciada
        public bool BuscaDivisionForanea(int id)
        {
            return edificiosDLL.BuscaDivisionForanea(id);
        }
    }
}