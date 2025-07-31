<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/Menu.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="SysEscolar.Presentation.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <!-- Formulario lado izquierdo 1/3 -->
            <div class="col-12 col-md-4 mb-4">
                <h3 class="mb-4 text-info">Agregar Nuevo Usuario</h3>
                <div class="border p-3 rounded shadow-sm bg-light">
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtNombre" Text="Nombre Usuario" CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtCorreo" Text="Correo electronico" CssClass="form-label" />
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtContra" Text="Contraseña" CssClass="form-label" />
                        <asp:TextBox ID="txtContra" runat="server" CssClass="form-control" TextMode="Password" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtContraConfirma" Text="Confirma la contraseña" CssClass="form-label" />
                        <asp:TextBox ID="txtContraConfirma" runat="server" CssClass="form-control" TextMode="Password" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="ddlRol" Text="Rol" CssClass="form-label" />
                        <asp:DropDownList ID="ddlRol" runat="server"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="ddlEstatus" Text="Estatus" CssClass="form-label" />
                        <asp:DropDownList ID="ddlEstatus" runat="server"></asp:DropDownList>
                    </div>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Usuario" CssClass="btn btn-primary w-100" OnClick="btnAgregar_Click" />
                </div>
            </div>

            <!-- GridView lado derecho 2/3 -->
            <div class="col-12 col-md-8">
                <h2 class="mb-4 text-info">Usuarios</h2>
                <div class="table-responsive">
                    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" DataKeyNames="IDUsuario" OnRowCommand="gvUsuarios_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="IDUsuario" HeaderText="ID" SortExpression="IDUsuario" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                            <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                            <asp:BoundField DataField="NombreRol" HeaderText="Rol" SortExpression="NombreRol" />
                            <asp:BoundField DataField="Status" HeaderText="Estatus" SortExpression="Status" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <div class="d-flex gap-2">
                                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("IDUsuario") %>' Text="Editar" CssClass="btn btn-warning btn-sm" />
                                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IDUsuario") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Deseas eliminar este Usuario?');" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>


        </div>
    </div>
</asp:Content>
