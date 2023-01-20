<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="ImportFromYoutubeChannel.aspx.cs" Inherits="System.Backend.ImportFromYoutubeChannel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="استيراد الفيديوهات من قناه يوتيوب" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/img/key.png" />
                <asp:Label runat="server" Text="استيراد الفيديوهات من قناه يوتيوب" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="msg" runat="server">
                <asp:Label ID="lblMessge" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" class="tblMain" cellspacing="2">
                    <tr>
                        <td>اي دي القسم<span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCatId" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
                                ControlToValidate="txtCatId"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>يوتيوب كود<span class="reqstar">*</span>
                        </td>
                        <td a>
                            <cc1:Editor ID="EditorCode" Width="500px" Height="500px" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" Text="حفظ البيانات" runat="server" ValidationGroup="1"
                    OnClick="btnSave_OnClick" />
            </td>
        </tr>
    </table>
</asp:Content>
