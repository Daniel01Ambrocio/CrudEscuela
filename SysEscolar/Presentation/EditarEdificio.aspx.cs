using SysEscolar.BLL;
using SysEscolar.DLL;
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
    public partial class EditarEdificio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IDEdificio"] != null)
                {
                    int idEdificio = (int)Session["IDEdificio"];

                    if (idEdificio > 0)
                    {
                        // Aquí puedes autollenar el formulario con los datos del edificio
                        CargarDatosEdificio(idEdificio);
                    }
                    else
                    {
                        Response.Redirect("Edificios.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Edificios.aspx");
                }
            }
        }
        private void CargarDatosEdificio(int idEdificio)
        {
            EdificiosBLL edificiosBLL = new EdificiosBLL();
            EdificioEnt edificio = edificiosBLL.ObtenerEdificioPorID(idEdificio);
            DivisionesBLL divisionesBLL = new DivisionesBLL();
            if (edificio != null)
            {
                txtNombre.Text = edificio.NombreEdificio;
                txtDescripcion.Text = edificio.DescripcionEdif;
                DataTable dt = divisionesBLL.ObtenerDivisiones();

                ddlDivision.DataSource = dt;
                ddlDivision.DataTextField = "NombreDivision";  // lo que se muestra
                ddlDivision.DataValueField = "IDDivision";     // el valor interno
                ddlDivision.DataBind();

                // Opción opcional al inicio (placeholder)
                ddlDivision.Items.Insert(0, new ListItem("-- Selecciona una división --", "0"));
                ddlDivision.SelectedValue = edificio.DivisionID.ToString();
            }
            else
            {
                // Si no encuentra el  edificio rediige
                Response.Redirect("Edificios.aspx");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            int iddivi = Convert.ToInt32(ddlDivision.SelectedValue);
            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtDescripcion.Text) &&  iddivi> 0)
            {
                EdificioEnt edificio = new EdificioEnt()
                {
                    IDEdificio = (int)Session["IDEdificio"],
                    NombreEdificio = txtNombre.Text.Trim(),
                    DescripcionEdif = txtDescripcion.Text.Trim(),
                    DivisionID = Convert.ToInt32(ddlDivision.SelectedValue)
                };

                EdificiosBLL edificiosBLL = new EdificiosBLL();
                bool actualizado = edificiosBLL.ActualizarEdificio(edificio);

                if (actualizado)
                {
                    MostrarAlerta("Edificio actualizado correctamente", true);
                    Response.Redirect("Edificios.aspx");
                }
                else
                {
                    MostrarAlerta("Error al actualizar el edificio. Intentelo nuevamente.", false);
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
            Response.Redirect("Edificios.aspx");
        }
    }
}