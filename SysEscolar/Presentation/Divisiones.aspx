<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/Menu.Master" AutoEventWireup="true" CodeBehind="Divisiones.aspx.cs" Inherits="SysEscolar.Presentation.Divisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <!-- Formulario lado izquierdo 1/3 -->
            <div class="col-12 col-md-4 mb-4">
                <h3 class="mb-3">Agregar Nueva División</h3>
                <div class="border p-3 rounded shadow-sm bg-light">
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtNombre" Text="Nombre División" CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtDescripcion" Text="Descripción" CssClass="form-label" />
                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                    </div>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar División" CssClass="btn btn-primary w-100" OnClick="btnAgregar_Click" />
                </div>
            </div>

            <!-- GridView lado derecho 2/3 -->
            <div class="col-12 col-md-8">
                <h2 class="mb-4">Divisiones</h2>
                <div class="table-responsive">
                    <asp:GridView ID="gvDivisiones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" DataKeyNames="IDDivision" OnRowCommand="gvDivisiones_RowCommand">
                        <columns>
                            <asp:BoundField DataField="IDDivision" HeaderText="ID" />
                            <asp:BoundField DataField="NombreDivision" HeaderText="Nombre" />
                            <asp:BoundField DataField="DescripcionDiv" HeaderText="Descripción" />
                            <asp:TemplateField HeaderText="Acciones">
                                <itemtemplate>
                                    <div class="d-flex gap-2">
                                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("IDDivision") %>' Text="Editar" CssClass="btn btn-warning btn-sm" />
                                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IDDivision") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Estás seguro de eliminar este registro?');" />
                                    </div>
                                </itemtemplate>

                            </asp:TemplateField>
                        </columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
