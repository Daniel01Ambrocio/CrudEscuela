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
    public class EspecialidadDLL
    {
        string conexion = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

        // Obtener todas las Especialidades
        public DataTable ObtenerEspecialidades()
        {
            DataTable dt = new DataTable();

            string query = @"
        SELECT 
            e.IDEspecialidad, 
            e.NombresEspecialidad, 
            e.DescripcionEsp,
            d.NombreDivision
        FROM 
            Especialidades e
        LEFT JOIN 
            Divisiones d ON e.DivisionID = d.IDDivision";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        // Agregar una nueva Especialidad
        public bool AgregarEspecialidad(EspecialidadEnt especialidad)
        {
            int filasAfectadas = 0;
            string query = "INSERT INTO Especialidades (NombresEspecialidad, DescripcionEsp, DivisionID) VALUES (@Nombre, @Descripcion, @DivisionID)";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", especialidad.NombresEspecialidad);
                cmd.Parameters.AddWithValue("@Descripcion", especialidad.DescripcionEsp);
                cmd.Parameters.AddWithValue("@DivisionID", especialidad.DivisionID);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }

        // Eliminar Especialidad por ID
        public bool EliminarEspecialidad(int idEspecialidad)
        {
            int filasAfectadas = 0;
            string query = "DELETE FROM Especialidades WHERE IDEspecialidad = @IDEspecialidad";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@IDEspecialidad", idEspecialidad);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }

        // Obtener la información de una Especialidad por ID
        public EspecialidadEnt ObtenerEspecialidadPorID(int idEspecialidad)
        {
            EspecialidadEnt especialidad = null;

            string query = "SELECT IDEspecialidad, NombresEspecialidad, DescripcionEsp, DivisionID FROM Especialidades WHERE IDEspecialidad = @idEspecialidad";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idEspecialidad", idEspecialidad);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        especialidad = new EspecialidadEnt()
                        {
                            IDEspecialidad = reader.GetInt32(0),
                            NombresEspecialidad = reader.IsDBNull(1) ? null : reader.GetString(1),
                            DescripcionEsp = reader.IsDBNull(2) ? null : reader.GetString(2),
                            DivisionID = reader.GetInt32(3)
                        };
                    }
                }
            }

            return especialidad;
        }

        // Actualizar una Especialidad
        public bool ActualizarEspecialidad(EspecialidadEnt especialidad)
        {
            int filasAfectadas = 0;

            string query = @"
        UPDATE Especialidades
        SET NombresEspecialidad = @Nombre, 
            DescripcionEsp = @Descripcion, 
            DivisionID = @DivisionID
        WHERE IDEspecialidad = @ID";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", especialidad.NombresEspecialidad ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion", especialidad.DescripcionEsp ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DivisionID", especialidad.DivisionID == 0 ? (object)DBNull.Value : especialidad.DivisionID);
                cmd.Parameters.AddWithValue("@ID", especialidad.IDEspecialidad);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }
        //valida que el iddivision no este referenciado 
        public bool BuscaDivisionForanea(int iddivision)
        {
            EspecialidadEnt especialidad = null;

            string query = "SELECT IDEspecialidad FROM Especialidades WHERE DivisionID = @divisionid";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@divisionid", iddivision);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        especialidad = new EspecialidadEnt()
                        {
                            IDEspecialidad = reader.GetInt32(0)
                        };
                    }
                }
            }
            if(especialidad == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}