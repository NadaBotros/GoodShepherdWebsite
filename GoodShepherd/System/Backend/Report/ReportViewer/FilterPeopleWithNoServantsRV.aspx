<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/System/Backend/Report/ReportViewer/Report.Master" CodeBehind="FilterPeopleWithNoServantsRV.aspx.cs" Inherits="System.Backend.FilterPeopleWithNoServantsRV" %>


<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPHTitle" runat="server">
        كشف بيانات المخدومين الغير مربوطين بخدام

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="CPHContent" runat="server">
    <asp:Label runat="server" Text="" ID="Label1" Style="float:right;padding-right:10px"></asp:Label>


    <asp:GridView runat="server" SkinID="grdnotPag" ID="GridView1" AllowPaging="false" >
        <Columns>
           
        </Columns>
    </asp:GridView>
</asp:Content>

