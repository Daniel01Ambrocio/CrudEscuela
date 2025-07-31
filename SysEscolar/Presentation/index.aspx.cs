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
    public partial class index : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Este método se ejecuta cuando se carga la página
                Session.Abandon();
            }
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            UsuariosBLL usuariosBLL = new UsuariosBLL();
            UsuarioEnt usuario = new UsuarioEnt();
            RolEnt rol = new RolEnt();
            RolBLL rolBLL = new RolBLL();
            string email = txtemail.Text;
            string password = txtpassword.Text;

            // Valida que el formulario no esté vacio
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                //Valida que el email y contraseña exista a un usuario
                usuario = usuariosBLL.ValidaCredenciales(email, password);
                if (usuario != null)
                {
                    //Valida que el usuario tenga un Estatus Alta
                    if (usuario.Status == "Alta")
                    {
                        //Valida el rol del usuario
                        rol = rolBLL.ObtenerRolPorID(usuario.RolID);
                        Session["IDUsu"] = usuario.IDUsuario;
                        Session["Rol"] = rol.NombreRol;
                        Session["Status"] = usuario.Status;
                        if (rol.NombreRol == "Gestor de Usuarios")
                        {
                            // Redirigir al area de Usuarios
                            Response.Redirect("Usuarios.aspx");
                        }
                        else if (rol.NombreRol == "Gestor Académico")
                        {
                            // Redirigir al area de divisiones
                            Response.Redirect("Divisiones.aspx");
                        }

                    }
                    else
                    {
                        MostrarAlerta("El usuario se encuentra suspendido.", false);
                    }

                }
                else
                {
                    MostrarAlerta("Correo y/o contraseña incorrectas.", false);
                }
            }
            else
            {
                MostrarAlerta("Formulario incompleto.", false);
            }
        }
        protected void LimpiarFormulario()
        {
            txtemail.Text = "";
            txtpassword.Text = "";
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