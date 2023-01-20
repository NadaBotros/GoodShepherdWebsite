<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/Report/ReportViewer/Report.Master" AutoEventWireup="true" CodeBehind="ActivityAttendRV.aspx.cs" Inherits="System.Backend.ActivityAttendRV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitle" runat="server">
    <asp:Label runat="server" ID="lblTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
          <tr>
            <td  style="padding-top: 20px">
                <asp:Label runat="server" Style="font-size: 18px; font-weight: bold; padding-top: 15px;" ID="lblMsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdData" Width="100%" AllowPaging="True" AllowSorting="True" SkinID="grdnotPag" runat="server" CssClass="grd"
                    AutoGenerateColumns="False" DataSourceID="odsData" >
                    <Columns>
                        <asp:BoundField DataField="Code" HeaderText="الكود" SortExpression="Code"></asp:BoundField>
                        <asp:BoundField DataField="FullName" HeaderText="الاسم" SortExpression="FullName"></asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="رقم الموبايل" SortExpression="Mobile"></asp:BoundField>
                        <asp:BoundField DataField="RoomNo" HeaderText="الغرفة" SortExpression="RoomNo"></asp:BoundField>
                        <asp:BoundField DataField="ActivitiesCount" HeaderText="عدد حضور الفقرات" SortExpression="ActivitiesCount"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <center>لا يوجد اسماء بالقائمة</center>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="ActivitiesAttendReportDT" TypeName="DAL.ActivitySectionUserManagement">
                    <SelectParameters>
                        <asp:SessionParameter Name="SectionsIds" SessionField="SectionsIds" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
      
    </table>

</asp:Content>
