<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/Menu.Master" AutoEventWireup="true" CodeBehind="Edificios.aspx.cs" Inherits="SysEscolar.Presentation.Edificios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <!-- Formulario lado izquierdo 1/3 -->
            <div class="col-12 col-md-4 mb-4">
                <h3 class="mb-4 text-info">Agregar Nuevo Edificio</h3>
                <div class="border p-3 rounded shadow-sm bg-light">
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtNombre" Text="Nombre Edificio" CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtDescripcion" Text="Descripción" CssClass="form-label" />
                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="ddlDivision" Text="División" CssClass="form-label" />
                        <asp:DropDownList ID="ddlDivision" runat="server"></asp:DropDownList>
                    </div>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Edificio" CssClass="btn btn-primary w-100" OnClick="btnAgregar_Click" />
                </div>
            </div>

            <!-- GridView lado derecho 2/3 -->
            <div class="col-12 col-md-8">
                <h2 class="mb-4 text-info">Edificios</h2>
                <div class="table-responsive">
                    <asp:GridView ID="gvEdificios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="IDEdificio" OnRowCommand="gvEdificios_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="IDEdificio" HeaderText="ID" />
                            <asp:BoundField DataField="NombreEdificio" HeaderText="Nombre" />
                            <asp:BoundField DataField="DescripcionEdif" HeaderText="Descripción" />
                            <asp:BoundField DataField="NombreDivision" HeaderText="División" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <div class="d-flex gap-2">
                                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("IDEdificio") %>' Text="Editar" CssClass="btn btn-warning btn-sm" />
                                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IDEdificio") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Deseas eliminar este edificio?');" />
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
