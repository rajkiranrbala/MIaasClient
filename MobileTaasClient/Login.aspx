<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MobileTaasClient.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="page-header">
      <h1>Login</h1>
    </div>
    <p class="lead">
    Username    <asp:TextBox ID="txtUserName" runat="server" Width="128px"></asp:TextBox> 
    </p>
    <p class="lead">
    Password
    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
        </p>
    <p class="lead">
    
    <asp:Label ID="txtError" ForeColor="Red" runat="server"></asp:Label>
        </p>
    <p class="lead">
        <asp:Button ID="btnSignIn" runat="server" Text="SignIn" OnClick="btnSignIn_Click" />
    </p>
</asp:Content>
