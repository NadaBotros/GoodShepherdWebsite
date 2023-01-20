<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="MyInfo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <style type="text/css">
        select, input[type="radio"] {
            direction: rtl;
        }
    </style>
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        <asp:Label runat="server" Text="تعديل بياناتي" ID="lblTitle"></asp:Label>
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="direction: rtl">
                <table class="SiteText" cellpadding="3" style="padding-top: 15px; float: right; direction: ltr; text-align: right">
                    <tr>

                        <td>
                            <asp:DropDownList SkinID="1" Width="310px" runat="server" ID="drpMaritalStatus">
                                <asp:ListItem>اعزب</asp:ListItem>
                                <asp:ListItem>متزوج</asp:ListItem>
                                <asp:ListItem>ارمل</asp:ListItem>
                                <asp:ListItem>مطلق</asp:ListItem>
                                <asp:ListItem>متوفي</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td><span class="reqstar">*</span> الحالة الاجتماعية  
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox SkinID="txtsite" runat="server" ID="txtFatherName"></asp:TextBox>
                        </td>
                        <td>اب الاعتراف
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox SkinID="txtsite" runat="server" ID="txtJob"></asp:TextBox>
                        </td>
                        <td>الوظيفة</td>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox SkinID="txtsite" runat="server" ID="txtJobPlace"></asp:TextBox>
                        </td>
                        <td>جهة العمل
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox SkinID="txtsite" MaxLength="11" runat="server" ID="txtMobile"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator22"
                                ValidationGroup="10" runat="server" ControlToValidate="txtMobile"></asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtMobile" FilterType="Numbers" runat="server"></asp:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" ControlToValidate="txtMobile" ValidationGroup="10" runat="server" ErrorMessage="رقم الموبايل غير صحيح" ValidationExpression="01[0-2]{1}[0-9]{8}"></asp:RegularExpressionValidator>
                        </td>
                        <td><span class="reqstar">*</span> رقم الموبايل المفضل   
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox SkinID="txtsite" runat="server" ID="txtMobile2"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtMobile2" FilterType="Numbers" runat="server"></asp:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true" ControlToValidate="txtMobile2" ValidationGroup="10" runat="server" ErrorMessage="رقم الموبايل غير صحيح" ValidationExpression="01[0-2]{1}[0-9]{8}"></asp:RegularExpressionValidator>
                        </td>
                        <td>رقم موبايل 2
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:DropDownList SkinID="1" Width="310px" runat="server" ID="drpBooldType">
                                <asp:ListItem Value="">غير معروفة</asp:ListItem>
                                <asp:ListItem>O-</asp:ListItem>
                                <asp:ListItem>O+</asp:ListItem>
                                <asp:ListItem>B-</asp:ListItem>
                                <asp:ListItem>B+</asp:ListItem>
                                <asp:ListItem>A-</asp:ListItem>
                                <asp:ListItem>A+</asp:ListItem>
                                <asp:ListItem>AB-</asp:ListItem>
                                <asp:ListItem>AB+</asp:ListItem>
                                <asp:ListItem>AB+</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>فصيله الدم 
                        </td>
                    </tr>
                    <tr>
                        <td dir="rtl">
                            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="radWhatsApp">
                                <asp:ListItem Value="True">لدية حساب</asp:ListItem>
                                <asp:ListItem Selected="True" Value="False">ليس لدية حساب</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>WhatsApp</td>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox SkinID="txtsite" runat="server" ID="txtFacebook"></asp:TextBox>
                        </td>
                        <td>Facebook 
                        </td>
                    </tr>

                    <tr>

                        <td>
                            <asp:TextBox SkinID="txtsite" runat="server" ID="txtEmail"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2"
                                ValidationGroup="10" runat="server" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true" ControlToValidate="txtEmail" ValidationGroup="10" runat="server" ErrorMessage="خطا بالبريد الالكتروني" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                        <td><span class="reqstar">*</span> البريد الالكتروني   
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox SkinID="txtsite" runat="server" ID="txtSkype"></asp:TextBox>
                        </td>
                        <td>Skype 
                        </td>
                    </tr>
                    <tr>

                        <td dir="rtl">
                            <div style="OVERFLOW-Y: scroll; WIDTH: 300px; HEIGHT: 100px;">
                                <asp:CheckBoxList runat="server" RepeatDirection="Horizontal" RepeatColumns="1" ID="chkHobbies" DataSourceID="odsHobbies" DataTextField="HobbyName" DataValueField="HobbyId"></asp:CheckBoxList>
                                <asp:ObjectDataSource ID="odsHobbies" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.HobbiesManagement">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </td>
                        <td style="vertical-align: top">الهوايات</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lblMsg"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton runat="server" Style="float: left" ValidationGroup="10" OnClick="btnSave_Click" Text="حفظ البيانات" CssClass="btnLogin" ID="btnSave"></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
