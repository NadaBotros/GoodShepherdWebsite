<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/System/Backend/Report/ReportViewer/Report.Master"  CodeBehind="AllPeopleRV.aspx.cs" Inherits="System.Backend.AllPeopleRV" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitle" runat="server">
    كشف بيانات كل الناس
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <asp:Label runat="server" Text="" ID="lblAttendAllNo" Style="float:right;padding-right:10px"></asp:Label>
    <asp:Label runat="server" Text="" ID="Label1" Style="float:right;padding-right:10px"></asp:Label>


    <asp:GridView runat="server" SkinID="grdnotPag" ID="grdData" AllowPaging="false" >
        <Columns>
           
        </Columns>
    </asp:GridView>
</asp:Content>

