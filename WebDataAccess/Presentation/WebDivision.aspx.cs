using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDataAccess.BLL;

namespace WebDataAccess
{
    public partial class WebDivision : System.Web.UI.Page
    {
        private readonly DivisionBLL DivisionBLL = new DivisionBLL();

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
                dtatrapa = await DivisionBLL.ObtenerDivisionesAsync();

                if (dtatrapa != null)
                {
                    if (dtatrapa.Rows.Count > 0)
                    {
                        GridView1.DataSource = dtatrapa;
                        GridView1.DataBind();
                    }
                    else
                    {
                        // La consulta fue correcta pero no hay resultados
                        NoHayResultados(dtatrapa, GridView1);
                    }
                }
            }
            catch (Exception ex)
            {
                TextBox1.Text = "Error: " + ex.Message;
            }
        }

        protected async void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            // Cargar los datos de manera asincrónica
            await CargarDatosAsync();
        }

        protected async void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int divisionID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["IDdivision"]);
                string nuevoNombre = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
                string nuevaDescripcion = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

                await DivisionBLL.ActualizarDivisionAsync(divisionID, nuevoNombre, nuevaDescripcion);

                GridView1.EditIndex = -1;

                await CargarDatosAsync();
            }
            catch (Exception ex)
            {
                TextBox1.Text = "Error al actualizar: " + ex.Message;
            }
        }

        protected async void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;

            // Volver a cargar los datos directamente aquí
            await CargarDatosAsync();
        }

        protected async void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < GridView1.Rows.Count)
            {
                try
                {
                    int divisionID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["IDdivision"]);
                    await DivisionBLL.EliminarDivisionAsync(divisionID);

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
            DataTable dtatrapa = await DivisionBLL.ObtenerDivisionesAsync();

            if (dtatrapa != null)
            {
                if (dtatrapa.Rows.Count > 0)
                {
                    GridView1.DataSource = dtatrapa;
                    GridView1.DataBind();
                }
                else
                {
                    NoHayResultados(dtatrapa, GridView1);
                }
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private async Task ButtonClickAsync()
        { 
            await Task.Delay(1000); // Simulación de espera de 1 segundos

            // Actualizar UI u otras acciones después del clic del botón
            TextBox1.Text = "Procesando accion...";
        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int a = 0; a < e.Row.Cells.Count - 1; a++)
                {
                    // Crear TextBox para Nombre
                    TextBox tb = new TextBox();
                    tb.ID = "tbd" + a;
                    tb.CssClass = "form-control short-textbox"; // Aplicar estilo de Bootstrap
                    e.Row.Cells[a].Controls.Add(tb);

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

                    await DivisionBLL.InsertarDivisionAsync(registro);

                    DataTable dtatrapa = await DivisionBLL.ObtenerDivisionesAsync();

                    if (dtatrapa != null && dtatrapa.Rows.Count > 0)
                    {
                        GridView1.DataSource = dtatrapa;
                        GridView1.DataBind();
                    }
                    else
                    {
                        NoHayResultados(dtatrapa, GridView1);
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