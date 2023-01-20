<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcPersonGrid.ascx.cs" Inherits="System.Backend.UserControls.UcPersonGrid" %>
<script type="text/javascript">
    //<![CDATA[
    function CheckAll(oCheckbox) {
        var grdData = document.getElementById("<%=grdData.ClientID %>");
        for (i = 1; i < grdData.rows.length; i++) {
            grdData.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
        }
    }

    //]]>
</script>
<asp:GridView ID="grdData" Width="100%" runat="server" CssClass="grd" AllowPaging="true" OnPageIndexChanging="GridView_PageIndexChanging"
    AutoGenerateColumns="False">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="chkItem" />
                <asp:Label runat="server" Text='<%#Eval("PersonId") %>' ID="lblId" Visible="false"></asp:Label>

            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <asp:CheckBox runat="server" onclick="CheckAll(this)" ID="chkAll" />
            </HeaderTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="PersonCode" HeaderText="الكود" SortExpression="PersonCode"></asp:BoundField>
        <asp:BoundField DataField="PersonName" HeaderText="الاسم" SortExpression="PersonName"></asp:BoundField>
        <asp:BoundField DataField="Mobile" HeaderText="رقم الموبايل" SortExpression="Mobile"></asp:BoundField>
        <asp:BoundField DataField="Studious" HeaderText="الحضور" SortExpression="Studious"></asp:BoundField>
        <asp:BoundField DataField="Relationship" HeaderText="القرابة" SortExpression="Relationship"></asp:BoundField>
        <asp:BoundField DataField="AreaName" HeaderText="المنطقة" SortExpression="AreaName"></asp:BoundField>
    </Columns>
                        <PagerSettings Mode="Numeric" />

    <EmptyDataTemplate>
        <center>لا يوجد اسماء بالقائمة</center>
    </EmptyDataTemplate>
</asp:GridView>
