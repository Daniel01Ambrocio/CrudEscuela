<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="SysEscolar.Presentation.index" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login</title>
    <!-- Enlace a Bootstrap (CDN) -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="d-flex align-items-center justify-content-center" style="height: 100vh;">

    <div class="card shadow" style="width: 20rem;">
        <div class="card-body">
            <h5 class="card-title text-center">Iniciar Sesión</h5>
            <form id="loginForm" runat="server">
                <div class="form-group">
                    <label for="email">Correo Electrónico</label>
                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" Placeholder="ejemplo@correo.com" TextMode="Email" required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="password">Contraseña</label>
                    <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="********" required="true"></asp:TextBox>
                </div>
                <div class="form-group text-center">
                    <asp:Button ID="LoginButton" runat="server" Text="Iniciar Sesión" CssClass="btn btn-primary btn-block" OnClick="LoginButton_Click" />
                </div>
            </form>
        </div>
    </div>

    <!-- Scripts de Bootstrap -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

</body>
</html>