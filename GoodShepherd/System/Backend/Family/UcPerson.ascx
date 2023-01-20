<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcPerson.ascx.cs" Inherits="System.Backend.UcPerson" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .imgPerson img {
        max-height: 100px;
        max-width: 100px;
    }
</style>
<table cellpadding="2">
    <tr>
        <td id="msg" runat="server">
            <asp:Label ID="lblMessge" Style="display: block; width: 100%" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding="3" cellspacing="0">

                <tr>
                    <td>الاسم بالكامل <span class="reqstar">*</span>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFullName"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            ValidationGroup="11" runat="server" ControlToValidate="txtFullName"></asp:RequiredFieldValidator>
                    </td>
                    <td>الكود
                    </td>
                    <td>
                        <asp:TextBox ReadOnly="true" runat="server" ID="txtCode"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>القرابة
                    </td>
                    <td>
                        <asp:DropDownList runat="server" SkinID="drpwList" ID="drpRelationship">
                            <asp:ListItem>الزوج</asp:ListItem>
                            <asp:ListItem>الزوجة</asp:ListItem>
                            <asp:ListItem>ابن</asp:ListItem>
                            <asp:ListItem>ابنة</asp:ListItem>
                            <asp:ListItem>اخ</asp:ListItem>
                            <asp:ListItem>اخت</asp:ListItem>
                            <asp:ListItem>والد الزوج</asp:ListItem>
                            <asp:ListItem>والدة الزوج</asp:ListItem>
                            <asp:ListItem>والد الزوجة</asp:ListItem>
                            <asp:ListItem>والدة الزوجة</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>الرقم القومي <span class="reqstar">*</span>
                    </td>
                    <td>
                        <asp:TextBox runat="server" MaxLength="14" ID="txtNationalId"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtNationalId" FilterType="Numbers" runat="server"></asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2"
                            ValidationGroup="11" runat="server" ControlToValidate="txtNationalId"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="reqNational" SetFocusOnError="true" ControlToValidate="txtNationalId" ValidationGroup="11" runat="server" ErrorMessage="لابد من ادخال 14 رقم" ValidationExpression="\d{14}"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>الحالة الاجتماعية  <span class="reqstar">*</span>
                    </td>
                    <td>
                        <asp:DropDownList SkinID="drpwList" runat="server" ID="drpMaritalStatus">
                            <asp:ListItem>اعزب</asp:ListItem>
                            <asp:ListItem>متزوج</asp:ListItem>
                            <asp:ListItem>ارمل</asp:ListItem>
                            <asp:ListItem>مطلق</asp:ListItem>
                            <asp:ListItem>متوفي</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>حضور الاجتماع
                    </td>
                    <td>
                        <asp:DropDownList SkinID="drpwlist" runat="server" RepeatDirection="Horizontal" ID="drpStudious">
                            <asp:ListItem Selected="True" Value="1">يحضر بالكنيسة</asp:ListItem>
                            <asp:ListItem  Value="2">يحضر بالقاعة</asp:ListItem>
                            <asp:ListItem Value="0">لا يحضر</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td>تاريخ الميلاد 
                    </td>
                    <td dir="ltr" style="text-align: right">
                        <asp:CalendarExtender ID="CalendarExtender1" Format="d/M/yyyy" TargetControlID="txtBirthDate" runat="server"></asp:CalendarExtender>
                        <asp:TextBox runat="server" ID="txtBirthDate"></asp:TextBox>
                    </td>
                    <td>اب الاعتراف
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFatherName"></asp:TextBox>
                        <asp:AutoCompleteExtender OnClientShown="ShowOptions" ID="txtStreet_AutoCompleteExtender" runat="server" ServiceMethod="GetFatherNameList" MinimumPrefixLength="1" TargetControlID="txtFatherName">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td>الوظيفة
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJob"></asp:TextBox>
                        <asp:AutoCompleteExtender OnClientShown="ShowOptions" ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetJobList" MinimumPrefixLength="1" TargetControlID="txtJob">
                        </asp:AutoCompleteExtender>
                    </td>
                    <td>جهة العمل
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJobPlace"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>رقم الموبايل المفضل  
                    </td>
                    <td>
                        <asp:TextBox MaxLength="11" runat="server" ID="txtMobile"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtMobile" FilterType="Numbers" runat="server"></asp:FilteredTextBoxExtender>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" ControlToValidate="txtMobile" ValidationGroup="11" runat="server" ErrorMessage="رقم الموبايل غير صحيح" ValidationExpression="01[0-2]{1}[0-9]{8}"></asp:RegularExpressionValidator>--%>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" ControlToValidate="txtMobile" ValidationGroup="11" runat="server" ErrorMessage="رقم الموبايل غير صحيح" ValidationExpression="01[0,1,2,5]{1}[0-9]{8}"></asp:RegularExpressionValidator>

                        </td>
                    <td>رقم موبايل 2
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMobile2"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtMobile2" FilterType="Numbers" runat="server"></asp:FilteredTextBoxExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true" ControlToValidate="txtMobile2" ValidationGroup="11" runat="server" ErrorMessage="رقم الموبايل غير صحيح" ValidationExpression="01[0,1,2,5]{1}[0-9]{8}"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>فصيله الدم 
                    </td>
                    <td>
                        <asp:DropDownList SkinID="drpwList" runat="server" ID="drpBooldType">
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
                    <td>WhatsApp</td>
                    <td>
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="radWhatsApp">
                            <asp:ListItem Value="True">لدية حساب</asp:ListItem>
                            <asp:ListItem Selected="True" Value="False">ليس لدية حساب</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td>Facebook 
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFacebook"></asp:TextBox>
                    </td>
                    <td rowspan="3" style="vertical-align: top">الهوايات</td>
                    <td rowspan="3">
                        <table width="100%">
                            <tr>
                                <td valign="top">
                                    <div style="overflow-y: scroll; width: 100%; height: 100px;">
                                        <asp:CheckBoxList runat="server" CellSpacing="1" CellPadding="1" RepeatDirection="Horizontal" RepeatColumns="1" ID="chkHobbies" DataSourceID="odsHobbies" DataTextField="HobbyName" DataValueField="HobbyId"></asp:CheckBoxList>
                                        <asp:ObjectDataSource ID="odsHobbies" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.HobbiesManagement">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </div>
                                </td>
                                <td valign="top">
                                    <asp:HyperLink Target="_blank" runat="server" CssClass="imgPerson" ID="lnkImage"></asp:HyperLink>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr>
                    <td>البريد الالكتروني
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true" ControlToValidate="txtEmail" ValidationGroup="11" runat="server" ErrorMessage="خطا بالبريد الالكتروني" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Skype 
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSkype"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>رفع الصوره</td>
                    <td colspan="3">
                        <asp:FileUpload runat="server" ID="upldImage" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
