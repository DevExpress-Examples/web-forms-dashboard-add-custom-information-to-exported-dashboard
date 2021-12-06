<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebDashboard_CustomExport.Default" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="position: absolute; top: 0; left: 0; right: 0; bottom: 0;">
        <dx:ASPxDashboard ID="ASPxDashboard1" runat="server" Width="100%" Height="100%"
            AllowExportDashboardItems="True"
            OnConfigureDataConnection="ASPxDashboard1_ConfigureDataConnection"            
            OnCustomExport="ASPxDashboard1_CustomExport">
        </dx:ASPxDashboard>
    </div>
    </form>
</body>
</html>