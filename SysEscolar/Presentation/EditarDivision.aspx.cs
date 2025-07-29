using SysEscolar.BLL;
using SysEscolar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysEscolar.Presentation
{
    public partial class EditarDivision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IDDivision"] != null)
                {
                    int iddivision = (int)Session["IDDivision"];

                    if (iddivision > 0)
                    {
                        // Aquí puedes autollenar el formulario con los datos de la división
                        CargarDatosDivision(iddivision);
                    }
                    else
                    {
                        Response.Redirect("Divisiones.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Divisiones.aspx");
                }
            }
        }
        private void CargarDatosDivision(int idDivision)
        {
            DivisionesBLL divisionesBLL = new DivisionesBLL();
            DivisionEnt division = divisionesBLL.ObtenerDivisionPorID(idDivision);

            if (division != null)
            {
                txtNombre.Text = division.NombreDivision;
                txtDescripcion.Text = division.DescripcionDiv;
            }
            else
            {
                // Si no encuentra la división, redirige
                Response.Redirect("Divisiones.aspx");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            DivisionEnt division = new DivisionEnt
            {
                IDDivision = (int)Session["IDDivision"],
                NombreDivision = txtNombre.Text.Trim(),
                DescripcionDiv = txtDescripcion.Text.Trim()
            };

            DivisionesBLL bll = new DivisionesBLL();
            bool actualizado = bll.ActualizarDivision(division);

            if (actualizado)
            {
                MostrarAlerta("División actualizada correctamente", true);
                Response.Redirect("Divisiones.aspx");
            }
            else
            {
                MostrarAlerta("Error al actualizar la división. Intentelo nuevamente.", false);
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
            Response.Redirect("Divisiones.aspx");
        }
    }
}