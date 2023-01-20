<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="NewsManagement.aspx.cs" Inherits="System.Backend.manage.NewsManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ajax__html_editor_extender_texteditor {
            background-color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="اخبار الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">

    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/User.png" />
                <asp:Label runat="server" Text="ادارة اخبار الاجتماع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
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
                        <td width="17%">قسم الخبر <span class="reqstar">*</span>
                        </td>
                        <td width="83%">
                            <asp:DropDownList Style="float: right" runat="server" DataSourceID="odsNewsTypes" DataTextField="NewsTypeName" DataValueField="NewsTypeId" ID="drpNewsType"></asp:DropDownList>
                            <asp:ObjectDataSource ID="odsNewsTypes" runat="server" SelectMethod="LoadByDeleteState"
                                TypeName="DAL.NewsTypesManagement">
                                <SelectParameters>
                                    <asp:Parameter Name="Active" Type="string" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td width="17%">عنوان الخبر <span class="reqstar">*</span>
                        </td>
                        <td width="83%">
                            <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                ValidationGroup="1" runat="server" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>يظهر فى شريط الاخبار <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="radShowInNewsBar" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True" Text="يظهر" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="False" Text="لا يظهر"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>يظهر فى جاليري الاخبار <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="radShowInGallery" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True" Text="يظهر" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="False" Text="لا يظهر"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>رفع صورة الخبر <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:FileUpload ID="fupldImage" runat="server" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Image runat="server" CssClass="imgborder"
                                ID="imgNews" />
                        </td>
                    </tr>
                    <tr>
                        <td>نص الخبر
                        </td>
                        <td dir="ltr">
                            <cc1:Editor ID="EditorNews" Width="700px" Height="500px" runat="server" />
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td class="tdbtns">
                <asp:Button ID="btnBack" runat="server" Text="رجوع للصفحة السابقة" OnClick="btnBack_Click" />
                <asp:Button ID="btnClear" runat="server" Text="تفريغ الخانات" OnClick="btnClear_Click" />
                <asp:Button ID="btnSaveAndNew" ValidationGroup="1" runat="server" Text="حفظ البيانات واضافة جديد"
                    OnClick="btnSaveAndNew_Click" />


                <asp:Button ID="btnSave" ValidationGroup="1" runat="server" Text="حفظ البيانات" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>


</asp:Content>
