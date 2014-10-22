<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Control.aspx.cs" Inherits="MobileTaasClient.Control" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Control</h1>
    </div>
    <div class="page-header">
        <h2>Details</h2>
        <h4><asp:Label ID="txtEmulatorId" Text="" runat="server"></asp:Label></h4>
    </div>
    <div id="tabs">
        <ul>
            <li><a href="#interaction">Interaction</a></li>
            <li><a href="#view">View</a></li>
            <li><a href="#poweroff">Power</a></li>
        </ul>
        <div id="interaction">
            <p class="lead">
                Interaction 
            </p>
            <h4>Launch URL
            </h4>
            <asp:TextBox ID="txtLaunchUrl" runat="server"></asp:TextBox><asp:Button ID="btnLaunchUrl" runat="server" Text="Launch URL" OnClick="btnLaunchUrl_Click" />
            <br />
            <h4>
                Intent 
            </h4>
            <asp:TextBox ID="txtLaunchIntent" runat="server"></asp:TextBox><asp:Button ID="btnLaunchIntent" runat="server" Text="Launch App" OnClick="btnLaunchIntent_Click" />
            <h4>
                Install Application 
            </h4>
            <asp:FileUpload ID="apkUpload" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Install App" OnClick="btnUpload_Click" />
        </div>
        <div id="view">
            <asp:Button ID="btnRefreshImage" runat="server" Text="Refresh Image" OnClick="btnRefreshImage_Click" />
            <asp:Image ID="imageData" runat="server" />
        </div>
        <div id="poweroff">
            <asp:Button ID="btnPowerOff" runat="server" Text="Power Off" OnClick="btnPowerOff_Click" />
        </div>
    </div>

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#tabs").tabs();
        });
    </script>
</asp:Content>
