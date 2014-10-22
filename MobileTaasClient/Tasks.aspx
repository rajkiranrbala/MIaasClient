<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="MobileTaasClient.Tasks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
    <h1>Tasks</h1>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="TasksSource">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="RequestDate" HeaderText="Date" ReadOnly="True" SortExpression="RequestDate" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="TasksSource" runat="server" OnObjectCreating="TasksSource_ObjectCreating" SelectMethod="GetTasks" TypeName="com.sjsu.mobiletaas.resourcemanager.ResourceManager">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="userid" SessionField="UserId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
