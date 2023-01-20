<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ActivitySectionsManagement.aspx.cs" Inherits="System.Backend.manage.ActivitySectionsManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="فقرات الانشطة" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="فقرات الانشطة" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
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
                                SkinID="drpwList" DataValueField="ActivityId" OnDataBound="drpActivity_DataBound" />
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
                        <td>اسم الفقرة <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                ValidationGroup="1" runat="server" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>


                    <tr>
                        <td>تاريخ الفقرة <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDate"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" Format="d/M/yyyy" TargetControlID="txtDate" runat="server"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                ValidationGroup="1" runat="server" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
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
