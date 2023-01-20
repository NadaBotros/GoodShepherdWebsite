<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ActivityUsersManagement.aspx.cs" Inherits="System.Backend.manage.ActivityUsersManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="المشركين بالنشاط" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="المشركين بالنشاط" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
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
                        <td>النشاط <span class="reqstar">*</span>
                        </td>
                        <td>
                           <asp:DropDownList runat="server" ID="drpActivity" DataSourceID="odsActivities" DataTextField="ActivityTitle" 
                              SkinID="drpwList" DataValueField="ActivityId" OnDataBound="drpActivity_DataBound"/>
                            <asp:ObjectDataSource ID="odsActivities" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.ActivitiesManage">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                ValidationGroup="1" runat="server" ControlToValidate="drpActivity"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>الاسم <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                ValidationGroup="1" runat="server" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>الكود <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCode"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                ValidationGroup="1" runat="server" ControlToValidate="txtCode"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>الموبايل الاساسي
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMobile"></asp:TextBox>
                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtMobile" FilterType="Numbers" runat="server"></asp:FilteredTextBoxExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true" ControlToValidate="txtMobile" ValidationGroup="11" runat="server" ErrorMessage="رقم الموبايل غير صحيح" ValidationExpression="01[0-2]{1}[0-9]{8}"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>الموبايل 2
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMobile2"></asp:TextBox>
                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtMobile2" FilterType="Numbers" runat="server"></asp:FilteredTextBoxExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" ControlToValidate="txtMobile2" ValidationGroup="11" runat="server" ErrorMessage="رقم الموبايل غير صحيح" ValidationExpression="01[0-2]{1}[0-9]{8}"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>رقم الغرفة 
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRoomNo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>ملاحظات
                        </td>
                        <td>
                            <asp:TextBox SkinID="txtmult" runat="server" TextMode="MultiLine" ID="txtNotes"></asp:TextBox>
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
