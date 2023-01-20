<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="CountriesTrafic.aspx.cs"  Inherits="System.Backend.CountriesTrafic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img class="imgIcon" src="../lib/icons/32/Reports.PNG" /><asp:Label runat="server"
        Text="احصائيات الزوار" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="تفاصبل زوار الموقع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
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
                <asp:GridView ID="grdData" runat="server" CssClass="grd" DataSourceID="odsData" AutoGenerateColumns="false"
                    OnRowDataBound="grdData_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="الدولة" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="border: 0px; padding: 2px">
                                            <asp:Image Width="16px" ID="ImageFlag" runat="server" ImageUrl='<%# GetCompleteUrl("flags/"+Eval("FlagImage"))%>' />
                                        </td>
                                        <td style="border: 0px; padding: 2px">
                                            <asp:Label runat="server" Text='<%#Eval("CountryName") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ItemStyle-Width="20%" DataField="IpAddress" HeaderText="Ip Address"
                            HeaderStyle-Width="20%" />
                      
                        <asp:BoundField ItemStyle-Width="20%" DataField="VisitPages" HeaderText="عدد الصفحات التي تم زيارتها"
                            HeaderStyle-Width="20%" />
                        <asp:BoundField ItemStyle-Width="20%" DataField="CreatedOn" HeaderText="وقت الزياره"
                            HeaderStyle-Width="20%" />
                    </Columns>
                    <PagerSettings Mode="Numeric" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadAllVisitors"
                    TypeName="DAL.VisitorManagement"></asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
