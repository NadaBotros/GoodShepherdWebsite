<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="ChangeInfo.aspx.cs" Inherits="System.Backend.ChangeInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="البيانات الشخصية" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/img/key.png" />
                <asp:Label runat="server" Text="تعديل بياناتي" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="msg" runat="server">
                <asp:Label ID="lblMessge" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" class="tblMain" cellspacing="2">
                    <tr>
                        <td>
                            كلمة السر القديمة <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtoldPassword" runat="server" TextMode="Password"></asp:TextBox>
                               </td>
                    </tr>
                    <tr>
                        <td>
                            كلمة السر الجديدة 
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            تاكيد كلمة السر <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtconfirm" runat="server" TextMode="Password"></asp:TextBox>
                                  <asp:CompareValidator ID="Compassword" runat="server" ControlToCompare="txtNewPassword"
                                ValidationGroup="1" ControlToValidate="txtconfirm"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                           اسم المستخدم <span class="reqstar">*</span>
                        </td>
                        <td width="85%">
                            <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox>
                                    </td>
                    </tr>
                    <tr>
                        <td>
                            اسم الدخول <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtLogInName"></asp:TextBox>
                                   </td>
                    </tr>
                    <tr>
                        <td>
                            البريد الالكتروني
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="reqLoginEmail" ValidationGroup="1" ControlToValidate="txtEmail"
                                runat="server" Text="Error" ErrorMessage="بريد الالكتروني خطا" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            رقم الموبايل
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="1"
                                ControlToValidate="txtMobile" runat="server" Text="Error" ErrorMessage="خطا فى رقم الموبايل"
                                ValidationExpression="01[0-2]{1}[0-9]{8}"></asp:RegularExpressionValidator>
                            <asp:FilteredTextBoxExtender ID="txtCost_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtMobile">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            الوظيفة
                        </td>
                        <td>
                            <asp:TextBox ID="txtJob" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           رفع الصورة الشخصية
                        </td>
                        <td>
                            <asp:FileUpload ID="fupldImage" runat="server" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Image runat="server" CssClass="imgborder" ImageUrl="~/System/Backend/lib/img/avatar150.png"
                                ID="imgUser" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdbtns">
                            <asp:Button ID="btnSave" Text="حفظ البيانات" runat="server" ValidationGroup="1"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

