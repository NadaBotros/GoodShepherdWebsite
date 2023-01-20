<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ServantPersonsList.aspx.cs" EnableEventValidation="false" Inherits="System.Backend.ServantPersonsList" %>

<%@ Register Src="~/System/Backend/UserControls/UcPersonGrid.ascx" TagPrefix="uc1" TagName="UcPersonGrid" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="مجموعات افتقاد الخدام" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="قائمة  افتقاد الخدام" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                <asp:HyperLink runat="server" class="addnew" ID="btnAddNew" NavigateUrl="~/System/Backend/ServantPersons/ServantPersonsManagement.aspx"
                    Text="اضافة جديد ...."></asp:HyperLink>
            </td>
        </tr>        
        <tr>
            <td>
                <table cellpadding="3">
                    <tr>
                        <td>اختر خادم الافتقاد</td>
                        <td>
                            <asp:DropDownList runat="server" AutoPostBack="true" Width="250px" DataTextField="PersonName" DataValueField="ServantId" ID="drpServant" DataSourceID="odsServants" OnSelectedIndexChanged="drpServant_SelectedIndexChanged"></asp:DropDownList>
                            <asp:ObjectDataSource ID="odsServants" runat="server" SelectMethod="LoadAllList" TypeName="DAL.ServantAftkadManagement">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="True" Name="IsServantAftkad" Type="Boolean" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" Text="حذف الاسماء المختارة من الخادم" runat="server" OnClick="btnDelete_Click" />
                            <asp:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText="هل انت متاكد من حذف الاسماء المختارة من قائمة افتقاد الخادم ؟" TargetControlID="btnDelete">
                            </asp:ConfirmButtonExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>نقل المخدومين الى الخادم </td>
                        <td>
                            <asp:DropDownList runat="server" Width="250px" DataTextField="PersonName" DataValueField="ServantId" ID="drpServantTransfer" DataSourceID="odsServants"></asp:DropDownList>

                        </td>

                        <td>
                            <asp:Button runat="server" Text="ربط الاسماء المختارة بالخادم الجديد" ID="btnTransfer" OnClick="btnTransfer_Click" />
                            <asp:ConfirmButtonExtender ID="btnTransfer_ConfirmButtonExtender" ConfirmText="هل انت متاكد من نقل المخدومين الي الخادم المختار ؟" runat="server" TargetControlID="btnTransfer">
                            </asp:ConfirmButtonExtender>
                        </td>
                       
                    </tr>
                   
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UcPersonGrid runat="server" ID="UcPersonGrid"  />
            </td>
        </tr>
    </table>
</asp:Content>
