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
    public class EdificioBLL
    {
        private readonly EdificioData EdificioData = new EdificioData();

        public async Task<DataTable> ObtenerEdificioAsync()
        {
            return await EdificioData.MostrarDatosGridViewAsync("SELECT * FROM Edificios");
        }

        public async Task InsertarEdificioAsync(GridViewRow registro)
        {
            string nombre = ((TextBox)registro.FindControl("tbd0")).Text;
            string descripcion = ((TextBox)registro.FindControl("tbd1")).Text;
            int division = int.Parse(((TextBox)registro.FindControl("tbd2")).Text);
            await EdificioData.InsertaEdificioAsync(nombre, descripcion, division);
        }

        public async Task ActualizarEdificioAsync(int EdificioID, string nuevoNombre, string nuevaDescripcion, int nuevadivision)
        {
            await EdificioData.ActualizarEdificioPorIDAsync(EdificioID, nuevoNombre, nuevaDescripcion, nuevadivision);
        }

        public async Task EliminarEdificioAsync(int EdificioID)
        {
            await EdificioData.EliminarEdificioPorIDAsync(EdificioID);
        }
    }
}