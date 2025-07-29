using SysEscolar.DLL;
using SysEscolar.Entities;
using SysEscolar.Presentation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SysEscolar.BLL
{
    public class EspecialidadBLL
    {
        private EspecialidadDLL especialidadDLL = new EspecialidadDLL();

        // Obtiene todas las Especialidades
        public DataTable ObtenerEspecialidades()
        {
            return especialidadDLL.ObtenerEspecialidades();
        }
        //Agrega un nuevo registro
        public bool AgregarEspecialidad(EspecialidadEnt especialidad)
        {
            return especialidadDLL.AgregarEspecialidad(especialidad);
        }
        // Elimina una Especialidad por ID
        public bool EliminarEspecialidad(int idEspecialidad)
        {
            return especialidadDLL.EliminarEspecialidad(idEspecialidad);
        }
        //Obtiene la informacion de una Especialidad por id
        public EspecialidadEnt ObtenerEspecialidadPorID(int idEspecialidad)
        {
            return especialidadDLL.ObtenerEspecialidadPorID(idEspecialidad);
        }
        //metodo para actualizar una Especialidad
        public bool ActualizarEspecialidad(EspecialidadEnt especialidad)
        {
            return especialidadDLL.ActualizarEspecialidad(especialidad);
        }
        //Valida que el iddivision no esté referenciada
        public bool BuscaDivisionForanea(int id)
        {
            return especialidadDLL.BuscaDivisionForanea(id);
        }
    }
}