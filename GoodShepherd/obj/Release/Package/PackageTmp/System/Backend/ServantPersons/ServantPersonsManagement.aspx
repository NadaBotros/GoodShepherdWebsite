<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="ServantPersonsManagement.aspx.cs" Inherits="System.Backend.ServantPersonsManagement" %>

<%@ Register Src="~/System/Backend/UserControls/UcSearch.ascx" TagPrefix="uc1" TagName="UcSearch" %>
<%@ Register Src="~/System/Backend/UserControls/UcPersonGrid.ascx" TagPrefix="uc1" TagName="UcPersonGrid" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="مجموعة افتقاد الخادم" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="اضافة مجموعة افتقاد الخادم" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="msg" runat="server">
                <asp:Label ID="lblMessge" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UcSearch runat="server" ID="UcSearch" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <table cellpadding="4" style="float: right; padding-top: 15px">
                                <tr>
                                    <td>اختر الخادم المراد ربط المخدومين المختاره به</td>
                                   
                                    <td>
                                        <asp:DropDownList runat="server" Width="250px" DataTextField="PersonName" DataValueField="ServantId" ID="drpServant" DataSourceID="odsServants"></asp:DropDownList>
                                        <asp:ObjectDataSource ID="odsServants" runat="server" SelectMethod="LoadAllList" TypeName="DAL.ServantAftkadManagement">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="True" Name="IsServantAftkad" Type="Boolean" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                    <td>
                                        <asp:Button Text="ربط المخدومين بالخادم" OnClick="btnSave_Click" runat="server" ID="btnSave" /></td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdbtns">
                            <asp:Button ID="btnSearch" runat="server" Text="عرض نتائج البحث" OnClick="btnSearch_Click" /></td>
                        
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UcPersonGrid runat="server" ID="UcPersonGrid" />
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
</asp:Content>
