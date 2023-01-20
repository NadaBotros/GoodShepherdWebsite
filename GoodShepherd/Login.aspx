<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Login" %>

<asp:Content runat="server" ContentPlaceHolderID="CPHHead" ID="contHead">
    <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        <asp:Label runat="server" ID="lblTitle">تسجيل الدخول</asp:Label>
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td class="SiteText">التسجيل خاص بخدام وافراد اجتماع الراعي الصالح فقط
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" DefaultButton="btnLogin" ID="pnlLogin">
                    <table class="SiteText" cellpadding="3" style="padding-top: 15px">
                        <tr>
                            <td>الكود</td>
                            <td>
                                <asp:TextBox SkinID="sd" CssClass="txt22" runat="server" ID="txtCode"></asp:TextBox></td>
                            <td>
                                <asp:RequiredFieldValidator ID="req1" ValidationGroup="1" ControlToValidate="txtCode" runat="server" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                            <td>الكود الموجود بكارت المتابعه</td>
                        </tr>
                        <tr>
                            <td>كلمة المرور</td>
                            <td>
                                <asp:TextBox SkinID="sd" CssClass="txt22" runat="server" ID="txtpassword" TextMode="Password"></asp:TextBox></td>
                            <td>
                                <asp:RequiredFieldValidator ID="req2" ValidationGroup="1" ControlToValidate="txtpassword" runat="server" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Label runat="server" ID="lblMSG" Style="text-align: center" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="3">
                                <asp:LinkButton ValidationGroup="1" CssClass="btnLogin" runat="server" ID="btnLogin" Text="دخول" OnClick="btnLogin_Click"></asp:LinkButton>
                                &nbsp;
                            <asp:LinkButton CssClass="btnLogin" runat="server" Width="150px" ID="btnForgetPassword" Text="نسيت كلمة المرور" OnClick="btnForgetPassword_OnClick" ></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
