<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="PageSectionManage.aspx.cs" Inherits="System.Backend.Pages.PageSectionManage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="صفحات الموقع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="ادارة صفحات الموقع" CssClass="tdPageTitle"
                    ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="msg" runat="server">
                <asp:Label ID="lblMessge" runat="server"></asp:Label>
            </td>
        </tr>
        <tr><td>
            الصفحة على الموقع تتكون من مجموعة من الفقرات يمكن لكل فقره ان تحتوي على عنوان او نص او صوره او فيديو يوتيوب<br />
            اذا كان في الفقره نص وصورة تعرض الصورة بحجم متوسط بجوار النص<br />
            اما اذا كانت الصورة بدون نص تعرض الصورة بحجم كبير فى منتصف الفقرة
            </td></tr>
        <tr>
            <td>
                <table cellpadding="2" class="tblMain" cellspacing="2">
                    <tr>
                        <td width="15%">
                            اسم الصفحة
                        </td>
                        <td width="85%">
                            <asp:DropDownList ID="drpPageName" style="float:right" runat="server" DataSourceID="odsPages" DataTextField="PageName"
                                DataValueField="PageId">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="odsPages" runat="server" SelectMethod="LoadPages" TypeName="DAL.PageSectionManagement">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            عنوان الفقرة 
                        </td>
                        <td width="85%">
                            <asp:TextBox runat="server" ID="txtPageTitle"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            لينك فيديو يوتيوب
                        </td>
                        <td width="85%">
                            <asp:TextBox runat="server" ID="txtYoutube"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            نص الفقرة <span class="reqstar">*</span>
                        </td>
                        <td width="85%">
                            <cc1:Editor ID="edContent" Height="500px" Width="700px" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Image runat="server" ID="imgSection" />
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            صورة الفقرة
                        </td>
                        <td width="85%">
                            <asp:FileUpload ID="fpld" runat="server" />
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
