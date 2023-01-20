<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeBehind="ChangeMyPicture.aspx.cs" Inherits="ChangeMyPicture" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHHead" ID="contHead">
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        تغير صورتك الشخصية
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td valign="top">
                            <table>
                                <tr>
                                    <td colspan="2" class="SiteText">ملاحظة :- هذة الصورة هي التي نستخدمها فى طباعة كارت المتابعة الخاص بك
                                    </td>
                                </tr>
                                <tr>
                                    <td>ارفع الصورة الشخصية</td>
                                    <td>
                                        <asp:FileUpload runat="server" ID="fpldImage" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label runat="server" ID="lblMSG" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:LinkButton runat="server" Text="رفع الصورة" CssClass="btnLogin" ID="btnSave" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <asp:Image style="padding-left:10px;float: left" runat="server" ID="imgPerson" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>
</asp:Content>
