﻿<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/System/Backend/Report/ReportViewer/Report.Master" CodeBehind="PeopleWithNoServantsRV.aspx.cs" Inherits="System.Backend.PeopleWithNoServantsRV" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitle" runat="server">
    كشف بيانات المخدومين الغير مربوطين بخدام
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <asp:Label runat="server" Text="" ID="lblAttendAllNo" Style="float:right;padding-right:10px"></asp:Label>


    <asp:GridView runat="server" SkinID="grdnotPag" ID="grdData" AllowPaging="false" >
        <Columns>
           
        </Columns>
    </asp:GridView>
</asp:Content>


