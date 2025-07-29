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
    public partial class EditarEspecialidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IDEspecialidad"] != null)
                {
                    int idEspecialidad = (int)Session["IDEspecialidad"];

                    if (idEspecialidad > 0)
                    {
                        // Aquí puedes autollenar el formulario con los datos de la Especialidad
                        CargarDatosEspecialidad(idEspecialidad);
                    }
                    else
                    {
                        Response.Redirect("Especialidades.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Especialidades.aspx");
                }
            }
        }
        private void CargarDatosEspecialidad(int idEspecialidad)
        {
            EspecialidadBLL especialidadBLL = new EspecialidadBLL();
            EspecialidadEnt especialidad = especialidadBLL.ObtenerEspecialidadPorID(idEspecialidad);
            DivisionesBLL divisionesBLL = new DivisionesBLL();
            if (especialidad != null)
            {
                txtNombre.Text = especialidad.NombresEspecialidad;
                txtDescripcion.Text = especialidad.DescripcionEsp;
                DataTable dt = divisionesBLL.ObtenerDivisiones();

                ddlDivision.DataSource = dt;
                ddlDivision.DataTextField = "NombreDivision";  // lo que se muestra
                ddlDivision.DataValueField = "IDDivision";     // el valor interno
                ddlDivision.DataBind();

                // Opción opcional al inicio (placeholder)
                ddlDivision.Items.Insert(0, new ListItem("-- Selecciona una división --", "0"));
                ddlDivision.SelectedValue = especialidad.DivisionID.ToString();
            }
            else
            {
                // Si no encuentra la especialoidad rediige
                Response.Redirect("Especialidades.aspx");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            int iddivi = Convert.ToInt32(ddlDivision.SelectedValue);
            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtDescripcion.Text) && iddivi > 0)
            {
                EspecialidadEnt especialidad = new EspecialidadEnt()
                {
                    IDEspecialidad = (int)Session["IDEspecialidad"],
                    NombresEspecialidad = txtNombre.Text.Trim(),
                    DescripcionEsp = txtDescripcion.Text.Trim(),
                    DivisionID = Convert.ToInt32(ddlDivision.SelectedValue)
                };

                EspecialidadBLL especialidadBLL = new EspecialidadBLL();
                bool actualizado = especialidadBLL.ActualizarEspecialidad(especialidad);

                if (actualizado)
                {
                    MostrarAlerta("Especialidad actualizada correctamente", true);
                    Response.Redirect("Especialidades.aspx");
                }
                else
                {
                    MostrarAlerta("Error al actualizar la Especialidad. Intentelo nuevamente.", false);
                }
            }
            else
            {
                MostrarAlerta("Formulario incompleto.", false);
            }

        }
        protected void LimpiarFormulario()
        {
            txtDescripcion.Text = "";
            txtNombre.Text = "";
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

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("Especialidades.aspx");
        }
    }
}