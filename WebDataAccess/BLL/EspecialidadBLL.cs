using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using WebDataAccess.Data;

namespace WebDataAccess.BLL
{
    public class EspecialidadBLL
    {
        private readonly EspecialidadData EspecialidadData = new EspecialidadData();

        public async Task<DataTable> ObtenerEspecialidadAsync()
        {
            return await EspecialidadData.MostrarDatosGridViewAsync("SELECT * FROM Especialidades");
        }

        public async Task InsertarEspecialidadAsync(GridViewRow registro)
        {
            string nombre = ((TextBox)registro.FindControl("tbd0")).Text;
            string descripcion = ((TextBox)registro.FindControl("tbd1")).Text;
            int division = int.Parse(((TextBox)registro.FindControl("tbd2")).Text);
            await EspecialidadData.InsertaEspecialidadAsync(nombre, descripcion, division);
        }

        public async Task ActualizarEspecialidadAsync(int EspecialidadID, string nuevoNombre, string nuevaDescripcion, int nuevadivision)
        {
            await EspecialidadData.ActualizarEspecialidadPorIDAsync(EspecialidadID, nuevoNombre, nuevaDescripcion, nuevadivision);
        }

        public async Task EliminarEspecialidadAsync(int EspecialidadID)
        {
            await EspecialidadData.EliminarEspecialidadPorIDAsync(EspecialidadID);
        }
    }
}