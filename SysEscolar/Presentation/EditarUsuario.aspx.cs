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
    public partial class EditarUsuario : System.Web.UI.Page
    {
        UsuarioEnt usuario = new UsuarioEnt();
        UsuariosBLL usuariosBLL = new UsuariosBLL();
        RolEnt rol = new RolEnt();
        RolBLL rolBLL = new RolBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuario.IDUsuario = (int)Session["IDUsu"];
                usuario.Status = Session["Status"].ToString();
                if (usuario.IDUsuario > 0 && usuario.Status == "Alta")
                {
                    rol.NombreRol = Session["Rol"].ToString();
                    if (rol.NombreRol == "Gestor de Usuarios")
                    {
                        //Codigo para hacer el crud
                        if (Session["IDUsuario"] != null)
                        {
                            int idUsuario = (int)Session["IDUsuario"];

                            if (idUsuario > 0)
                            {
                                // Aquí puedes autollenar el formulario con los datos de la Usuario
                                CargarDatosUsuario(idUsuario);
                                CargarRoles();
                                CargarStatus();
                            }
                            else
                            {
                                Response.Redirect("Usuarios.aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect("Usuarios.aspx");
                        }

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
        private void CargarDatosUsuario(int idUsuario)
        {
            usuario = usuariosBLL.ObtenerUsuarioPorID(idUsuario);
            
            if (usuario != null)
            {
                txtNombre.Text = usuario.Nombre;
                ddlStatus.SelectedValue = usuario.Status.ToString();
                ddlRol.SelectedValue = usuario.RolID.ToString();
            }
            else
            {
                // Si no encuentra la especialoidad redirige
                Response.Redirect("Usuarios.aspx");
            }
        }
        private void CargarRoles()
        {
            DataTable dt = rolBLL.ObtenerRoles();

            ddlRol.DataSource = dt;
            ddlRol.DataTextField = "NombreRol";  // lo que se muestra
            ddlRol.DataValueField = "IDRol";     // el valor interno
            ddlRol.DataBind();

            // Opción opcional al inicio (placeholder)
            ddlRol.Items.Insert(0, new ListItem("-- Selecciona un rol --", "0"));
        }
        private void CargarStatus()
        {
            // Opción opcional al inicio (placeholder)
            ddlStatus.Items.Insert(0, new ListItem("-- Selecciona un estatus --", "0"));
            // Insertar las opciones "Alta" y "Baja" al principio
            ddlStatus.Items.Insert(1, new ListItem("Alta", "Alta"));
            ddlStatus.Items.Insert(2, new ListItem("Baja", "Baja"));


        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            int idrol = Convert.ToInt32(ddlRol.SelectedValue);
            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(ddlStatus.SelectedValue) && ddlStatus.SelectedValue != "0" && idrol > 0)
            {
                UsuarioEnt Usuario = new UsuarioEnt()
                {
                    IDUsuario = (int)Session["IDUsuario"],
                    Nombre = txtNombre.Text.Trim(),
                    Status = ddlStatus.SelectedValue.ToString(),
                    RolID = Convert.ToInt32(ddlRol.SelectedValue)
                };

                bool actualizado = usuariosBLL.ActualizarUsuario(Usuario);

                if (actualizado)
                {
                    MostrarAlerta("Usuario actualizado correctamente", true);
                    Response.Redirect("Usuarios.aspx");
                }
                else
                {
                    MostrarAlerta("Error al actualizar al Usuario. Intentelo nuevamente.", false);
                }
            }
            else
            {
                MostrarAlerta("Formulario incompleto.", false);
            }

        }
        protected void LimpiarFormulario()
        {
            txtNombre.Text = "";
            ddlRol.SelectedIndex = -1;
            ddlStatus.SelectedIndex = -1;
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
            Response.Redirect("Usuarios.aspx");
        }
    }
}