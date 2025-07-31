using SysEscolar.DLL;
using SysEscolar.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SysEscolar.BLL
{
    public class UsuariosBLL
    {
        UsuariosDLL usuariosDLL = new UsuariosDLL();
        public UsuarioEnt ValidaCredenciales(string email, string password)
        {
            return usuariosDLL.ValidaCredenciales(email, password);
        }
        // Obtiene todas las Usuarios
        public DataTable ObtenerUsuarios()
        {
            return usuariosDLL.ObtenerUsuarios();
        }
        //Agrega un nuevo registro
        public bool AgregarUsuario(UsuarioEnt Usuario)
        {
            return usuariosDLL.AgregarUsuario(Usuario);
        }
        // Elimina un Usuario por ID
        public bool EliminarUsuario(int idUsuarios)
        {
            return usuariosDLL.EliminarUsuario(idUsuarios);
        }
        //Obtiene la informacion de un Usuario por id
        public UsuarioEnt ObtenerUsuarioPorID(int idUsuario)
        {
            return usuariosDLL.ObtenerUsuarioPorID(idUsuario);
        }
        //metodo para actualizar un Usuario
        public bool ActualizarUsuario(UsuarioEnt Usuario)
        {
            return usuariosDLL.ActualizarUsuario(Usuario);
        }
        //metodo para validar la existencia de un correo
        public bool ValidaCorreoExiste(string correo)
        {
            return usuariosDLL.ValidaCorreoExiste(correo);
        }
        //Metodo que valida la contraseña corresponda al id
        public bool ValidaContraPorID(int id, string contra)
        {
            return usuariosDLL.ValidaContraPorID(id, contra);
        }
        //Metodo que  cambia la contraseña
        public bool CambiarContra(int id, string contra)
        {
            return usuariosDLL.CambiarContra(id, contra);
        }
    }
}