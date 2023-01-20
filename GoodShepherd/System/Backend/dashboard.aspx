<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="dashboard.aspx.cs" Inherits="System.Backend.dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="ادارة الموقع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="لوحة تحكم الموقع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>

            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="5" width="500px" cellspacing="3">
                    <tr>
                        <td>عدد الزار المتواجدين الان : -
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCurrent"></asp:Label>
                        </td>
                        <td>عدد زوار اخر 30 يوم :-
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lbllast30days"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>اجمالي عدد زيارات صفحات الموقع :-
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblPages"></asp:Label>
                        </td>
                        <td>اجمالي عدد زوار الموقع :-
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTotal"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="4">
                            <table cellpadding="5">
                                <tr>
                                    <td>حالة البث المباشر للصوت</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpSound">
                                            <asp:ListItem Value="1">لا يعمل</asp:ListItem>
                                            <asp:ListItem Value="2">مسجل</asp:ListItem>
                                            <asp:ListItem Value="3">بث مباشر</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>حالة البث المباشر للفيديو</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpVideo">
                                            <asp:ListItem Value="1">لا يعمل</asp:ListItem>
                                            <asp:ListItem Value="2">مسجل</asp:ListItem>
                                            <asp:ListItem Value="3">بث مباشر</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>الثيم الحالي</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpTheme">
                                            <asp:ListItem Value="Baskha">اسبوع الالام</asp:ListItem>
                                            <asp:ListItem Value="Birthday">عيد ميلاد الاجتماع</asp:ListItem>
                                            <asp:ListItem Value="christmas">الكريسماس</asp:ListItem>
                                            <asp:ListItem Value="Default">الاساسي</asp:ListItem>
                                            <asp:ListItem Value="Easter">القيامه</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button OnClick="btnSave_Click" runat="server" ID="btnSave" Text="حفظ البيانات" /></td>
                                </tr>
                            </table>
                        </td>

                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>
