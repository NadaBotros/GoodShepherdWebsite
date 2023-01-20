<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ServantAftkadList.aspx.cs" EnableEventValidation="false" Inherits="System.Backend.ServantAftkadList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="خدام الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="قائمة خدام الاجتماع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                <asp:HyperLink runat="server" class="addnew" ID="btnAddNew" NavigateUrl="~/System/Backend/ServantAftkad/ServantAftkadManagement.aspx"
                    Text="اضافة جديد ...."></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:DropDownList ID="drpExport" CssClass="drplist" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpExport_SelectedIndexChanged">
                    <asp:ListItem Value="" Selected="True" Text="تحويل الى ملف"></asp:ListItem>
                    <asp:ListItem Value="pdf" Text="PDF"></asp:ListItem>
                    <asp:ListItem Value="word" Text="Word"></asp:ListItem>
                    <asp:ListItem Value="exel" Text="Exel"></asp:ListItem>
                </asp:DropDownList>

                <asp:Label runat="server" ID="lblsearch" CssClass="lblSearch" Text="بحث : "></asp:Label>
                <asp:TextBox ID="txSearch" SkinID="txtSearch" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdData" Width="100%" runat="server" CssClass="grd" DataSourceID="odsData"
                    AutoGenerateColumns="False" OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">
                    <Columns>

                        <asp:BoundField DataField="PersonName" HeaderText="اسم الخادم" SortExpression="PersonName">
                            <HeaderStyle Width="30%" />
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="رقم الموبايل" SortExpression="Mobile">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Services" HeaderText="خدمات الخادم" SortExpression="Services">
                            <HeaderStyle Width="40%" />
                            <ItemStyle Width="40%" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                  <asp:HyperLink ID="imgEdit" runat="server" CssClass="imgIcon3 tip_right_top" NavigateUrl='<%#"ServantAftkadManagement.aspx?id="+Eval("ServantId")%>'
                                    ImageUrl="~/System/Backend/lib/img/Edit.png" title="تعديل"></asp:HyperLink>
                                <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/Delete.png"
                                    CommandName="deleteitem" CommandArgument='<%# Eval("servantId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                    title="Delete Item"></asp:ImageButton>
                                 <asp:Image ID="imgInfo" CssClass="imgIcon3 tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),Eval("ModifiedOn"),Eval("ModifiedBy")) %>'
                                    ImageUrl="../lib/img/Info.png" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="Numeric" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadAll"
                    TypeName="DAL.ServantAftkadManagement"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnPopup" style="display:none" runat="server" />
                <asp:ModalPopupExtender ID="MPEPersonInfo" runat="server"
                    TargetControlID="btnPopup"
                    PopupControlID="pnlPoup"
                    BackgroundCssClass="modalBackground"
                    DropShadow="True"
                    CancelControlID="lnkClosePopup" Enabled="True">
                </asp:ModalPopupExtender>
                <asp:Panel runat="server" CssClass="dvPopup" ID="pnlPoup">

                    <table>
                        <tr>
                            <td>
                                <asp:HyperLink ID="lnkClosePopup"  Style="float: left;width:25px" runat="server">
                                    <asp:Image runat="server" ImageUrl="~/System/Backend/lib/img/close.png" Width="20px" />
                                </asp:HyperLink>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="padding-top:15px">
                                <asp:Label runat="server" style="font-size:14px" Text="هذا الخادم مرتبط بمخدمين فلابد من نقل المخدمين اولا ثم يرجي محاوله حذفه" CssClass="tdPageTitle" ID="Label1"></asp:Label>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
