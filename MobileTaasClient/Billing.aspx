<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="MobileTaasClient.Billing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1>Billing</h1>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:BoundField DataField="TransactionID" HeaderText="TransactionID" SortExpression="TransactionID"></asp:BoundField>
            <asp:BoundField DataField="EmulatorId" HeaderText="EmulatorId" SortExpression="EmulatorId">
                <ItemStyle Width="300px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Version" HeaderText="Version" SortExpression="Version"></asp:BoundField>
            <asp:BoundField DataField="Memory" HeaderText="Memory" SortExpression="Memory"></asp:BoundField>
            <asp:BoundField DataField="StartTime" HeaderText="StartTime" SortExpression="StartTime"></asp:BoundField>
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate"></asp:BoundField>
            <asp:TemplateField HeaderText="Duration">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# ((DateTime)Eval("EndTime") - (DateTime)Eval("StartTime")).TotalSeconds.ToString() + "s" %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# "$" + (((DateTime)Eval("EndTime") - (DateTime)Eval("StartTime")).TotalSeconds * ((double)(decimal)Eval("Rate"))).ToString()%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="GetPendingPayments" TypeName="com.sjsu.mobiletaas.resourcemanager.ResourceManager" OnObjectCreating="ObjectDataSource1_ObjectCreating">
        <SelectParameters>
            <asp:SessionParameter SessionField="UserId" DefaultValue="-1" Name="userid" Type="Int32"></asp:SessionParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:Button ID="btnPay" runat="server" Text="Pay" OnClick="btnPay_Click" />
</asp:Content>
