<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ServantPersons.aspx.cs" EnableEventValidation="false" Inherits="System.Backend.ServantPersons" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="التقارير" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="../../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="تقرير مجموعة افتقاد الخدام" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3">
                    <tr>
                        <td>اختر خادم الافتقاد</td>
                        <td>
                            <asp:DropDownList runat="server" Width="250px" DataTextField="PersonName" DataValueField="ServantId" ID="drpServant" DataSourceID="odsServants"></asp:DropDownList>
                            <asp:ObjectDataSource ID="odsServants" runat="server" SelectMethod="LoadAllList" TypeName="DAL.ServantAftkadManagement"></asp:ObjectDataSource>
                        </td>
                        <td>
                            <asp:Button ID="btnReport" Text="عرض التقرير" runat="server" OnClick="btnReport_Click" />
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>
