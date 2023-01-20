<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="ActivityManagement.aspx.cs" Inherits="System.Backend.ActivityManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/ucSmallSearch.ascx" TagName="ucSmallSearch" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="رحلات و مؤتمرات" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">

    <table width="99%">
        <tr>
            <td id="msg" runat="server">
                <asp:Label ID="lblMessge" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table cellpadding="5">
                    <tr>
                        <td>اسم النشاط
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtTitle" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>تفاصيل النشاط
                        </td>
                        <td>
                            <asp:TextBox runat="server" SkinID="txtmult" Style="height: 200px" TextMode="MultiLine" ID="txtDesc"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>تكلفة النشاط
                        </td>
                        <td>
                            <asp:TextBox runat="server" SkinID="txtmult" ID="txtPrice" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPrice" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>اسباب الاعتذار
                        </td>
                        <td>
                            <asp:TextBox runat="server" SkinID="txtmult" TextMode="MultiLine" ID="txtRefuse"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>تاريخ النشاط</td>
                        <td>
                            <asp:CalendarExtender ID="CalendarExtender2" Format="d/M/yyyy" TargetControlID="txtDate" runat="server"></asp:CalendarExtender>
                            <asp:TextBox runat="server" ID="txtDate"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtDate" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>عدد ايام النشاط
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDaysNo"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtDaysNo" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>مكان النشاط
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPlace"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtPlace" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>الخادم المسؤل
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtServantName"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtServantName" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>موبايل الخادم المسؤل
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMobile"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>اخر ميعاد للتسجيل</td>
                        <td>
                            <asp:CalendarExtender ID="CalendarExtender1" Format="d/M/yyyy" TargetControlID="txtLastDate" runat="server"></asp:CalendarExtender>
                            <asp:TextBox runat="server" ID="txtLastDate"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td>فيديو يوتيوب لينك</td>
                        <td>

                            <asp:TextBox runat="server" ID="txtVideoPath"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Image Style="float: right" runat="server" ID="imgActivity" />
                        </td>
                    </tr>
                    <tr>
                        <td>ارفع صورة لمكان النشاط <span class="reqstar">*</span></td>
                        <td>
                            <asp:FileUpload runat="server" ID="fupldActivity" />
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
