<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="DayAttend.aspx.cs" Inherits="System.Backend.DayAttend" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="التقارير" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="../../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="كشف حضور يوم" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3">
                    <tr>
                        <td>اختر خادم الافتقاد</td>
                        <td>
                            <asp:DropDownList runat="server" SkinID="drpwList" Width="250px" DataTextField="PersonName" OnDataBound="drpServant_DataBound" DataValueField="ServantId" ID="drpServant" DataSourceID="odsServants"></asp:DropDownList>
                            <asp:ObjectDataSource ID="odsServants" runat="server" SelectMethod="LoadAllList" TypeName="DAL.ServantAftkadManagement"></asp:ObjectDataSource>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>تاريخ الاجتماع</td>
                        <td>
                            <asp:DropDownList runat="server" Width="250px" SkinID="drpwList" DataTextField="AttendDate" 
                                DataValueField="AttendDate" ID="drpDates" DataSourceID="odsDates"></asp:DropDownList>
                            <asp:ObjectDataSource ID="odsDates" runat="server" SelectMethod="AttendDates" TypeName="DAL.PersonAttendManagement"></asp:ObjectDataSource>
                        </td>
                        <td>
                          </td>
                    </tr>
                    <tr>
                        <td>نوع الكشف</td>
                        <td>
                            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="radAttendType">
                                <asp:ListItem Value="True" Selected="True">كشف الحضور</asp:ListItem>
                                <asp:ListItem Value="False">كشف الغياب</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdbtns">
                <asp:Button ID="btnReport" Text="عرض التقرير" runat="server" OnClick="btnReport_Click" />&nbsp;
                <asp:Button ID="btnGmailExport" Text="Export to Gmail Contacts" runat="server" OnClick="btnGmailExport_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
