<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="AudioStream.aspx.cs" Inherits="AudioStream" %>

<%@ Register Src="~/UserControls/ucPageContent.ascx" TagPrefix="uc1" TagName="ucPageContent" %>
<asp:Content ContentPlaceHolderID="CPHHead" ID="as" runat="server">
    <script src="themes/Default/audioplayer/mediaelement-and-player.min.js"></script>
    <link href="themes/Default/audioplayer/mediaelementplayer.css" rel="stylesheet" />
    <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">

    <table width="100%">
        <tr>
            <td>
                <uc1:ucPageContent runat="server" ID="ucPageContent" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <audio id="player2" src="http://54.213.170.187:8000/smsaradioandroid" type="audio/mp3" controls="controls">
                </audio>

                <script>
                    $('audio,video').mediaelementplayer();
                </script>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="5">
                    <tr>
                        <td>
                            <asp:Label runat="server" Style="font-weight: bold" ID="lblStreamAudio"></asp:Label></td>
                        <td align="center">
                            <asp:HyperLink Style="display: block; width: 60px" ID="lnkAudioStream" runat="server" NavigateUrl="~/AudioStream.aspx"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 20px; padding-bottom: 20px">
                <div style="margin: auto;" class="a2a_kit a2a_kit_size_32 a2a_default_style">
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
