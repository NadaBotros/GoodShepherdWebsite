<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="viewmessage.aspx.cs" Inherits="viewmessage" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHHead" ID="contHead">
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
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
        </tr></table>
</asp:Content>
