<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarContraseña.aspx.cs" Inherits="SysEscolar.Presentation.CambiarContraseña" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Cambiar Contraseña</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center align-items-center min-vh-100">
                <div class="col-12 col-sm-8 col-md-6 col-lg-4">
                    <div class="bg-white p-4 rounded-3 shadow-sm">
                        <h3 class="text-center text-primary mb-4">Actualizar Contraseña</h3>

                        <div class="mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtContraAnterior" Text="Contraseña anterior" CssClass="form-label" />
                            <asp:TextBox ID="txtContraAnterior" runat="server" TextMode="Password" CssClass="form-control" Placeholder="******" />
                        </div>

                        <div class="mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtNueva" Text="Nueva contraseña" CssClass="form-label" />
                            <asp:TextBox ID="txtNueva" runat="server" CssClass="form-control" TextMode="Password" Placeholder="******" />
                        </div>

                        <div class="mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtConfirma" Text="Confirma la contraseña" CssClass="form-label" />
                            <asp:TextBox ID="txtConfirma" runat="server" TextMode="Password" CssClass="form-control" Placeholder="******" />
                        </div>

                        <div class="d-grid gap-2">
                            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Contraseña" CssClass="btn btn-success" OnClick="btnActualizar_Click"/>
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