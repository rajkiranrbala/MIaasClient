<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Launch.aspx.cs" Inherits="MobileTaasClient.Launch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Launch</h1>
    </div>
    <p class="lead">
        Android Version
        <asp:DropDownList ID="cmbAndroidVersion" runat="server">
            <asp:ListItem Selected="True" Value="10">Android 2.3.3</asp:ListItem>
            <asp:ListItem Value="16">Android 4.1.2</asp:ListItem>
            <asp:ListItem Value="17">Android 4.2.2</asp:ListItem>
            <asp:ListItem Value="18">Android 4.3</asp:ListItem>
            <asp:ListItem Value="19">Android 4.4</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p class="lead">
        Memory Size
        <asp:DropDownList ID="cmbMemory" runat="server">
            <asp:ListItem Value="512">512MB</asp:ListItem>
            <asp:ListItem Value="1024">1024MB</asp:ListItem>
            <asp:ListItem Value="2048">2048MB</asp:ListItem>
        </asp:DropDownList>

    </p>
    &nbsp;<p class="lead">
        <asp:Button ID="btnLaunch" runat="server" Text="Launch" OnClick="btnLaunch_Click" />
    </p>

    <div id="pricing">
        <table>
            <thead>
                <tr>
                    <th style="width: 150px">Version</th>
                    <th style="width: 100px">Memory</th>
                    <th style="width: 200px">Price / Second</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Android 2.3.3</td>
                    <td>512M</td>
                    <td>$0.010</td>
                </tr>
                <tr>
                    <td>Android 2.3.3</td>
                    <td>1024M</td>
                    <td>$0.030</td>
                </tr>
                <tr>
                    <td>Android 2.3.3</td>
                    <td>2048M</td>
                    <td>$0.060</td>
                </tr>
                <tr>
                    <td>Android 4.1.2</td>
                    <td>512M</td>
                    <td>$0.010</td>
                </tr>
                <tr>
                    <td>Android 4.1.2</td>
                    <td>1024M</td>
                    <td>$0.030</td>
                </tr>
                <tr>
                    <td>Android 4.1.2</td>
                    <td>2048M</td>
                    <td>$0.060</td>
                </tr>
                <tr>
                    <td>Android 4.2.2</td>
                    <td>512M</td>
                    <td>$0.010</td>
                </tr>
                <tr>
                    <td>Android 4.2.2</td>
                    <td>1024M</td>
                    <td>$0.030</td>
                </tr>
                <tr>
                    <td>Android 4.2.2</td>
                    <td>2048M</td>
                    <td>$0.060</td>
                </tr>
                <tr>
                    <td>Android 4.3</td>
                    <td>512M</td>
                    <td>$0.010</td>
                </tr>
                <tr>
                    <td>Android 4.3</td>
                    <td>1024M</td>
                    <td>$0.030</td>
                </tr>
                <tr>
                    <td>Android 4.3</td>
                    <td>2048M</td>
                    <td>$0.060</td>
                </tr>
                <tr>
                    <td>Android 4.4</td>
                    <td>512M</td>
                    <td>$0.010</td>
                </tr>
                <tr>
                    <td>Android 4.4</td>
                    <td>1024M</td>
                    <td>$0.030</td>
                </tr>
                <tr>
                    <td>Android 4.4</td>
                    <td>2048M</td>
                    <td>$0.060</td>
                </tr>

            </tbody>
        </table>

    </div>
</asp:Content>
