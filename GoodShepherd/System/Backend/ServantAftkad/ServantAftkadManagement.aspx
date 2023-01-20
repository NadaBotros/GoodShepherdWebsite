<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ServantAftkadManagement.aspx.cs" Inherits="System.Backend.manage.ServantAftkadManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="خدام الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="اضافة خادم اجتماع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
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
                        <td>كلمة البحث <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSearch"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                ValidationGroup="21" runat="server" Display="Dynamic" ControlToValidate="txtSearch"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnSearch" ValidationGroup="21" OnClick="btnSearch_Click" Text="بحث" />
                        </td>
                    </tr>
                    <tr>
                        <td>اختر اسم الخادم <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:ListBox runat="server" ID="lstServants" Width="312px" Height="100px"></asp:ListBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                ValidationGroup="1" runat="server" Display="Dynamic" ControlToValidate="lstServants"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>خدمات الخادم <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" TextMode="MultiLine" SkinID="txtmult" ID="txtServices"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                ValidationGroup="1" runat="server" Display="Dynamic" ControlToValidate="txtServices"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>هل الخادم يفتقد
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="radServantType" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True">نعم</asp:ListItem>
                                <asp:ListItem Value="False" Selected="True">لا</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="tdbtns">
                <asp:Button ID="btnBack" runat="server" Text="رجوع للصفحة السابقة" OnClick="btnBack_Click" />
                <asp:Button ID="btnSave" ValidationGroup="1" runat="server" Text="حفظ البيانات"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnSaveAndNew" ValidationGroup="1" runat="server" Text="حفظ البيانات واضافة جديد"
                    OnClick="btnSaveAndNew_Click" />

            </td>
        </tr>
    </table>
</asp:Content>
