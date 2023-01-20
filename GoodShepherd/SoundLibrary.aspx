<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="SoundLibrary.aspx.cs" Inherits="SoundLibrary" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <div id="fb-root"></div>
    <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=448197185202052";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
    <script src="//platform.linkedin.com/in.js" type="text/javascript">
 lang: en_US
    </script>
    <script>                                                    !function (d, s, id) {
                                                        var js, fjs = d.getElementsByTagName(s)[0];
                                                        if (!d.getElementById(id)) {
                                                            js = d.createElement(s);
                                                            js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs);
                                                        }
                                                    }
 (document, "script", "twitter-wjs");</script>
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
                            runat="server" ID="lblCatName" Text='<%# " » "+ Eval("ItemTitle") %>' NavigateUrl='<%# "SoundLibrary.aspx?id="+Eval("LibraryItemId") %>'></asp:HyperLink>
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
                                        <asp:HyperLink ID="lnkch" runat="server" Text='<%# Eval("ItemTitle") %>' NavigateUrl='<%# "SoundLibrary.aspx?id="+Eval("LibraryItemId") %>'
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
                                                <a target="_blank" href="https://twitter.com/share" class="twitter-share-button" data-url='<%#"http://shepherdmeeting.com/shepherdmeeting.com/SoundLibrary.aspx?id="+Eval("LibraryItemId") %>'></a>
                                            </div>
                                        </td>
                                        <td valign="top">
                                            <div class="fb-share-button" data-href='<%#"http://shepherdmeeting.com/shepherdmeeting.com/SoundLibrary.aspx?id="+Eval("LibraryItemId") %>' data-type="button_count"></div>
                                        </td>
                                        <td valign="top">
                                            <div style="height: 20px">
                                                <script type="IN/Share" data-url='<%#"http://shepherdmeeting.com/shepherdmeeting.com/SoundLibrary.aspx?id="+Eval("LibraryItemId") %>'></script>
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
                <asp:GridView SkinID="grd320" AllowPaging="true" PageSize="12" Style="border-collapse: separate; width: 100%" runat="server"
                    ID="grdFiles" AutoGenerateColumns="False" DataSourceID="odsFiles" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    CellPadding="4" PagerSettings-Mode="Numeric" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="عنوان الملف">
                            <ItemTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="font-weight: bold"><a style="color: black" href='<%#"SoundLibraryDetails.aspx?id="+Eval("FileId") %>'><%#Eval("FileTitle") %></a></td>
                                    </tr>
                                    <tr>
                                        <td style="font-style: italic"><%#Eval("FileOwner") %>  </td>
                                    </tr>
                                    <tr>
                                        <td style="font-style: italic"><%#Eval("FileDesc") %> </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="float: left">
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <a target="_blank" href="https://twitter.com/share" class="twitter-share-button" data-url='<%#"http://shepherdmeeting.com/shepherdmeeting.com/SoundLibraryDetails.aspx?id="+Eval("FileId") %>'></a>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="fb-share-button" data-href='<%#"http://shepherdmeeting.com/shepherdmeeting.com/SoundLibraryDetails.aspx?id="+Eval("FileId") %>' data-type="button_count"></div>
                                                    </td>

                                                    <td>
                                                        <div>
                                                            <script type="IN/Share" data-url='<%# "http://shepherdmeeting.com/shepherdmeeting.com/SoundLibraryDetails?id="+Eval("FileId") %>'></script>
                                                        </div>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="تاريخ الملف" ItemStyle-HorizontalAlign="Center" SortExpression="FileDate" DataField="FileDate" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="حجم الملف">
                            <ItemTemplate>
                                <span style="direction: rtl"><%# FileSize(Eval("FileName").ToString()) %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تحميل الملف" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <table style="margin: auto">
                                    <tr>
                                        <td>
                                            <asp:HyperLink runat="server" NavigateUrl='<%# "files/audio/" +Eval("FileName")  %>' ImageUrl="~/themes/Default/img/download22.png" ID="HyperLink1"></asp:HyperLink></td>
                                        <td>
                                            <asp:HyperLink runat="server" ImageUrl="~/themes/Default/img/Sound.png" NavigateUrl='<%# "SoundLibraryDetails.aspx?id=" +Eval("FileId")  %>' ID="lnkVideo"></asp:HyperLink></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <HeaderStyle BackColor="#6B696B" Font-Size="12px" Font-Names="ge_ss_twomedium" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="Grid-Paging" />
                    <RowStyle BackColor="#F7F7DE" Font-Size="11px" Font-Names="ge_ss_twomedium" />

                </asp:GridView>
                <asp:ObjectDataSource ID="odsFiles" runat="server" SelectMethod="LoadByDeleteState"
                    TypeName="DAL.LibraryFilesManage">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                        <asp:QueryStringParameter Name="LibraryItemId" QueryStringField="id" Type="String" />
                    
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblMsg" Style="font-family: ge_ss_twomedium; font-size: 12px" runat="server"></asp:Label></td>
        </tr>

    </table>
</asp:Content>
