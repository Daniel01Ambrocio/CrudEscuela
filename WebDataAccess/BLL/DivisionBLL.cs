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
    public class DivisionBLL
    {
        private readonly DivisionData DivisionData = new DivisionData();

        public async Task<DataTable> ObtenerDivisionesAsync()
        {
            return await DivisionData.MostrarDatosGridViewAsync("SELECT * FROM Divisiones");
        }

        public async Task InsertarDivisionAsync(GridViewRow registro)
        {
            string nombre = ((TextBox)registro.FindControl("tbd0")).Text;
            string descripcion = ((TextBox)registro.FindControl("tbd1")).Text;

            await DivisionData.InsertaDivisionAsync(nombre, descripcion);
        }

        public async Task ActualizarDivisionAsync(int divisionID, string nuevoNombre, string nuevaDescripcion)
        {
            await DivisionData.ActualizarDivisionPorIDAsync(divisionID, nuevoNombre, nuevaDescripcion);
        }

        public async Task EliminarDivisionAsync(int divisionID)
        {
            await DivisionData.EliminarDivisionPorIDAsync(divisionID);
        }
    }
}