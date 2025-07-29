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
    public class DivisionesDLL
    {
        string conexion = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

        // Obtener todas las divisiones
        public DataTable ObtenerDivisiones()
        {
            DataTable dt = new DataTable();

            string query = "SELECT IDDivision, NombreDivision, DescripcionDiv FROM Divisiones";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }
        //Agregar una nueva division
        public bool AgregarDivision(DivisionEnt division)
        {
            int filasAfectadas = 0;
            string query = "INSERT INTO Divisiones (NombreDivision, DescripcionDiv) VALUES (@Nombre, @Descripcion)";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", division.NombreDivision);
                cmd.Parameters.AddWithValue("@Descripcion", division.DescripcionDiv);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }

        // Eliminar división por ID
        public bool EliminarDivision(int idDivision)
        {
            int filasAfectadas = 0;
            string query = "DELETE FROM Divisiones WHERE IDDivision = @IDDivision";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@IDDivision", idDivision);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }
        //Obtiene la informacion de una division por id
        public DivisionEnt ObtenerDivisionPorID(int idDivision)
        {
            DivisionEnt division = null;

            string query = "SELECT IDDivision, NombreDivision, DescripcionDiv FROM Divisiones WHERE IDDivision = @idDivision";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idDivision", idDivision);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        division = new DivisionEnt
                        {
                            IDDivision = reader.GetInt32(0),
                            NombreDivision = reader.IsDBNull(1) ? null : reader.GetString(1),
                            DescripcionDiv = reader.IsDBNull(2) ? null : reader.GetString(2)
                        };
                    }
                }
            }

            return division;
        }
        //metodo para actualizar una division
        public bool ActualizarDivision(DivisionEnt division)
        {
            int filasAfectadas = 0;

            string query = @"UPDATE Divisiones
                     SET NombreDivision = @Nombre, DescripcionDiv = @Descripcion
                     WHERE IDDivision = @ID";

            using (SqlConnection conn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", division.NombreDivision ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion", division.DescripcionDiv ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ID", division.IDDivision);

                conn.Open();
                filasAfectadas = cmd.ExecuteNonQuery();
            }

            return filasAfectadas > 0;
        }

    }
}