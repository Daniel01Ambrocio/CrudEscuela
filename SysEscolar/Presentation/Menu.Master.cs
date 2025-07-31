using SysEscolar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysEscolar.Presentation
{
    public partial class Menu : System.Web.UI.MasterPage
    {
        RolEnt rol = new RolEnt();
        UsuarioEnt usuario = new UsuarioEnt();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuario.IDUsuario = (int)Session["IDUsu"];
                usuario.Status = Session["Status"].ToString();

                if (usuario.IDUsuario > 0 && usuario.Status == "Alta")
                {
                    rol.NombreRol = Session["Rol"].ToString();

                    // Aquí controlas la visibilidad de las opciones del menú
                    if (rol.NombreRol == "Gestor de Usuarios")
                    {
                        divdivi.Visible = false;
                        divedi.Visible = false;
                        divespe.Visible = false;
                        divusu.Visible = true;
                    }
                    else if (rol.NombreRol == "Gestor Académico")
                    {
                        divdivi.Visible = true;
                        divedi.Visible = true;
                        divespe.Visible = true;
                        divusu.Visible = false;
                    }
                    else
                    {
                        Response.Redirect("index.aspx");
                    }

                }
            }
        }
    }
}