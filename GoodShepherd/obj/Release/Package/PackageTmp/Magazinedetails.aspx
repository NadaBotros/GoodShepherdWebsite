<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="Magazinedetails.aspx.cs" Inherits="Magazinedetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
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
            <td style="padding: 10px; font-weight: bold">
                <asp:Label runat="server" ID="lblMagDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Image runat="server" ID="imgMagazine" />
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
                    <asp:HyperLink runat="server" ID="lnkDownlaod" ToolTip="تحميل المجلة">
<img src="themes/Default/img/download2.png" />
                    </asp:HyperLink>
                </div>
                <script type="text/javascript" src="//static.addtoany.com/menu/page.js"></script>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Literal runat="server" ID="frmMagazine"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
