using SysEscolar.BLL;
using SysEscolar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysEscolar.Presentation
{
    public partial class CambiarContraseña : System.Web.UI.Page
    {
        UsuarioEnt usuario = new UsuarioEnt();
        UsuariosBLL usuariosBLL = new UsuariosBLL();
        RolEnt rol = new RolEnt();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuario.IDUsuario = (int)Session["IDUsu"];
                usuario.Status = Session["Status"].ToString();
                if (usuario.IDUsuario > 0 && usuario.Status == "Alta")
                {
                    
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
            }
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            rol.NombreRol = Session["Rol"].ToString();
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
            else if (rol.NombreRol == "Estudiante")
            {
                // Redirigir a la vista del Estudiante
                Response.Redirect("Estudiante.aspx");
            }
        }
        public bool ContraSegura(string password)
        {
            // Expresión regular para validar los requisitos:
            // - Al menos una letra mayúscula (A-Z)
            // - Al menos una letra minúscula (a-z)
            // - Al menos un número (0-9)
            // - Al menos 6 caracteres
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$";

            // Crear una instancia de Regex con el patrón
            Regex regex = new Regex(pattern);

            // Validar la contraseña contra la expresión regular
            return regex.IsMatch(password);
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            //Validamos que el formulario este lleno
            if (!string.IsNullOrEmpty(txtNueva.Text) && !string.IsNullOrEmpty(txtConfirma.Text) && !string.IsNullOrEmpty(txtContraAnterior.Text))
            {
                //validamos que la contraseña anterior corresponda al id
                int id = (int)Session["IDUsu"];
                bool valida = usuariosBLL.ValidaContraPorID(id, txtContraAnterior.Text);
                if (valida)
                {
                    //Validamos que ambas contraseñas nuevas sean iguales
                    if (txtConfirma.Text == txtNueva.Text)
                    {
                        //Validamos que la nueva contraseña y la anterior sean diferentes
                        if (txtNueva.Text != txtContraAnterior.Text)
                        {
                            //Validamos que sea una contraseña segura
                            bool contraSegura = ContraSegura(txtNueva.Text);
                            if (contraSegura)
                            {
                                //Actualizamos la contraseña
                                bool confirmaActualizacion = usuariosBLL.CambiarContra(id, txtNueva.Text);
                                if (confirmaActualizacion)
                                {
                                    MostrarAlerta("Actualización de contraseña exitosa.", true);


                                }
                                else
                                {
                                    MostrarAlerta("Error en el sistma. No se pudo actualixar la contraseña.", false);
                                }
                            }
                            else
                            {
                                MostrarAlerta("La nueva contraseña debe cumplir con los siguientes requisitos: debe contener al menos una letra mayúscula, una letra minúscula, un número, y tener una longitud mínima de 6 caracteres.", false);

                            }

                        }
                        else
                        {
                            MostrarAlerta("La nueva contraseña no puede ser igual a la anterior.", false);
                        }


                    }
                    else
                    {
                        MostrarAlerta("La nueva contraseña y la confirmación de contraseña no son iguales.", false);
                    }
                }
                else
                {
                    MostrarAlerta("La contraseña anterior no corresponde.", false);
                }
            }
            else
            {
                MostrarAlerta("El formulario está incompleto.", false);
            }
        }
        protected void LimpiarFormulario()
        {
            txtConfirma.Text = "";
            txtContraAnterior.Text = "";
            txtNueva.Text = "";
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
                setTimeout(function() {{ alerta.remove(); }}, 6000);";

            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarAlerta", script, true);
        }
    }
}