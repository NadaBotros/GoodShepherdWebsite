<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/Report/ReportViewer/Report.Master" AutoEventWireup="true" CodeBehind="AttendBetweenDatesRV.aspx.cs" Inherits="System.Backend.AttendBetweenDatesRV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitle" runat="server">
    <asp:Label runat="server" ID="lblPageTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <asp:Label runat="server" Text="" ID="lblAttendAllNo" Style="float:right;padding-right:10px"></asp:Label>

    <asp:GridView ID="grdData" AllowPaging="false" SkinID="grdnotPag" Width="100%" AllowSorting="false" runat="server" CssClass="grd"
        AutoGenerateColumns="False">
        <Columns>           
            <asp:BoundField DataField="PersonCode" HeaderText="الكود" SortExpression="PersonCode"></asp:BoundField>
            <asp:BoundField DataField="PersonName" HeaderText="الاسم" SortExpression="PersonName"></asp:BoundField>
            <asp:BoundField DataField="MobileNo1" HeaderText=" رقم التليفون 1" SortExpression="MobileNo1"></asp:BoundField>
            <asp:BoundField DataField="MobileNo2" HeaderText=" رقم التليفون 2" SortExpression="MobileNo2"></asp:BoundField>
            <asp:BoundField DataField="HomePhone" HeaderText="تليفون البيت" SortExpression="HomePhone"></asp:BoundField>


        </Columns>
        
    </asp:GridView>
</asp:Content>
