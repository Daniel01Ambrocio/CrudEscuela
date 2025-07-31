using SysEscolar.BLL;
using SysEscolar.DLL;
using SysEscolar.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysEscolar.Presentation
{
    public partial class Usuarios : System.Web.UI.Page
    {
        UsuarioEnt usuario = new UsuarioEnt();
        UsuariosBLL usuariosBLL = new UsuariosBLL();
        RolEnt rol = new RolEnt();
        RolBLL rolBLL = new RolBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Deshabilitar el caché del navegador
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
            if (!IsPostBack)
            {
                if (Session["IDUsu"] == null)
                {
                    Response.Redirect("index.aspx");
                }
                else
                {


                    usuario.IDUsuario = (int)Session["IDUsu"];
                    usuario.Status = Session["Status"].ToString();
                    if (usuario.IDUsuario > 0 && usuario.Status == "Alta")
                    {
                        rol.NombreRol = Session["Rol"].ToString();
                        if (rol.NombreRol == "Gestor de Usuarios")
                        {
                            //Codigo para hacer el crud
                            CargarUsuarios();
                            CargarRoles();
                            CargarStatus();
                        }
                        else
                        {
                            Response.Redirect("index.aspx");
                        }
                    }
                }
            }
        }
        private void CargarUsuarios()
        {
            gvUsuarios.DataSource = usuariosBLL.ObtenerUsuarios(); // Devuelve un DataTable o lista de objetos
            gvUsuarios.DataBind();
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
            DataTable dt = new DataTable();
            // Opción opcional al inicio (placeholder)
            ddlEstatus.Items.Insert(0, new ListItem("-- Selecciona un estatus --", "0"));
            // Insertar las opciones "Alta" y "Baja" al principio
            ddlEstatus.Items.Insert(1, new ListItem("Alta", "Alta"));   
            ddlEstatus.Items.Insert(2, new ListItem("Baja", "Baja")); 

            
        }
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Session["IDUsuario"] = id;
                Response.Redirect("EditarUsuario.aspx");
            }
            else if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                bool eliminado = usuariosBLL.EliminarUsuario(id);

                if (eliminado)
                {
                    CargarUsuarios();
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
            string correo = txtCorreo.Text;
            string contra = txtContra.Text;
            string confirmaContra = txtContra.Text;
            int idRol = int.Parse(ddlRol.SelectedValue);
            string status = ddlEstatus.SelectedValue;

            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(correo) && !string.IsNullOrEmpty(contra) && !string.IsNullOrEmpty(confirmaContra) && idRol > 0 && status != "0" )
            {
                //Validamos que ambas contraseñas sean iguales
                if(contra == confirmaContra)
                {
                    bool contrasenaSegura = ContraSegura(contra);
                    if (contrasenaSegura)
                    {
                        //Validamos que el correo no exista en otra cuenta
                        bool correoExiste = usuariosBLL.ValidaCorreoExiste(correo);
                        if (correoExiste)
                        {
                            MostrarAlerta("El correo ya está asociado a otra cuenta. Intente con un correo diferente.", false);
                        }
                        else
                        {
                            UsuarioEnt Usuario = new UsuarioEnt
                            {
                                Nombre = nombre,
                                Correo = correo,
                                Contrasena = contra,
                                RolID = idRol,
                                Status = status
                            };

                            bool resultado = usuariosBLL.AgregarUsuario(Usuario);

                            if (resultado)
                            {
                                LimpiarFormulario();
                                MostrarAlerta("Usuario agregado exitosamente.", true);
                                CargarUsuarios(); // Actualiza el GridView u otra lista
                            }
                            else
                            {
                                MostrarAlerta("Error al agregar el Usuario, inténtalo de nuevo.", false);
                            }
                        }
                    }
                    else
                    {
                        MostrarAlerta("La nueva contraseña debe cumplir con los siguientes requisitos: debe contener al menos una letra mayúscula, una letra minúscula, un número, y tener una longitud mínima de 6 caracteres.", false);

                    }
                }
                else
                {
                    //No son iguales
                    MostrarAlerta("Las contraseñas no coinciden.", false);
                }

            }
            else
            {
                MostrarAlerta("Todos los campos son obligatorios.", false);
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
        protected void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtContraConfirma.Text = "";
            txtContra.Text = "";
            ddlEstatus.SelectedIndex = -1;
            ddlRol.SelectedIndex = -1;
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