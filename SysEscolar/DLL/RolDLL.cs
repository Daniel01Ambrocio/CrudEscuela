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
    public class RolDLL
    {
        string conexion = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        //valida que las credenciales existan a un usuario
        public RolEnt ObtenerRolPorID(int idrol)
        {
            RolEnt rol = null;

            // Corregimos la consulta SQL, especificando las columnas correctas
            string query = "SELECT NombreRol FROM Roles WHERE IDRol = @idrol";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Parámetros de la consulta
                cmd.Parameters.AddWithValue("@idrol", idrol);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Asignamos los valores a la instancia de rolent
                        rol = new RolEnt()
                        {
                            NombreRol = reader.GetString(0)
                        };
                    }
                }
            }

            return rol;
        }
        // Obtener todas los roles
        public DataTable ObtenerRoles()
        {
            DataTable dt = new DataTable();

            string query = @"
            SELECT IDRol, NombreRol from roles";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }
    }
}