<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="PageTreeManagement.aspx.cs" Inherits="System.Backend.manage.PageTreeManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="قائمة الموقع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="قائمة الموقع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="msg" runat="server">
                <asp:Label ID="lblMessge" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>                       
                        <td valign="top">
                            <table cellpadding="2" cellspacing="2">
                               
                                <tr>
                                    <td>القائمة الرئيسية</td>
                                    <td>
                                        <asp:DropDownList OnDataBound="drpCategory_DataBound" SkinID="drpwList" runat="server" ID="drpCategory" DataSourceID="odsCategories" DataTextField="PageTitle" DataValueField="SiteTreeId"></asp:DropDownList>
                                        <asp:ObjectDataSource ID="odsCategories" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.SiteTreeManage">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                                <asp:Parameter DefaultValue="" Name="ParentSiteTreeId" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>اسم الصفحة <span class="reqstar">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" Style="width: 550px" ID="txtName"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            ValidationGroup="1" runat="server" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td>لينك القسم
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" Style="width: 550px" ID="txtPageFile"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                            </table>
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
