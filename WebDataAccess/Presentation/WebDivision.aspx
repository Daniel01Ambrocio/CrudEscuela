<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebDivision.aspx.cs" Inherits="WebDataAccess.WebDivision"  Async="true"  %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Divisiones</title>
<link rel="stylesheet" type="text/css" href="../Styles/styles.css"/></head>
<body>
    
    <form id="form1" runat="server" class="container mt-5">
            <div class="menu-container">
    <ul class="menu-list">
        <li class="menu-item"><a class="menu-link" href="WebDivision.aspx">CRUD Division</a></li>
        <li class="menu-item"><a class="menu-link" href="WebEspecialidad.aspx">CRUD Especialidad</a></li>
        <li class="menu-item"><a class="menu-link" href="WebEdificio.aspx">CRUD Edificio</a></li>
    </ul>
</div>
        <h2 class="mb-4">CRUD de Divisiones</h2>
       <div class="page-container">
            <asp:Button ID="Button1" runat="server" Text="Ejecutar" OnClick="Button1_Click" CssClass="btn btn-primary" />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                DataKeyNames="IDdivision"
                OnRowCreated="GridView1_RowCreated"
                OnRowEditing="GridView1_RowEditing"
                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                OnRowUpdating="GridView1_RowUpdating" ShowFooter="True"
                OnRowDeleting="GridView1_RowDeleting"
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="NombreDivision" HeaderText="Division" ItemStyle-CssClass="font-weight-bold" />
                    <asp:BoundField DataField="DescripcionDiv" HeaderText="Descripción" />
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ItemStyle-CssClass="text-center" />
                </Columns>
            </asp:GridView>
            <br />
            <br />
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ReadOnly="true" Width="354px"></asp:TextBox>
        </div>
    </form>
</body>
</html>