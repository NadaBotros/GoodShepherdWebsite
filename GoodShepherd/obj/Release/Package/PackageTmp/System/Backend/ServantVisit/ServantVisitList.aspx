<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ServantVisitList.aspx.cs" EnableEventValidation="false" Inherits="System.Backend.ServantVisitList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="افتقاد الخادم" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="قائمة افتقاد الخادم" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                <asp:HyperLink runat="server" class="addnew" ID="btnAddNew" NavigateUrl="~/System/Backend/ServantVisit/ServantVisitsManagement.aspx"
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
                <asp:DropDownList runat="server" SkinID="drpwList" style="float:left" CssClass="drplist" AutoPostBack="True" DataTextField="PersonName" DataValueField="ServantId" ID="drpServant" DataSourceID="odsServants"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsServants" runat="server" SelectMethod="LoadAllList" TypeName="DAL.ServantAftkadManagement">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="True" Name="IsServantAftkad" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Label runat="server" ID="lblsearch" CssClass="lblSearch" Text="بحث : "></asp:Label>
                <asp:TextBox ID="txSearch" SkinID="txtSearch" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdData" Width="100%" runat="server" CssClass="grd" DataSourceID="odsData"
                    AutoGenerateColumns="False" OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="PersonCode" HeaderText="الكود" SortExpression="PersonCode">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PersonName" HeaderText="اسم المخدوم" SortExpression="PersonName">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="موبايل" SortExpression="Mobile">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VisitDate" HeaderText="تاريخ الزياره" SortExpression="VisitDate">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RemiderDate" HeaderText="تاريخ التذكير" SortExpression="RemiderDate">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="imgEdit" runat="server" CssClass="imgIcon3 tip_right_top" NavigateUrl='<%#"ServantVisitsManagement.aspx?id="+Eval("ServantVisitId")%>'
                                    ImageUrl="~/System/Backend/lib/img/Edit.png" title="Edit"></asp:HyperLink>
                                <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/Delete.png"
                                    CommandName="deleteitem" CommandArgument='<%# Eval("ServantVisitId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                    title="Delete Item"></asp:ImageButton>
                                <asp:Image ID="imgInfo" CssClass="imgIcon3 tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),Eval("ModifiedOn"),Eval("ModifiedBy")) %>'
                                    ImageUrl="../lib/img/Info.png" runat="server" />
                            </ItemTemplate>
                        
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="Numeric" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="ServantVisits"
                    TypeName="DAL.ServantVisitsManagement">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpServant" Name="ServantId" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
