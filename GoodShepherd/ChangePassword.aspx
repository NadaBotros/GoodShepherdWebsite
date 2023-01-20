<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ChangePassword" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHHead" ID="contHead">
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        تعديل كلمة المرور
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 15px">
                <asp:Panel runat="server" DefaultButton="btnSave"  ID="pnlChangePassword">
                    <table cellpadding="2" class="SiteText" style="direction: ltr; text-align: right" cellspacing="2">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtoldPassword" CssClass="txt22" SkinID="120" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
                                    ControlToValidate="txtoldPassword"></asp:RequiredFieldValidator>
                            </td>
                            <td>كلمة المرور القديمة 
                            </td>
                        </tr>
                        <tr>

                            <td>
                                <asp:TextBox ID="txtNewPassword" CssClass="txt22" SkinID="120" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxt" runat="server" ValidationGroup="1" ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
                            </td>
                            <td>كلمة المرور الجديدة 
                            </td>
                        </tr>
                        <tr>

                            <td>
                                <asp:TextBox ID="txtconfirm" CssClass="txt22" SkinID="120" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqConfirm" runat="server" ControlToValidate="txtconfirm"
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="Compassword" runat="server" ControlToCompare="txtNewPassword"
                                    ValidationGroup="1" ControlToValidate="txtconfirm"></asp:CompareValidator>
                            </td>
                            <td>تاكيد كلمة المرور الجديدة 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label runat="server" ForeColor="Red" ID="lblMSG"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:LinkButton CssClass="btnLogin" ID="btnSave" Text="تغير كلمة المرور" runat="server" ValidationGroup="1"
                                    OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
