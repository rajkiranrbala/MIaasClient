<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Devices.aspx.cs" Inherits="MobileTaasClient.Devices" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Devices</h1>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="DeviceSources" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" DataKeyName="EmulatorId">
        <Columns>            
            <asp:BoundField DataField="name" ItemStyle-Width="100px" HeaderText="Name" SortExpression="name"></asp:BoundField>
            <asp:BoundField DataField="memorySize" ItemStyle-Width="400px" HeaderText="Memory" SortExpression="memorySize"></asp:BoundField>
            <asp:BoundField DataField="version" ItemStyle-Width="100px" HeaderText="Version" SortExpression="version"></asp:BoundField>
            <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>

                    <asp:LinkButton Width="100px" ID="btnCopy" Text="Control" runat="server" CausesValidation="False" CommandName="Control" CommandArgument='<%# Eval("name") %>'>
                    </asp:LinkButton>

                </ItemTemplate>

                <ItemStyle HorizontalAlign="Right" Width="150px"></ItemStyle>

            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="DeviceSources" runat="server" OnObjectCreating="DeviceSources_ObjectCreating" SelectMethod="GetActiveEmulators" TypeName="com.sjsu.mobiletaas.resourcemanager.ResourceManager">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="userid" SessionField="UserId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
