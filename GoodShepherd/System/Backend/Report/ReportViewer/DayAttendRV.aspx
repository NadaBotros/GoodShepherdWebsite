<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/Report/ReportViewer/Report.Master" AutoEventWireup="true" CodeBehind="DayAttendRV.aspx.cs" Inherits="System.Backend.DayAttendRV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitle" runat="server">
    <asp:Label runat="server" ID="lblTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <asp:GridView ID="grdData" Width="100%" AllowSorting="True" SkinID="grdnotPag" runat="server" CssClass="grd"
                    AutoGenerateColumns="False" DataSourceID="odsServantPersons">
                    <Columns>
                        <asp:BoundField DataField="PersonCode" HeaderText="الكود" SortExpression="PersonCode"></asp:BoundField>
                        <asp:BoundField DataField="PersonName" HeaderText="الاسم" SortExpression="PersonName"></asp:BoundField>
                        <asp:BoundField DataField="HomePhone" HeaderText="التليفون" SortExpression="HomePhone"></asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="رقم الموبايل" SortExpression="Mobile"></asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="البريد الاليكتروني" SortExpression="Email"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <center>لا يوجد اسماء بالقائمة</center>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsServantPersons" runat="server" SelectMethod="PersonsAttendReport" TypeName="DAL.PersonAttendManagement">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="ServantId" QueryStringField="id" Type="String" />
                        <asp:QueryStringParameter Name="Date" QueryStringField="Date" Type="String" />
                        <asp:QueryStringParameter Name="AttendType" QueryStringField="Attend" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td align="center" style="padding-top:20px">
                <asp:Label runat="server" Style="font-size: 18px;font-weight:bold; padding-top: 15px;" ID="lblMsg"></asp:Label>
            </td>
        </tr>
    </table>

</asp:Content>
