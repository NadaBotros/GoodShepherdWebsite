<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="ChurchMeetingsSchedule.aspx.cs" Inherits="ChurchMeetingsSchedule" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHHead" ID="contHead">
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<%@ Register Src="~/UserControls/ucPageContent.ascx" TagPrefix="uc1" TagName="ucPageContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <uc1:ucPageContent runat="server" ID="ucPageContent" />
</asp:Content>
