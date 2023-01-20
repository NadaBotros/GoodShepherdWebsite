<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ImagesSizeList.aspx.cs" Inherits="System.Backend.ImagesSizeList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
  <img src="../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="Images Size" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="Images Size List" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                <asp:HyperLink runat="server" class="addnew" ID="btnAddNew" NavigateUrl="ImagesSizeManagement.aspx"
                    Text="Add New..."></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td align="right">
            <asp:DropDownList ID="drpExport" CssClass="drplist" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpExport_SelectedIndexChanged">
                    <asp:ListItem Value="" Selected="True" Text="Export File"></asp:ListItem>
                    <asp:ListItem Value="pdf" Text="PDF"></asp:ListItem>
                    <asp:ListItem Value="word" Text="Word"></asp:ListItem>
                    <asp:ListItem Value="exel" Text="Exel"></asp:ListItem>                   
                </asp:DropDownList>
                <asp:DropDownList ID="drpViews" CssClass="drplist drpRecords" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="True" Selected="True" Text="Select View"></asp:ListItem>
                    <asp:ListItem Value="True" Text="Active"></asp:ListItem>
                    <asp:ListItem Value="False" Text="Deleted"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txSearch" SkinID="txtSearch" runat="server"></asp:TextBox>
                <asp:Label runat="server" ID="lblsearch" CssClass="lblSearch" Text="Search : "></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdData" runat="server" CssClass="grd" DataSourceID="odsData" AutoGenerateColumns="False"
                    OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Width" HeaderText="Size Width">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Height" HeaderText="Size Height">
                            <ItemStyle Width="25%" />
                            <HeaderStyle Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Section" HeaderText="Section">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="25%" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="imgInfo" CssClass="imgIcon tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),Eval("ModifiedOn"),Eval("ModifiedBy")) %>'
                                    ImageUrl="../lib/img/Info.png" runat="server" />
                                <asp:ImageButton ID="imgRestore" runat="server" CssClass="imgIcon tip_right_top"
                                    class="tip_right_top" ImageUrl="../lib/img/Restor.png" CommandName="restoreitem"
                                    CommandArgument='<%# Eval("ImagesSizeId") %>' Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"true") %>'
                                    ToolTip="Restore Item"></asp:ImageButton>
                                <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon tip_right_top" ImageUrl="../lib/img/Delete.png"
                                    CommandName="deleteitem" CommandArgument='<%# Eval("ImagesSizeId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                    Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"false") %>'
                                    title="Delete Item"></asp:ImageButton>
                                <asp:HyperLink ID="imgEdit" runat="server" CssClass="imgIcon tip_right_top" NavigateUrl='<%#"ImagesSizeManagement.aspx?id="+Eval("ImagesSizeId")%>'
                                    ImageUrl="../lib/img/Edit.png" title="Edit"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="25%" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="Numeric" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadByDeleteState"
                    TypeName="DAL.ImagesSizesManagement">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpViews" Name="DS" PropertyName="SelectedValue"
                            Type="string" DefaultValue="true" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
