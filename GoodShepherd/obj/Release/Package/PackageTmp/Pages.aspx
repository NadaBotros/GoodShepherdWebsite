<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="Pages.aspx.cs" Inherits="GoodShepherd.Pages" %>

<%@ Register Src="~/UserControls/ucPageContent.ascx" TagPrefix="uc1" TagName="ucPageContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <uc1:ucPageContent runat="server" ID="ucPageContent" />
</asp:Content>
