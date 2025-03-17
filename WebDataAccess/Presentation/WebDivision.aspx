<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebDivision.aspx.cs" Inherits="WebDataAccess.WebDivision"  Async="true"  %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Divisiones</title>
    <!-- Bootstrap CDN -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
    <link rel="stylesheet" type="text/css" href="../Styles/styles.css"/>
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">

        <h2 class="mb-4">CRUD de Divisiones</h2>

        <!-- Botón de ejecutar -->
        <asp:Button ID="Button1" runat="server" Text="Ejecutar" OnClick="Button1_Click" CssClass="btn btn-primary mb-4" Visible="False" />

        <!-- GridView con estilos Bootstrap -->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="IDdivision"
            OnRowCreated="GridView1_RowCreated"
            OnRowEditing="GridView1_RowEditing"
            OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowUpdating="GridView1_RowUpdating" ShowFooter="True"
            OnRowDeleting="GridView1_RowDeleting"
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
            CssClass="table table-striped table-bordered text-center">
            <Columns>
                <asp:BoundField DataField="NombreDivision" HeaderText="Division" ItemStyle-CssClass="font-weight-bold" />
                <asp:BoundField DataField="DescripcionDiv" HeaderText="Descripción" />
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ItemStyle-CssClass="text-center" />
            </Columns>
        </asp:GridView>

        <!-- Campo de texto -->
        <div class="form-group mt-4">
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ReadOnly="true" placeholder="Selecciona una división..." Wrap="False" />
        </div>
    </form>

    <!-- Bootstrap JS y dependencias -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
