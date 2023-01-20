<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="MeetingArchiveManagement.aspx.cs" Inherits="System.Backend.manage.MeetingArchiveManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="ارشيف الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="ادارة ارشيف الاجتماع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="msg" runat="server">
                <asp:Label ID="lblMessge" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" cellspacing="2">
                    <tr>
                        <td>النوع <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:DropDownList style="float:right" runat="server" ID="drpType">
                                <asp:ListItem>مؤتمرات</asp:ListItem>
                                <asp:ListItem>رحلات</asp:ListItem>
                                <asp:ListItem>ندوات</asp:ListItem>
                                <asp:ListItem>الاجتماعات</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>العنوان <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                ValidationGroup="1" runat="server" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>التاريخ <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDate"></asp:TextBox>
                            <asp:CalendarExtender Format="d/M/yyyy" ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                ValidationGroup="1" runat="server" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>ايجابيات
                        </td>
                        <td>
                            <asp:TextBox Width="600px" Height="180px" TextMode="MultiLine" SkinID="txtmult" runat="server" ID="txtPositives"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>مقترحات
                        </td>
                        <td>
                            <asp:TextBox Width="600px" Height="180px" TextMode="MultiLine" SkinID="txtmult" runat="server" ID="txtSuggestion"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>سلبيات
                        </td>
                        <td>
                            <asp:TextBox Width="600px" Height="180px" TextMode="MultiLine" SkinID="txtmult" runat="server" ID="txtNevative"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="tdbtns">
                <asp:Button ID="btnBack" runat="server" Text="رجوع للصفحة السابقة" OnClick="btnBack_Click" />
                <asp:Button ID="btnClear" runat="server" Text="تفريغ الخانات" OnClick="btnClear_Click" />
                <asp:Button ID="btnSaveAndNew" ValidationGroup="1" runat="server" Text="حفظ البيانات واضافة جديد"
                    OnClick="btnSaveAndNew_Click" />
                <asp:Button ID="btnSave" ValidationGroup="1" runat="server" Text="حفظ البيانات" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
