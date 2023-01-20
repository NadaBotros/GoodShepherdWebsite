<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="AreaManagement.aspx.cs" Inherits="System.Backend.manage.AreaManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="مناطق المخدومين" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="مناطق المخدومين" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
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
                        <td>اسم المدينة <span class="reqstar">*</span>
                        </td>
                        <td>
                          <asp:DropDownList DataTextField="CityName" SkinID="drpwList" DataValueField="CityId" runat="server" ID="drpCity" DataSourceID="odsCity">

                          </asp:DropDownList>
                            <asp:ObjectDataSource ID="odsCity" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.CityManagement">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>اسم المنطقة <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server"  ID="txtName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                ValidationGroup="1" runat="server" ControlToValidate="txtName"></asp:RequiredFieldValidator>
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
