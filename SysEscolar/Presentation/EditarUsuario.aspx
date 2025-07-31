<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarUsuario.aspx.cs" Inherits="SysEscolar.Presentation.EditarUsuario" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Editar Usuario</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form2" runat="server">
        <div class="container">
            <div class="row justify-content-center align-items-center min-vh-100">
                <div class="col-12 col-sm-10 col-md-6 col-lg-5">
                    <div class="bg-white p-4 rounded shadow">
                        <h3 class="text-center text-primary mb-4">Actualizar Usuario</h3>

                        <div class="mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtNombre" Text="Nombre Usuario" CssClass="form-label" />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" AssociatedControlID="ddlStatus" Text="Estatus" CssClass="form-label" />
                            <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" AssociatedControlID="ddlRol" Text="Rol" CssClass="form-label" />
                            <asp:DropDownList ID="ddlRol" runat="server"></asp:DropDownList>
                        </div>

                        <div class="d-grid gap-2">
                            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Usuario" CssClass="btn btn-success" OnClick="btnActualizar_Click" />
                            <asp:Button ID="btnAtras" runat="server" Text="Regresar" CssClass="btn btn-outline-secondary" OnClick="btnAtras_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS (opcional si necesitas interactividad) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

