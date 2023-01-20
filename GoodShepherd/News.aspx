<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        الاخبار
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="odsNews" runat="server" SelectMethod="LoadAll" TypeName="DAL.NewsManagement"></asp:ObjectDataSource>
                <asp:ListView ID="lst" DataSourceID="odsNews" GroupItemCount="4" runat="server">
                    <LayoutTemplate>
                        <table runat="server" cellspacing="3" style="margin: auto" cellpadding="0">
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
                                        <asp:HyperLink runat="server" CssClass="lnkImage" NavigateUrl='<%#"newsdetails.aspx?id="+Eval("NewsId") %>' ID="lnkImage" ImageUrl='<%#"images/S140_160/"+Eval("NewsImage") %>'></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href='<%#"newsdetails.aspx?id="+Eval("NewsId") %>'><%#Eval("NewsTitle") %></a></td>
                                </tr>
                            </table>
                        </td>
                    </ItemTemplate>
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:DataPager PageSize="12" runat="server"  ID="lstPager"
                    PagedControlID="lst">
                    <Fields>
                        <asp:NumericPagerField ButtonCount="10" NumericButtonCssClass="numeric_button" CurrentPageLabelCssClass="current_page" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>
</asp:Content>
