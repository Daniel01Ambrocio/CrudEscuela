<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebEdificio.aspx.cs" Inherits="WebDataAccess.Presentation.WebEdificio" Async="true"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>CRUD de Edificios</title>
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
        <h2 class="mb-4">CRUD de Edificios</h2>
       <div class="page-container">
            <asp:Button ID="Button1" runat="server" Text="Ejecutar" OnClick="Button1_Click" CssClass="btn btn-primary" />
            <br />
            <br />
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"
                DataKeyNames="IDedificio"
                OnRowCreated="GridView3_RowCreated"
                OnRowEditing="GridView3_RowEditing"
                OnRowCancelingEdit="GridView3_RowCancelingEdit"
                OnRowUpdating="GridView3_RowUpdating" ShowFooter="True"
                OnRowDeleting="GridView3_RowDeleting"
                OnSelectedIndexChanged="GridView3_SelectedIndexChanged" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="NombreEdificio" HeaderText="Edificio" ItemStyle-CssClass="font-weight-bold" />
                    <asp:BoundField DataField="DescripcionEdif" HeaderText="Descripción" />
                     <asp:BoundField DataField="DivisionID" HeaderText="Division" />
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