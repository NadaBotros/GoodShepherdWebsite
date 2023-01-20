<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="AttendBetweenDates.aspx.cs" Inherits="System.Backend.AttendBetweenDates" %>

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
                <asp:Label runat="server" Text="تقرير كشف حضور فتره" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
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
                        <td>بدايه فتره الحضور</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFrom"></asp:TextBox>
                            <asp:CalendarExtender Format="d/M/yyyy" TargetControlID="txtFrom" ID="extDateFrom" runat="server"></asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ControlToValidate="txtFrom" ID="rveFrom" ErrorMessage="*" runat="server" ValidationGroup="1"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>نهايه فتره الحضور</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTo"></asp:TextBox>
                            <asp:CalendarExtender Format="d/M/yyyy" TargetControlID="txtTo" ID="CalendarExtender1" runat="server"></asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ControlToValidate="txtTo" ID="RequiredFieldValidator1" ErrorMessage="*" runat="server" ValidationGroup="1"></asp:RequiredFieldValidator></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdbtns">
                <asp:Button ID="btnReport" Text="عرض التقرير" runat="server" OnClick="btnReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
