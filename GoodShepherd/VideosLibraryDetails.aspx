<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="VideosLibraryDetails.aspx.cs" Inherits="VideosLibraryDetails" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60919.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <script src="themes/Default/audioplayer/mediaelement-and-player.min.js"></script>
    <link href="themes/Default/audioplayer/mediaelementplayer.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
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
                        <asp:Parameter Name="Id" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
<tr>
            <td>
                <table width="100%">
                    <tr>
                        <td valign="top">
                            <table style="float: right; font-family: ge_ss_twomedium; font-size: 13px;">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblOwner"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblDate"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblNotes"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:Image runat="server" Style="float: left; padding-left: 5px" ID="imgSpeaker" /></td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr>
            <td align="center" style="font-family: ge_ss_twomedium; font-size: 12px;">
                <asp:Literal runat="server" ID="lblYoutube" ></asp:Literal>
            </td>
        </tr>
   <tr>
            <td style="padding-top: 20px; padding-bottom: 20px">
                <div style="margin: auto" class="a2a_kit a2a_kit_size_32 a2a_default_style">
                    <a class="a2a_dd" href="http://www.addtoany.com/share_save"></a>
                    <a class="a2a_button_facebook"></a>
                    <a class="a2a_button_twitter"></a>
                    <a class="a2a_button_google_plus"></a>
                    <a class="a2a_button_email"></a>
                    <a class="a2a_button_linkedin"></a>
                   
                </div>
                <script type="text/javascript" src="//static.addtoany.com/menu/page.js"></script>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Literal runat="server" ID="lblFaceBookComment"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
