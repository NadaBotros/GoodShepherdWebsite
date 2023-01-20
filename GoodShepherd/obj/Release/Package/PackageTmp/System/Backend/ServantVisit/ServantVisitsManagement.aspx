<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ServantVisitsManagement.aspx.cs" Inherits="System.Backend.manage.ServantVisitsManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="زيارات الخدام" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="اضافه زياره خادم" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
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
                        <td>اسم الخادم <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" SkinID="drpwList"  AutoPostBack="True" DataTextField="PersonName" DataValueField="ServantId" ID="drpServant" DataSourceID="odsServants" OnDataBound="drpServant_DataBound"></asp:DropDownList>
                            <asp:ObjectDataSource ID="odsServants" runat="server" SelectMethod="LoadAllList" TypeName="DAL.ServantAftkadManagement">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="True" Name="IsServantAftkad" Type="Boolean" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ControlToValidate="drpServant" ID="RequiredFieldValidator1" ValidationGroup="1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>اسم المخدوم <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" SkinID="drpwList" DataTextField="PersonName" DataValueField="PersonId" ID="drpPerson" DataSourceID="odsPersons" OnDataBound="drpPerson_DataBound"></asp:DropDownList>
                            <asp:ObjectDataSource ID="odsPersons" runat="server" SelectMethod="ServantPersons" TypeName="DAL.ServantPersonsManagement">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="drpServant" Name="ServantId" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ControlToValidate="drpPerson" ID="RequiredFieldValidator2" ValidationGroup="1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>تاريخ الزيارة <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtVisitDate"></asp:TextBox>
                            <asp:CalendarExtender Format="d/M/yyyy" ID="CalendarExtender1" TargetControlID="txtVisitDate" runat="server"></asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ControlToValidate="txtVisitDate" ID="RequiredFieldValidator3" ValidationGroup="1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>ملاحظات الزياره 
                        </td>
                        <td>
                            <asp:TextBox  SkinID="txtmult" runat="server" ID="txtNotes" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>هل الملاحظه هامه 
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="radNotesType">
                                <asp:ListItem Value="True">نعم</asp:ListItem>
                                <asp:ListItem Value="False" Selected="True">لا</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>تاريخ التنبيه
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReminder"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" Format="d/M/yyyy" TargetControlID="txtReminder" runat="server"></asp:CalendarExtender>
                        </td>
                        <td></td>
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
