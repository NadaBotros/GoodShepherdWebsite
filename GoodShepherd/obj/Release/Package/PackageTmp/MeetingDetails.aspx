<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="MeetingDetails.aspx.cs" Inherits="MeetingDetails" %>

<asp:Content ContentPlaceHolderID="CPHHead" ID="as" runat="server">
    <script src="themes/Default/audioplayer/mediaelement-and-player.min.js"></script>
    <link href="themes/Default/audioplayer/mediaelementplayer.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        <asp:HyperLink runat="server" Style="color: white" ID="lnkSPeaker"></asp:HyperLink>
                        <asp:Label runat="server" ID="lblTitle"></asp:Label>
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
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
                                        <asp:Label runat="server" ID="lblSpeaker"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblChurch"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblDate"></asp:Label></td>
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
                <asp:Literal runat="server" ID="lblMediaPlayer" ></asp:Literal><script>
                                                                                  $('audio,video').mediaelementplayer();
    </script>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Literal runat="server" ID="lblYoutube"></asp:Literal>
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
                    <asp:HyperLink runat="server" ID="lnkDownlaod" ToolTip="تحميل ملف العظه">
<img src="themes/Default/img/download2.png" />
                    </asp:HyperLink>
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
