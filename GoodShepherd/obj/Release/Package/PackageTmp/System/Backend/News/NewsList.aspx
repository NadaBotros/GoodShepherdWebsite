<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="NewsList.aspx.cs" EnableEventValidation="false" Inherits="System.Backend.NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="اخبار الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/User.png" />
                <asp:Label runat="server" Text="قائمة اخبار الاجتماع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                <asp:HyperLink runat="server" class="addnew" ID="btnAddNew" NavigateUrl="~/System/Backend/News/NewsManagement.aspx"
                    Text="اضافة جديد ..."></asp:HyperLink>
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
                <asp:DropDownList ID="drpViews" CssClass="drplist drpRecords" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="True" Selected="True" Text="نوع العرض"></asp:ListItem>
                    <asp:ListItem Value="True" Text="البيانات الحالية"></asp:ListItem>
                    <asp:ListItem Value="False" Text="البيانات المحذوفة"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="drpNewsTypes" DataTextField="NewsTypeName" DataValueField="NewsTypeId" CssClass="drplist drpRecords" OnDataBound="drpNewsType_DataBound" runat="server" AutoPostBack="True" DataSourceID="odsNewsTypes">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsNewsTypes" runat="server" SelectMethod="LoadByDeleteState"
                    TypeName="DAL.NewsTypesManagement">
                    <SelectParameters>
                        <asp:Parameter Name="Active" Type="string" DefaultValue="true" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Label runat="server" ID="lblsearch" CssClass="lblSearch" Text="بحث : "></asp:Label>
                <asp:TextBox ID="txSearch" SkinID="txtSearch" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdData" Width="100%"  runat="server" CssClass="grd" DataSourceID="odsData" AutoGenerateColumns="False"
                    OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="NewsTypeName" HeaderText="قسم الخبر" SortExpression="NewsTypeName">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NewsTitle" HeaderText="عنوان الخبر" SortExpression="NewsTitle">
                            <ItemStyle Width="65%" />
                            <HeaderStyle Width="65%" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="imgInfo" CssClass="imgIcon3 tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),Eval("ModifiedOn"),Eval("ModifiedBy")) %>'
                                    ImageUrl="../lib/img/Info.png" runat="server" />
                                <asp:ImageButton ID="imgRestore" runat="server" CssClass="imgIcon3 tip_right_top"
                                    class="tip_right_top" ImageUrl="../lib/img/Restor.png" CommandName="restoreitem"
                                    CommandArgument='<%# Eval("NewsId") %>' Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"true") %>'
                                    ToolTip="استعادة المحذوف"></asp:ImageButton>
                                <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/Delete.png"
                                    CommandName="deleteitem" CommandArgument='<%# Eval("NewsId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                    Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"false") %>'
                                    title="حذف"></asp:ImageButton>
                                <asp:HyperLink ID="imgEdit" runat="server" CssClass="imgIcon3 tip_right_top" NavigateUrl='<%#"NewsManagement.aspx?id="+Eval("NewsId")%>'
                                    ImageUrl="~/System/Backend/lib/img/Edit.png" title="تعديل"></asp:HyperLink>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="Numeric" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadByDeleteState"
                    TypeName="DAL.NewsManagement">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpViews" Name="Active" PropertyName="SelectedValue"
                            Type="string" />
                        <asp:ControlParameter ControlID="drpNewsTypes" Name="NewsTypeId" PropertyName="SelectedValue"
                            Type="string" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
