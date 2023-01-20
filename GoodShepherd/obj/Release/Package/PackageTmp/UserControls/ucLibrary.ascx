<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLibrary.ascx.cs" Inherits="ucLibrary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<table>
    <tr>
        <td>
            <div class="dvMainPageTitle">
                <div class="dvTitle">
                    <asp:Label runat="server" ID="lblTitle"></asp:Label>
                </div>
                <div class="dvTitleBg"></div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Repeater runat="server" ID="rptLibrary" OnItemDataBound="rptLibrary_ItemDataBound" DataSourceID="odsPath">
                <ItemTemplate>
                    <asp:HyperLink Style="padding-top: 5px; padding-bottom: 5px; color: black; float: right; font-weight: bold; font-size: 13px; padding-left: 7px; padding-right: 7px; display: inline-block; text-decoration: none"
                        runat="server" ID="lblCatName" Text='<%# " » "+ Eval("ItemTitle") %>' NavigateUrl='<%# Generatelink(Eval("LibraryItemId")) %>'></asp:HyperLink>
                    <asp:DropDownExtender ID="DropDownExtender1" runat="server" TargetControlID="lblCatName" DropDownControlID="grdChildCats"
                        HighlightBackColor="White" HighlightBorderColor="Gray" DropArrowBackColor="White"
                        Enabled="True">
                    </asp:DropDownExtender>
                    <asp:Label runat="server" ID="lblId" Visible="false" Text='<%#Eval("ParentItemId") %>'></asp:Label>
                    <asp:GridView runat="server" ID="grdChildCats" DataSourceID="odsCatParent" CssClass="DropPanel"
                        CellPadding="2" SkinID="grdsite3" ShowHeader="false" RowStyle-BackColor="White" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" AutoGenerateColumns="false" Width="160px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkch" runat="server" Text='<%# Eval("ItemTitle") %>' NavigateUrl='<%# Generatelink(Eval("LibraryItemId")) %>'
                                        CssClass="link5"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="odsCatParent" runat="server" SelectMethod="LoadByDeleteState"
                        TypeName="DAL.LibraryManage">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                            <asp:Parameter Name="ParentItemId" Type="String" />
                            <asp:Parameter DefaultValue="" Name="LibraryType" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </ItemTemplate>
            </asp:Repeater>
            <asp:ObjectDataSource ID="odsPath" runat="server" SelectMethod="GetPath" TypeName="DAL.LibraryManage">
                <SelectParameters>
                    <asp:QueryStringParameter Name="Id" QueryStringField="id" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView SkinID="2350" ShowHeader="False" Style="border-collapse: separate;" OnRowDataBound="grdCategories_RowDataBound" runat="server" ID="grdCategories" AutoGenerateColumns="False" DataSourceID="odsData" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="70%" ItemStyle-Width="70%">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="lnkTitle" Text='<%# "† "+ Eval("ItemTitle") %>'></asp:HyperLink>
                        </ItemTemplate>

                        <HeaderStyle Width="70%"></HeaderStyle>

                        <ItemStyle Width="70%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="30%" ItemStyle-Width="30%">
                        <ItemTemplate><table style="float: left">
                            <tr>
                                <td>
                                    <div>
                                        <a target="_blank" href="https://twitter.com/share" class="twitter-share-button" data-url='<%#"http://shepherdmeeting.com/MeetingDetails.aspx?mid="+Eval("MeetingId") %>'></a>
                                    </div>
                                </td>
                                <td>
                                    <div class="fb-share-button" data-href='<%#"http://shepherdmeeting.com/MeetingDetails.aspx?mid="+Eval("MeetingId") %>' data-type="button_count"></div>
                                </td>

                                <td>
                                    <div>
                                        <script type="IN/Share" data-url='<%#"http://shepherdmeeting.com/MeetingDetails.aspx?mid="+Eval("MeetingId") %>' data-counter="right"></script>
                                    </div>
                                </td>

                            </tr>
                        </table>
                        </ItemTemplate>
                        
                        <HeaderStyle Width="30%"></HeaderStyle>
                        <ItemStyle Width="30%"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
            <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadByDeleteState"
                TypeName="DAL.LibraryManage">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                    <asp:QueryStringParameter Name="ParentItemId" QueryStringField="id" Type="String" />
                    <asp:Parameter DefaultValue="" Name="LibraryType" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server"></asp:Label></td>
    </tr>
</table>
