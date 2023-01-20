<%@ Page Title="اجتماع الراعي الصالح | مجلة الاجتماع" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="MeetingMagazine.aspx.cs" Inherits="MeetingMagazine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        مجلة الاجتماع
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="odsNews" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.MagazineManage">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ListView ID="lst" DataSourceID="odsNews" GroupItemCount="4" runat="server">
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
                                        <asp:HyperLink runat="server" CssClass="lnkImage" NavigateUrl='<%#"Magazinedetails.aspx?id="+Eval("MagazineId") %>' ID="lnkImage" ImageUrl='<%#"images/S140_200/"+Eval("MagazineCover") %>'></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href='<%#"Magazinedetails.aspx?id="+Eval("MagazineId") %>'><%#Eval("MagazineTitle")+"<br/>"+Eval("MagazineDate") %></a></td>
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
