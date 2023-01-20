<%@ Page Title="نسيت كلمة المرور" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="ForgetPassword" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHHead" ID="contHead">
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        <asp:Label runat="server" Text="نسيت كلمة المرور" ID="lblTitle"></asp:Label>
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" DefaultButton="btnSend" ID="pnlLogin">
                    <table class="SiteText" cellpadding="3" style="padding-top: 15px">
                        <tr>
                            <td>الكود</td>
                            <td>
                                <asp:TextBox SkinID="sd" CssClass="txt22" runat="server" ID="txtCode"></asp:TextBox></td>
                            <td>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtCode" FilterType="Numbers" runat="server"></asp:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="req1" ValidationGroup="1" ControlToValidate="txtCode" runat="server" ForeColor="Red"
                                    SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                            <td>الكود الموجود بكارت المتابعه</td>
                        </tr>
                        <tr>
                            <td>الرقم القومي</td>
                            <td>
                                <asp:TextBox SkinID="sd" CssClass="txt22" runat="server" ID="txtNationalId"></asp:TextBox></td>
                            <td>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtNationalId" FilterType="Numbers" runat="server"></asp:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" ForeColor="Red" SetFocusOnError="true"
                                    ValidationGroup="1" runat="server" ControlToValidate="txtNationalId"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="reqNational" SetFocusOnError="true" ForeColor="Red" ControlToValidate="txtNationalId" ValidationGroup="1" 
                                    runat="server" ErrorMessage="لابد من ادخال 14 رقم" ValidationExpression="\d{14}"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Label runat="server" ID="lblMSG" Style="text-align: center" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="3">
                                <asp:LinkButton ValidationGroup="1" CssClass="btnLogin" runat="server" ID="btnSend" Text="عرض كلمة المرور" OnClick="btnSend_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
