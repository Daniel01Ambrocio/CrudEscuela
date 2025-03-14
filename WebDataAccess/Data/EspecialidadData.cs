using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebDataAccess.Data
{
    public class EspecialidadData
    {
        public async Task<DataTable> MostrarDatosGridViewAsync(string query)
        {
            DataTable dtsalida = null;
            SqlConnection con4 = null;

            try
            {
                // Abrir conexión de manera asincrónica
                con4 = await OpenConnectionAsync();

                if (con4 != null)
                {
                    SqlCommand carrito2 = new SqlCommand();
                    carrito2.Connection = con4;
                    carrito2.CommandText = query;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = carrito2;

                    // Contenedor DataSet
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);
                    dtsalida = ds.Tables[0];
                }
            }
            catch (Exception z)
            {
                // Manejo de errores
                // (Puedes ajustar según tus necesidades)
                throw new Exception("Error al acceder a la base de datos: " + z.Message);
            }
            finally
            {
                // Cerrar conexión de manera asincrónica
                if (con4 != null && con4.State == ConnectionState.Open)
                {
                    con4.Close();
                    con4.Dispose();
                }
            }

            return dtsalida;
        }

        public async Task InsertaEspecialidadAsync(string nombre, string descripcion, int division)
        {
            using (SqlConnection con5 = await OpenConnectionAsync())
            {
                if (con5 != null)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con5;
                        cmd.CommandText = "INSERT INTO Especialidades(NombresEspecialidad, DescripcionEsp, DivisionID) VALUES (@Nombre, @Descripcion, @Division)";
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@Division", division);
                        try
                        {
                            await cmd.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error al insertar en la base de datos: " + ex.Message);
                        }
                    }
                }
            }
        }

        public async Task ActualizarEspecialidadPorIDAsync(int EspecialidadID, string nuevoNombre, string nuevaDescripcion, int nuevadivision)
        {
            using (SqlConnection con7 = await OpenConnectionAsync())
            {
                if (con7 != null)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con7;
                        cmd.CommandText = "UPDATE Especialidades SET NombresEspecialidad = @NuevoNombre, DescripcionEsp = @NuevaDescripcion ,DivisionID = @nuevadivision WHERE IDespecialidad = @EspecialidadID";
                        cmd.Parameters.AddWithValue("@EspecialidadID", EspecialidadID);
                        cmd.Parameters.AddWithValue("@NuevoNombre", nuevoNombre);
                        cmd.Parameters.AddWithValue("@NuevaDescripcion", nuevaDescripcion);
                        cmd.Parameters.AddWithValue("@nuevadivision", nuevadivision);
                        try
                        {
                            await cmd.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            // Manejo de errores
                            // (Puedes ajustar según tus necesidades)
                            throw new Exception("Error al actualizar en la base de datos: " + ex.Message);
                        }
                    }
                }
            }
        }

        public async Task EliminarEspecialidadPorIDAsync(int EspecialidadID)
        {
            using (SqlConnection con6 = await OpenConnectionAsync())
            {
                if (con6 != null)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con6;
                        cmd.CommandText = @"DELETE FROM Especialidades WHERE IDespecialidad = @EspecialidadID";
                        cmd.Parameters.AddWithValue("@EspecialidadID", EspecialidadID);

                        try
                        {
                            await cmd.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            // Manejo de errores
                            // (Puedes ajustar según tus necesidades)
                            throw new Exception("Error al eliminar en la base de datos: " + ex.Message);
                        }
                    }
                }
            }
        }

        public async Task<SqlConnection> OpenConnectionAsync()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;

            try
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }

                // Abrir conexión de manera asincrónica
                await connection.OpenAsync();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // (Puedes ajustar según tus necesidades)
                connection = null;
                throw new Exception("Error al conectar a la base de datos: " + ex.Message);
            }

            return connection;
        }
    }
}