<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" 
    CodeBehind="PersonsCards.aspx.cs" Inherits="System.Backend.PersonsCards" %>

<%@ Register Src="~/System/Backend/UserControls/UcAdvancedSearch.ascx" 
    TagPrefix="uc1" TagName="UcAdvancedSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="التقارير" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="طباعة كروت الاجتماع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UcAdvancedSearch runat="server" id="UcAdvancedSearch" />
            </td>
        </tr>
        <tr>
            <td align="center" >
                <asp:Button runat="server" style="margin: auto" Text="طباعة الكروت" ID="btnPrint" OnClick="btnPrint_OnClick" />
            </td>
        </tr>
    </table>
</asp:Content>
