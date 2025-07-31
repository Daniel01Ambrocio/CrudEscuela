using SysEscolar.BLL;
using SysEscolar.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SysEscolar.DLL
{
    public class UsuariosDLL
    {
        string conexion = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        //valida que las credenciales existan a un usuario
        public UsuarioEnt ValidaCredenciales(string correo, string pass)
        {
            UsuarioEnt usuario = null;

            // Corregimos la consulta SQL, especificando las columnas correctas
            string query = "SELECT IDUsuario, Nombre, Correo, Contrasena, Status, RolID FROM Usuarios WHERE Correo = @correo AND Contrasena = @contrasena";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Parámetros de la consulta
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@contrasena", pass);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Asignamos los valores a la instancia de UsuarioEnt
                        usuario = new UsuarioEnt()
                        {
                            IDUsuario = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Correo = reader.GetString(2),
                            Contrasena = reader.GetString(3),
                            Status = reader.GetString(4),
                            RolID = reader.GetInt32(5)
                        };
                    }
                }
            }

            return usuario;
        }
        // Obtener todas los Usuarios
        public DataTable ObtenerUsuarios()
        {
            DataTable dt = new DataTable();

            string query = @"
            SELECT 
                u.IDUsuario, 
                u.Nombre, 
                u.Correo,
                r.NombreRol,
                u.Status
            FROM 
                Usuarios u
            LEFT JOIN 
            Roles r ON u.RolID = r.IDRol";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }
        //Agregar un nuevo Usuario
        public bool AgregarUsuario(UsuarioEnt Usuario)
        {
            int filasAfectadas = 0;
            string query = "INSERT INTO Usuarios (Nombre, Correo, Contrasena, RolID, Status) VALUES (@Nombre, @Correo, @Contrasena, @RolID, @Status)";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", Usuario.Nombre);
                cmd.Parameters.AddWithValue("@Correo", Usuario.Correo);
                cmd.Parameters.AddWithValue("@Contrasena", Usuario.Contrasena);
                cmd.Parameters.AddWithValue("@RolID", Usuario.RolID);
                cmd.Parameters.AddWithValue("@Status", Usuario.Status);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }

        // Eliminar Usuario por ID
        public bool EliminarUsuario(int idUsuario)
        {
            int filasAfectadas = 0;
            string query = "DELETE FROM Usuarios WHERE IDUsuario = @IDUsuario";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@IDUsuario", idUsuario);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }
        //Obtiene la informacion de un Usuario por id
        public UsuarioEnt ObtenerUsuarioPorID(int idUsuario)
        {
            UsuarioEnt Usuario = null;

            string query = "SELECT IDUsuario, Nombre, Correo, Contrasena, RolID, Status FROM Usuarios WHERE IDUsuario = @idUsuario";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Usuario = new UsuarioEnt()
                        {
                            IDUsuario = reader.GetInt32(0),
                            Nombre = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Correo = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Contrasena = reader.IsDBNull(3) ? null : reader.GetString(3),
                            RolID = reader.GetInt32(4),
                            Status = reader.IsDBNull(5) ? null : reader.GetString(5)
                        };
                    }
                }
            }

            return Usuario;
        }
        //metodo para actualizar un Usuario
        public bool ActualizarUsuario(UsuarioEnt Usuario)
        {
            int filasAfectadas = 0;

            string query = @"UPDATE Usuarios
                     SET Nombre = @Nombre, 
                         Status = @Status,
                         RolID = @RolID
                     WHERE IDUsuario = @ID";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", Usuario.Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", Usuario.Status ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RolID", Usuario.RolID == 0 ? (object)DBNull.Value : Usuario.RolID);
                cmd.Parameters.AddWithValue("@ID", Usuario.IDUsuario);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }
        //valida que el correo no exista
        public bool ValidaCorreoExiste(string correo)
        {
            UsuarioEnt Usuario = null;

            string query = "SELECT IDUsuario FROM Usuarios WHERE Correo = @Correo";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Correo", correo);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Usuario = new UsuarioEnt()
                        {
                            IDUsuario = reader.GetInt32(0)
                        };
                    }
                }
            }
            if (Usuario == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //Metodo que valida la contraseña corresponda al id
        public bool ValidaContraPorID(int id, string contra)
        {
            UsuarioEnt Usuario = null;

            string query = "SELECT IDUsuario FROM Usuarios WHERE IDUsuario = @IDUsuario and Contrasena = @Contrasena";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@IDUsuario", id);
                cmd.Parameters.AddWithValue("@Contrasena", contra);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Usuario = new UsuarioEnt()
                        {
                            IDUsuario = reader.GetInt32(0)
                        };
                    }
                }
            }
            if (Usuario == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //metodo para actualizar un Usuario
        public bool CambiarContra(int id, string contra)
        {
            int filasAfectadas = 0;

            string query = @"UPDATE Usuarios
                     SET Contrasena = @Contrasena
                     WHERE IDUsuario = @ID";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Contrasena", contra);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }
    }
}