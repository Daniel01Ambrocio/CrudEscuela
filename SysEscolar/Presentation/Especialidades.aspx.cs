using SysEscolar.BLL;
using SysEscolar.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysEscolar.Presentation
{
    public partial class Especialidades : System.Web.UI.Page
    {
        EspecialidadBLL especialidadBLL = new EspecialidadBLL();
        DivisionesBLL divisionesBLL = new DivisionesBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                CargarDivisiones();
            }
        }

        private void CargarEspecialidades()
        {
            gvEspecialidades.DataSource = especialidadBLL.ObtenerEspecialidades(); // Devuelve un DataTable o lista de objetos
            gvEspecialidades.DataBind();
        }
        private void CargarDivisiones()
        {
            DataTable dt = divisionesBLL.ObtenerDivisiones();

            ddlDivision.DataSource = dt;
            ddlDivision.DataTextField = "NombreDivision";  // lo que se muestra
            ddlDivision.DataValueField = "IDDivision";     // el valor interno
            ddlDivision.DataBind();

            // Opción opcional al inicio (placeholder)
            ddlDivision.Items.Insert(0, new ListItem("-- Selecciona una división --", "0"));
        }
        protected void gvEspecialidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Session["IDEspecialidad"] = id;
                Response.Redirect("EditarEspecialidad.aspx");
            }
            else if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                bool eliminado = especialidadBLL.EliminarEspecialidad(id);

                if (eliminado)
                {
                    CargarEspecialidades();
                    MostrarAlerta("Eliminación exitosa.", true);
                }
                else
                {
                    // Opcional: mostrar mensaje de error
                    MostrarAlerta("Eliminación fallida.", false);
                }
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            int idDivision = int.Parse(ddlDivision.SelectedValue);

            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(descripcion) && idDivision > 0)
            {
                EspecialidadEnt especialidad = new EspecialidadEnt
                {
                    NombresEspecialidad = nombre,
                    DescripcionEsp = descripcion,
                    DivisionID = idDivision
                };

                bool resultado = especialidadBLL.AgregarEspecialidad(especialidad);

                if (resultado)
                {
                    LimpiarFormulario();
                    MostrarAlerta("Especialidad agregado exitosamente.", true);
                    CargarEspecialidades(); // Actualiza el GridView u otra lista
                }
                else
                {
                    MostrarAlerta("Error al agregar la Especialidad, inténtalo de nuevo.", false);
                }
            }
            else
            {
                MostrarAlerta("Todos los campos son obligatorios.", false);
            }
        }

        protected void LimpiarFormulario()
        {
            txtDescripcion.Text = "";
            txtNombre.Text = "";
            ddlDivision.SelectedIndex = -1;
        }
        protected void MostrarAlerta(string mensaje, bool esExito)
        {
            // Color verde para éxito, rojo para error
            string color = esExito ? "green" : "red";

            // Script para mostrar una alerta centrada con estilos personalizados
            string script = $@"
                var alerta = document.createElement('div');
                alerta.innerText = '{mensaje}';
                alerta.style.position = 'fixed';
                alerta.style.top = '50%';
                alerta.style.left = '50%';
                alerta.style.transform = 'translate(-50%, -50%)';
                alerta.style.backgroundColor = '{color}';
                alerta.style.color = 'white';
                alerta.style.padding = '15px 30px';
                alerta.style.borderRadius = '8px';
                alerta.style.fontWeight = 'bold';
                alerta.style.boxShadow = '0 4px 12px rgba(0, 0, 0, 0.2)';
                alerta.style.zIndex = '9999';
                document.body.appendChild(alerta);
                setTimeout(function() {{ alerta.remove(); }}, 3000);";

            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarAlerta", script, true);
        }
    }
}