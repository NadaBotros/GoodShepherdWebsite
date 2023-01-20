<%@ Page Title="اجتماع الراعي الصالح | البوم الذكريات" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="WebsiteAlbums.aspx.cs" Inherits="GoodShepherd.WebsiteAlbums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        البوم الذكريات
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="LoadWebsiteAlbums" TypeName="DAL.AlbumManagement">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ListView ID="lst" DataSourceID="ods" GroupItemCount="4" runat="server">
                    <LayoutTemplate>
                        <table id="Table1" runat="server" cellspacing="3"  cellpadding="0">
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
                                        <asp:HyperLink runat="server" CssClass="lnkImage" NavigateUrl='<%#"WebsiteAlbumImages.aspx?id="+Eval("AlbumId") %>' ID="lnkImage" ImageUrl='<%#"images/S140_110/"+Eval("AlbumCover") %>'></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href='<%#"WebsiteAlbumImages.aspx?id="+Eval("AlbumId") %>'><%#Eval("AlbumName")+"<br/>"+Eval("date") %></a></td>
                                </tr>
                            </table>
                        </td>
                    </ItemTemplate>
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:DataPager PageSize="12" runat="server" ID="lstPager"
                    PagedControlID="lst">
                    <Fields>
                        <asp:NumericPagerField ButtonCount="10" NumericButtonCssClass="numeric_button" CurrentPageLabelCssClass="current_page" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>
</asp:Content>
