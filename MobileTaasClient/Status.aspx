<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Status.aspx.cs" Inherits="MobileTaasClient.Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .floatingBox {
            display:inline-block;
            width: 420px;
            height: 200px;
         }
        .gBox {
            float:left;
            width: 200px;
            height: 160px;
         }
    </style>
    <div class="page-header">
        <h1>Satus</h1>
    </div>
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1">
        <ItemTemplate>
            <div class="floatingBox">
                <center><h4><%# Eval("IpAddress") %></h4></center>
                <div class="gBox" id='<%# "mem" + Eval("IpAddress") %>'> </div>
                <div class="gBox" id='<%# "emu" + Eval("IpAddress") %>'> </div>
                <script type="text/javascript">
                    new JustGage({
                        id: '<%# "mem" + Eval("IpAddress") %>',
                        min: 0,
                        max: <%#   ((int)Eval("totalMemory") / 1024) %>,
                        value: <%# ((int)Eval("freeMemory") / 1024) %>,
                        title: "Memory Usage",
                        label: "",
                        levelColorsGradient: false
                    });
                    new JustGage({
                         id: '<%# "emu" + Eval("IpAddress") %>',
                         min: 0,
                         max: <%# Eval("maxSupported") %>,
                        value: <%# Eval("emulatorCount") %>,
                        title: "Emulators",
                        label: "",
                        levelColorsGradient: false
                     });
                </script>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetServerInformation" TypeName="com.sjsu.mobiletaas.resourcemanager.ResourceManager" OnObjectCreating="ObjectDataSource1_ObjectCreating"></asp:ObjectDataSource>
</asp:Content>
