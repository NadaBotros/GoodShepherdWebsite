<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="SpeakersManagement.aspx.cs" Inherits="System.Backend.SpeakersManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/System/Backend/Family/UcPerson.ascx" TagPrefix="uc1" TagName="UcPerson" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="واعظين الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/User.png" />
                <asp:Label runat="server" Text="ادارة واعظين الاجتماع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
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
                        <td width="15%">اسم الواعظ <span class="reqstar">*</span>
                        </td>
                        <td width="85%">
                            <asp:TextBox runat="server" ID="txtSpeakerTitle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                ValidationGroup="1" runat="server" ControlToValidate="txtSpeakerTitle"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">كنيسة الواعظ <span class="reqstar">*</span>
                        </td>
                        <td width="85%">
                            <asp:TextBox runat="server" ID="txtChurch"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td>رفع صورة الواعظ <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:FileUpload ID="fupldImage" runat="server" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Image runat="server" CssClass="imgborder"
                                ID="imgSpeaker" />
                        </td>
                    </tr>
                    <tr>
                        <td>ملاحظات
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNotes" Height="70px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td class="tdbtns">
                <asp:Button ID="btnBack" runat="server" Text="رجوع للصفحة السابقة" OnClick="btnBack_Click" />
                <asp:Button ID="btnClear" runat="server" Text="تفريغ الخانات" OnClick="btnClear_Click" />
                <asp:Button ID="btnSaveAndNew" ValidationGroup="1" runat="server" Text="حفظ البيانات واضافة جديد"
                    OnClick="btnSaveAndNew_Click" />


                <asp:Button ID="btnSave" ValidationGroup="1" runat="server" Text="حفظ البيانات" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
