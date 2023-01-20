<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeBehind="UserWelcome.aspx.cs" Inherits="UserWelcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        بياناتي
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-top:15px">
                <asp:Label runat="server" CssClass="rowstyle" ID="lblWelcome"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
