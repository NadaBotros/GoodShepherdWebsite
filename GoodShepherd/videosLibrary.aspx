<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="videosLibrary.aspx.cs" Inherits="videosLibrary" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHHead" ID="contHead">
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
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
                            runat="server" ID="lblCatName" Text='<%# " » "+ Eval("ItemTitle") %>' NavigateUrl='<%# "videosLibrary.aspx?id="+Eval("LibraryItemId") %>'></asp:HyperLink>
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
                                        <asp:HyperLink ID="lnkch" runat="server" Text='<%# Eval("ItemTitle") %>' NavigateUrl='<%# "videosLibrary.aspx?id="+Eval("LibraryItemId") %>'
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
            <td style="padding-bottom: 20px">
                <asp:GridView SkinID="2350" Width="100%" ShowHeader="False" OnRowDataBound="grdCategories_RowDataBound" runat="server" ID="grdCategories"
                    AutoGenerateColumns="False" DataSourceID="odsData" BackColor="White" BorderStyle="None" BorderWidth="0px" CellPadding="4" ForeColor="Black"
                    GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="65%" ItemStyle-Width="65%">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" Style="color: black" CssClass="lnk33" ID="lnkTitle" Text='<%# "† "+ Eval("ItemTitle") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="35%" ItemStyle-Width="35%">
                            <ItemTemplate>
                                <table style="float: left">
                                    <tr>
                                        <td valign="top">
                                            <div>
                                                <a target="_blank" href="https://twitter.com/share" class="twitter-share-button" data-url='<%#"http://shepherdmeeting.com/videosLibrary.aspx?id="+Eval("LibraryItemId") %>'></a>
                                            </div>
                                        </td>
                                        <td valign="top">
                                            <div class="fb-share-button" data-href='<%#"http://shepherdmeeting.com/videosLibrary.aspx?id="+Eval("LibraryItemId") %>' data-type="button_count"></div>
                                        </td>
                                        <td valign="top">
                                            <div style="height: 20px">
                                                <script type="IN/Share" data-url='<%#"http://shepherdmeeting.com/videosLibrary.aspx?id="+Eval("LibraryItemId") %>' data-counter="right"></script>
                                            </div>
                                        </td>

                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Size="12px" Font-Names="ge_ss_twomedium" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" Height="20px" Font-Size="11px" Font-Names="ge_ss_twomedium" />
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
                <asp:Label ID="lblMsg" Style="font-family: ge_ss_twomedium; font-size: 12px" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="odsFiles" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.LibraryFilesManage">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                        <asp:QueryStringParameter DefaultValue="" Name="LibraryItemId" QueryStringField="id" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ListView ID="lstFiles" DataSourceID="odsFiles" GroupItemCount="4" runat="server">
                    <LayoutTemplate>
                        <table id="Table1" runat="server" cellspacing="3" style="margin: auto" cellpadding="0">
                            <tr runat="server" id="groupPlaceholder">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr runat="server" id="tableRow">
                            <td runat="server" id="itemPlaceholder" />
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <td id="itemPlaceholder" class="tdView" align="center" valign="top" runat="server">
                            <table style="margin: auto; text-align: center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="top">
                                        <asp:HyperLink runat="server" CssClass="lnkImage" NavigateUrl='<%#"VideosLibraryDetails.aspx?id="+Eval("FileId") %>' ID="lnkImage" ImageUrl='<%# DAL.GeneralMethods.GetYoutubeThum(Eval("YoutubeLink")) %>'></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href='<%#"VideosLibraryDetails.aspx?id="+Eval("FileId") %>'><%#Eval("FileTitle")+"<br/>"+Eval("FileOwner") %></a></td>
                                </tr>
                            </table>
                        </td>
                    </ItemTemplate>
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:DataPager PageSize="28" runat="server" ID="lstPager"
                    PagedControlID="lstFiles">
                    <Fields>
                        <asp:NumericPagerField ButtonCount="10" NumericButtonCssClass="numeric_button" CurrentPageLabelCssClass="current_page" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
        <tr>
            <td>
                <script src="//platform.linkedin.com/in.js" type="text/javascript">lang: en_US</script>
                <script>                                                    !function (d, s, id) {
                                                        var js, fjs = d.getElementsByTagName(s)[0];
                                                        if (!d.getElementById(id)) {
                                                            js = d.createElement(s);
                                                            js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs);
                                                        }
                                                    }
 (document, "script", "twitter-wjs");</script>
                <div id="fb-root"></div>
                <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=448197185202052";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
            </td>
        </tr>
    </table>
</asp:Content>
