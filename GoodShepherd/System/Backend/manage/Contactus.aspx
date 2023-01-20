<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="Contactus.aspx.cs" Inherits="Contactus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="بيانات مبرمج الموقع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/User.png" />
                <asp:Label runat="server" Text="بيانات مبرمج الموقع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td runat="server" style="padding-top: 80px;padding-bottom:80px; text-align: center;font-size:20px">م / امجد جمال خلف
        <br /> <br />
                01229464974
        <br /> <br />
                amgad_gam@yhaoo.com
            </td>
        </tr>
    </table>
</asp:Content>
