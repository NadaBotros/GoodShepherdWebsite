<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="ChurchPopVisitingaspx.aspx.cs" Inherits="ChurchPopVisitingaspx" %>

<%@ Register Src="~/UserControls/ucPageContent.ascx" TagPrefix="uc1" TagName="ucPageContent" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHHead" ID="contHead">
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <uc1:ucPageContent runat="server" ID="ucPageContent" />
</asp:Content>
