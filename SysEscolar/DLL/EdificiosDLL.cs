using SysEscolar.Entities;
using SysEscolar.Presentation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SysEscolar.DLL
{
    public class EdificiosDLL
    {
        string conexion = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

        // Obtener todas los edificios
        public DataTable ObtenerEdificios()
        {
            DataTable dt = new DataTable();

            string query = @"
            SELECT 
                e.IDEdificio, 
                e.NombreEdificio, 
                e.DescripcionEdif,
                d.NombreDivision
            FROM 
                Edificios e
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
        //Agregar un nuevo edificio
        public bool AgregarEdificio(EdificioEnt edificio)
        {
            int filasAfectadas = 0;
            string query = "INSERT INTO Edificios (NombreEdificio, DescripcionEdif, DivisionID) VALUES (@Nombre, @Descripcion, @DivisionID)";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", edificio.NombreEdificio);
                cmd.Parameters.AddWithValue("@Descripcion", edificio.DescripcionEdif);
                cmd.Parameters.AddWithValue("@DivisionID", edificio.DivisionID);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }

        // Eliminar Edificio por ID
        public bool EliminarEdificio(int idEdificio)
        {
            int filasAfectadas = 0;
            string query = "DELETE FROM Edificios WHERE IDEdificio = @IDEdificio";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@IDEdificio", idEdificio);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }
        //Obtiene la informacion de un Edificio por id
        public EdificioEnt ObtenerEdificioPorID(int idEdificio)
        {
            EdificioEnt edificio = null;

            string query = "SELECT IDEdificio, NombreEdificio, DescripcionEdif, DivisionID FROM Edificios WHERE IDEdificio = @idEdificio";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idEdificio", idEdificio);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        edificio = new EdificioEnt()
                        {
                            IDEdificio = reader.GetInt32(0),
                            NombreEdificio = reader.IsDBNull(1) ? null : reader.GetString(1),
                            DescripcionEdif = reader.IsDBNull(2) ? null : reader.GetString(2),
                            DivisionID = reader.GetInt32(3)
                        };
                    }
                }
            }

            return edificio;
        }
        //metodo para actualizar un edificio
        public bool ActualizarEdificio(EdificioEnt edificio)
        {
            int filasAfectadas = 0;

            string query = @"UPDATE Edificios
                     SET NombreEdificio = @Nombre, 
                         DescripcionEdif = @Descripcion, 
                         DivisionID = @DivisionID
                     WHERE IDEdificio = @ID";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", edificio.NombreEdificio ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion", edificio.DescripcionEdif ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DivisionID", edificio.DivisionID == 0 ? (object)DBNull.Value : edificio.DivisionID);
                cmd.Parameters.AddWithValue("@ID", edificio.IDEdificio);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }
        //valida que el iddivision no este referenciado 
        public bool BuscaDivisionForanea(int iddivision)
        {
            EdificioEnt edificio = null;

            string query = "SELECT IDEdificio FROM Edificios WHERE DivisionID = @idDivision";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idDivision", iddivision);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        edificio = new EdificioEnt()
                        {
                            IDEdificio = reader.GetInt32(0)
                        };
                    }
                }
            }
            if (edificio == null)
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