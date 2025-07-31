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
    public partial class Divisiones : System.Web.UI.Page
    {
        UsuarioEnt usuario = new UsuarioEnt();
        RolEnt rol = new RolEnt();
        DivisionesBLL divisionesBLL = new DivisionesBLL();
        EspecialidadBLL especialidadBLL = new EspecialidadBLL();
        EdificiosBLL edificiosBLL = new EdificiosBLL();
        DivisionEnt division = new DivisionEnt();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuario.IDUsuario = (int)Session["IDUsu"];
                usuario.Status = Session["Status"].ToString();
                if (usuario.IDUsuario > 0 && usuario.Status == "Alta")
                {
                    rol.NombreRol = Session["Rol"].ToString();
                    if (rol.NombreRol == "Gestor Académico")
                    {
                        //Codigo para hacer el crud
                        CargarDivisiones();
                        LimpiarFormulario();
                    }
                    else
                    {
                        Response.Redirect("index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("index.aspx");
                }

            }
        }

        private void CargarDivisiones()
        {
            gvDivisiones.DataSource = divisionesBLL.ObtenerDivisiones(); // Devuelve un DataTable o lista de objetos
            gvDivisiones.DataBind();
        }

        protected void gvDivisiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Session["IDDivision"] = id;
                Response.Redirect("EditarDivision.aspx");
            }
            else if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                //Validamos que no este siendo referenciado en otra tabla el iddivision
                bool idReferenciadoEs = especialidadBLL.BuscaDivisionForanea(id);
                if (idReferenciadoEs == false)
                {
                    bool idReferenciadoEd = edificiosBLL.BuscaDivisionForanea(id);
                    if (idReferenciadoEd == false)
                    {
                        bool eliminado = divisionesBLL.EliminarDivision(id);

                        if (eliminado)
                        {
                            CargarDivisiones();
                            MostrarAlerta("Eliminación exitosa.", true);
                        }
                        else
                        {
                            // Opcional: mostrar mensaje de error
                            MostrarAlerta("Eliminación fallida.", false);
                        }
                    }
                    else
                    {
                        // Opcional: mostrar mensaje de error
                        MostrarAlerta("Eliminación fallida. La division está siendo referenciada en la tabla Edificios.", false);
                    }
                }
                else
                {
                    // Opcional: mostrar mensaje de error
                    MostrarAlerta("Eliminación fallida. La division está siendo referenciada en la tabla Especialidades.", false);
                }

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;

            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(descripcion))
            {
                // Ambos valores tienen contenido
                division.NombreDivision = txtNombre.Text;
                division.DescripcionDiv = txtDescripcion.Text;
                bool valida = divisionesBLL.AgregarDivision(division);
                if (valida)
                {
                    LimpiarFormulario();
                    MostrarAlerta("Registro exitoso.", true);
                    CargarDivisiones();
                }
                else
                {
                    MostrarAlerta("Error al registrar una nueva división, intentelo nuevamente.", false);
                }
            }
            else
            {
                MostrarAlerta("Faltan datos obligatorios", false);
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


    }
}