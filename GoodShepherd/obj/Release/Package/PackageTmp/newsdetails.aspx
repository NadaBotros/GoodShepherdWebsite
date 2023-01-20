<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="newsdetails.aspx.cs" Inherits="newsdetails" %>

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
            <td align="center">
                <asp:Image runat="server" Width="60%" ID="imgNews" />
            </td>
        </tr>
        <tr>
            <td style="font-weight:bold">
                <asp:Label runat="server" ID="lblDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="SiteText" style="padding-top: 10px;">
                <asp:Label runat="server" CssClass="news" ID="lblNewsContent"></asp:Label>
            </td>
        </tr>
       
    </table>
</asp:Content>
