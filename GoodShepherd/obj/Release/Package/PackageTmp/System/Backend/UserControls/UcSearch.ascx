<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcSearch.ascx.cs" Inherits="System.Backend.UserControls.UcSearch" %>
<style>
    .dvPageContent {
        min-height: 300px;
    }
</style>
<table cellpadding="5">
    <tr>
        <td>الحضور</td>
        <td>
            <asp:DropDownCheckBoxes ID="drpStudious" runat="server" UseSelectAllNode="true">
                <asp:ListItem Selected="True" Value="1">يحضر بالكنيسة</asp:ListItem>
                <asp:ListItem Value="2">يحضر بالقاعة</asp:ListItem>
                <asp:ListItem Value="0">لا يحضر</asp:ListItem>
            </asp:DropDownCheckBoxes>
        </td>
        <td>القرابة</td>
        <td>
            <asp:DropDownCheckBoxes runat="server" UseSelectAllNode="true"
                ID="drpRelationship">
                <asp:ListItem Selected="True">الزوج</asp:ListItem>
                <asp:ListItem Selected="True">الزوجة</asp:ListItem>
                <asp:ListItem>ابن</asp:ListItem>
                <asp:ListItem>ابنة</asp:ListItem>
                <asp:ListItem>اخ</asp:ListItem>
                <asp:ListItem>اخت</asp:ListItem>
                <asp:ListItem>والد الزوج</asp:ListItem>
                <asp:ListItem>والدة الزوج</asp:ListItem>
                <asp:ListItem>والد الزوجة</asp:ListItem>
                <asp:ListItem>والدة الزوجة</asp:ListItem>
            </asp:DropDownCheckBoxes>
        </td>
        <td>الحالة الاجتماعية
        </td>
        <td>
            <asp:DropDownCheckBoxes UseSelectAllNode="true" runat="server" ID="drpMaritalStatus">
                <asp:ListItem Selected="True">اعزب</asp:ListItem>
                <asp:ListItem Selected="True">متزوج</asp:ListItem>
                <asp:ListItem Selected="True">ارمل</asp:ListItem>
                <asp:ListItem Selected="True">مطلق</asp:ListItem>
                <asp:ListItem>متوفي</asp:ListItem>
            </asp:DropDownCheckBoxes>
        </td>
    </tr>
    <tr>
        <td>الوظيفة 
        </td>
        <td>
            <asp:DropDownCheckBoxes runat="server" OnDataBound="drpJob_DataBound" ID="drpJob" DataSourceID="odsJob" DataTextField="Job" DataValueField="Job"></asp:DropDownCheckBoxes>
            <asp:ObjectDataSource ID="odsJob" runat="server" SelectMethod="LoadAllJobs" TypeName="DAL.PersonManagement"></asp:ObjectDataSource>
        </td>
        <td>فصيله الدم 
        </td>
        <td>
            <asp:DropDownCheckBoxes runat="server" ID="drpBooldType">
                <asp:ListItem Selected="True" Value="">غير معروفة</asp:ListItem>
                <asp:ListItem Selected="True">O-</asp:ListItem>
                <asp:ListItem Selected="True">O+</asp:ListItem>
                <asp:ListItem Selected="True">B-</asp:ListItem>
                <asp:ListItem Selected="True">B+</asp:ListItem>
                <asp:ListItem Selected="True">A-</asp:ListItem>
                <asp:ListItem Selected="True">A+</asp:ListItem>
                <asp:ListItem Selected="True">AB-</asp:ListItem>
                <asp:ListItem Selected="True">AB+</asp:ListItem>
                <asp:ListItem Selected="True">AB+</asp:ListItem>
            </asp:DropDownCheckBoxes>
        </td>
        <td>اب الاعتراف 
        </td>
        <td>
            <asp:DropDownCheckBoxes runat="server" OnDataBound="drpAbA3traf_DataBound" ID="drpAbA3traf" DataSourceID="odsAbA3traf" DataTextField="FatherName" DataValueField="FatherName"></asp:DropDownCheckBoxes>
            <asp:ObjectDataSource ID="odsAbA3traf" runat="server" SelectMethod="LoadAllAbaKhna" TypeName="DAL.PersonManagement"></asp:ObjectDataSource>
        </td>
    </tr>
    <tr>

        <td>العنوان - المدينة</td>
        <td>
            <asp:DropDownCheckBoxes runat="server" ID="drpCity" DataTextField="CityName" DataValueField="CityId" UseButtons="true" UseSelectAllNode="true" DataSourceID="odsCity" OnSelectedIndexChanged="drpCity_SelectedIndexChanged" OnDataBound="drpCity_DataBound"></asp:DropDownCheckBoxes>
            <asp:ObjectDataSource ID="odsCity" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.CityManagement">
                <SelectParameters>
                    <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
        <td>العنوان - المنطقة </td>
        <td>
            <asp:DropDownCheckBoxes runat="server" ID="drpArea" DataTextField="AreaName" DataValueField="AreaId"></asp:DropDownCheckBoxes>

        </td>
        <td>كلمة البحث</td>
        <td>
            <asp:TextBox SkinID="as" Width="235px" runat="server" ID="txtSearch"></asp:TextBox></td>
    </tr>

</table>
