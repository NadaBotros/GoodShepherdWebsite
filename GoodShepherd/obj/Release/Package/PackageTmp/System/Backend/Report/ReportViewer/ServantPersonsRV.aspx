<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/Report/ReportViewer/Report.Master" AutoEventWireup="true" CodeBehind="ServantPersonsRV.aspx.cs" Inherits="System.Backend.ServantPersonsRV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitle" runat="server">
    <asp:Label runat="server" ID="lblTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <asp:GridView ID="grdData" Width="100%" AllowSorting="true" SkinID="grdnotPag" runat="server" CssClass="grd"
        AutoGenerateColumns="False" DataSourceID="odsServantPersons">
        <Columns>
            <asp:BoundField DataField="PersonCode" HeaderText="الكود" SortExpression="PersonCode"></asp:BoundField>
            <asp:BoundField DataField="PersonName" HeaderText="الاسم" SortExpression="PersonName"></asp:BoundField>
            <asp:BoundField DataField="HomePhone" HeaderText="التليفون" SortExpression="HomePhone"></asp:BoundField>
            <asp:BoundField DataField="Mobile" HeaderText="رقم الموبايل" SortExpression="Mobile"></asp:BoundField>
            <asp:BoundField DataField="BirthDate" HeaderText="تاريخ الميلاد" SortExpression="BirthDate"></asp:BoundField>
            <asp:BoundField DataField="MarriageDate" HeaderText="تاريخ الزواج" SortExpression="MarriageDate"></asp:BoundField>
            <asp:BoundField DataField="Address" HeaderText="العنوان" SortExpression="Address"></asp:BoundField>
            <asp:BoundField DataField="Studious" HeaderText="حضورالاجتماع" SortExpression="Studious"></asp:BoundField>

        </Columns>
        <EmptyDataTemplate>
            <center>لا يوجد اسماء بالقائمة</center>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsServantPersons" runat="server" SelectMethod="ServantPersonsReport" TypeName="DAL.ServantPersonsManagement">
        <SelectParameters>
            <asp:QueryStringParameter Name="ServantId" QueryStringField="id" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
