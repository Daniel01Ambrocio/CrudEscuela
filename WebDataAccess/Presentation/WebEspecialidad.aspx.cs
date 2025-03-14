using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Data.SqlClient.SqlConnection;
using static System.Data.SqlClient.SqlCommand;
using WebDataAccess.BLL;

namespace WebDataAccess.Presentation
{
    public partial class WebEspecialidad : System.Web.UI.Page
    {
        private readonly EspecialidadBLL EspecialidadBLL = new EspecialidadBLL();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await CargarDatosAsync();
            }
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            DataTable dtatrapa = null;
            try
            {
                dtatrapa = await EspecialidadBLL.ObtenerEspecialidadAsync();

                if (dtatrapa != null)
                {
                    if (dtatrapa.Rows.Count > 0)
                    {
                        GridView2.DataSource = dtatrapa;
                        GridView2.DataBind();
                    }
                    else
                    {
                        // La consulta fue correcta pero no hay resultados
                        NoHayResultados(dtatrapa, GridView2);
                    }
                }
            }
            catch (Exception ex)
            {
                TextBox1.Text = "Error: " + ex.Message;
            }
        }

        protected async void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;

            // Cargar los datos de manera asincrónica
            await CargarDatosAsync();
        }

        protected async void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int idespecialidad = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Values["IDespecialidad"]);
                string nuevoNombre = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
                string nuevaDescripcion = ((TextBox)GridView2.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
                int nuevadivision = int.Parse(((TextBox)GridView2.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
                await EspecialidadBLL.ActualizarEspecialidadAsync(idespecialidad, nuevoNombre, nuevaDescripcion, nuevadivision);

                GridView2.EditIndex = -1;

                await CargarDatosAsync();
            }
            catch (Exception ex)
            {
                TextBox1.Text = "Error al actualizar: " + ex.Message;
            }
        }

        protected async void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;

            // Volver a cargar los datos directamente aquí
            await CargarDatosAsync();
        }

        protected async void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < GridView2.Rows.Count)
            {
                try
                {
                    int AulaID = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Values["IDespecialidad"]);
                    await EspecialidadBLL.EliminarEspecialidadAsync(AulaID);

                    await CargarDatosAsync();
                }
                catch (Exception ex)
                {
                    TextBox1.Text = "Error al eliminar: " + ex.Message;
                }
            }
        }

        private void NoHayResultados(DataTable resulconsul, GridView gv1)
        {
            try
            {
                // Se va a agregar un nuevo renglón vacío al datatable
                resulconsul.Rows.Add(resulconsul.NewRow());
                gv1.DataSource = resulconsul;
                gv1.DataBind();

                // Modificar el GridView para que salga un mensaje sobre el renglón vacío
                int totalColumnas = gv1.Columns.Count;
                gv1.Rows[0].Cells.Clear(); // Eliminamos todas las celdas
                gv1.Rows[0].Cells.Add(new TableCell());

                // Vamos a decirle que esa celda vacía ocupe el espacio de todas las vacías
                gv1.Rows[0].Cells[0].ColumnSpan = totalColumnas;

                // Cambiar el estilo para que se note el mensaje
                gv1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                gv1.Rows[0].Cells[0].ForeColor = Color.Red;
                gv1.Rows[0].Cells[0].Font.Bold = true;

                // El mensaje dentro de la celda
                gv1.Rows[0].Cells[0].Text = "No hay resultados!";
            }
            catch (Exception ex)
            {
                TextBox1.Text = "Error al mostrar mensaje de no hay resultados: " + ex.Message;
            }
        }

        private async Task CargarDatosAsync()
        {
            DataTable dtatrapa = await EspecialidadBLL.ObtenerEspecialidadAsync();

            if (dtatrapa != null)
            {
                if (dtatrapa.Rows.Count > 0)
                {
                    GridView2.DataSource = dtatrapa;
                    GridView2.DataBind();
                }
                else
                {
                    NoHayResultados(dtatrapa, GridView2);
                }
            }
        }
        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private async Task ButtonClickAsync()
        {
            await Task.Delay(1000); // Simulación de espera de 1 segundos

            // Actualizar UI u otras acciones después del clic del botón
            TextBox1.Text = "Procesando accion...";
        }
        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int a = 0; a < e.Row.Cells.Count - 1; a++)
                {
                    if (a == 0 || a == 1)
                    {
                        // Crear TextBox para columnas 1 y 2
                        TextBox tb = new TextBox();
                        tb.ID = "tbd" + a;
                        tb.CssClass = "form-control short-textbox"; // Aplicar estilo de Bootstrap
                        e.Row.Cells[a].Controls.Add(tb);
                    }
                    else if (a == 2)
                    {
                        // Crear TextBox para Número (tipo number)
                        TextBox tb = new TextBox();
                        tb.ID = "tbd" + a;
                        tb.CssClass = "form-control short-textbox"; // Aplicar estilo de Bootstrap
                        tb.Attributes["type"] = "number"; // Establecer el tipo de entrada como número
                        e.Row.Cells[a].Controls.Add(tb);
                    }

                    if (a == e.Row.Cells.Count - 2)
                    {
                        // Crear Button para Agregar
                        Button btd = new Button();
                        btd.ID = "btnNew";
                        btd.Text = "Agregar...";
                        e.Row.Cells[a].Controls.Add(btd);
                        btd.CssClass = "btn btn-primary"; // Aplicar estilo de Bootstrap
                        btd.Click += btd_Click;

                        // Registrar la tarea asincrónica
                        RegisterAsyncTask(new PageAsyncTask(ButtonClickAsync));
                    }
                }
            }
        }


        protected async void btd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow registro = (GridViewRow)btn.NamingContainer;

            if (registro != null)
            {
                try
                {
                    btn.Enabled = false;

                    await EspecialidadBLL.InsertarEspecialidadAsync(registro);

                    DataTable dtatrapa = await EspecialidadBLL.ObtenerEspecialidadAsync();

                    if (dtatrapa != null && dtatrapa.Rows.Count > 0)
                    {
                        GridView2.DataSource = dtatrapa;
                        GridView2.DataBind();
                    }
                    else
                    {
                        NoHayResultados(dtatrapa, GridView2);
                    }
                }
                finally
                {
                    btn.Enabled = true;
                }
            }
        }
    }
}