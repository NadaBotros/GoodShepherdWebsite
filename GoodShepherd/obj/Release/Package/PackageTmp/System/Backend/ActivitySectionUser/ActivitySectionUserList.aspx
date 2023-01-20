<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ActivitySectionUserList.aspx.cs" EnableEventValidation="false" Inherits="System.Backend.ActivitySectionUserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text=" حضور فقرات الانشطة" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="قائمة حضور فقرات الانشطة" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                <asp:HyperLink runat="server" class="addnew" ID="btnAddNew" NavigateUrl="ActivitySectionUserManagement.aspx"
                    Text="اضافة جديد ...."></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:DropDownList ID="drpExport" CssClass="drplist" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpExport_SelectedIndexChanged">
                    <asp:ListItem Value="" Selected="True" Text="تحويل الى ملف"></asp:ListItem>
                    <asp:ListItem Value="pdf" Text="PDF"></asp:ListItem>
                    <asp:ListItem Value="word" Text="Word"></asp:ListItem>
                    <asp:ListItem Value="exel" Text="Exel"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList runat="server" Width="200px"  Style="float: left" CssClass="drplist" AutoPostBack="True" DataTextField="ActivityTitle" DataValueField="ActivityId" ID="drpActivity" DataSourceID="odsActivities" OnSelectedIndexChanged="drpActivity_SelectedIndexChanged"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsActivities" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.ActivitiesManage">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:DropDownList runat="server" Width="250px" ID="drpActivitySection" Style="float: left" CssClass="drplist" AutoPostBack="True" DataSourceID="odsSections" DataTextField="SectionTitle" DataValueField="ActivitySectionId" OnSelectedIndexChanged="drpActivitySection_SelectedIndexChanged" />
                <asp:ObjectDataSource ID="odsSections" runat="server" SelectMethod="LoadList" TypeName="DAL.ActivitySectionsManagement">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpActivity" Name="ActivityId" PropertyName="SelectedValue" Type="String" />
                        <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Label runat="server" ID="lblsearch" CssClass="lblSearch" Text="بحث : "></asp:Label>
                <asp:TextBox ID="txSearch" SkinID="txtSearch" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 10px; padding-bottom: 10px" class="tdbtns">
                <asp:Button Text="الغاء حضور الاسماء المختاره" runat="server" ID="btnDelete" OnClick="btnDelete_Click" />
                <asp:Label runat="server" Text="" ID="lblAttendNo" Style="float: right; padding-right: 10px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdData" Width="100%" DataSourceID="odsData" AllowPaging="True" runat="server" CssClass="grd"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkItem" />
                                <asp:Label runat="server" Text='<%#Eval("ActivitySectionUserId") %>' ID="lblId" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" onclick="CheckAll(this)" ID="chkAll" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Code" HeaderText="الكود" SortExpression="Code">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FullName" HeaderText="الاسم" SortExpression="FullName">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="موبايل" SortExpression="Mobile">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RoomNo" HeaderText="الغرفة" SortExpression="RoomNo">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="imgInfo" CssClass="imgIcon3 tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),"","") %>'
                                    ImageUrl="../lib/img/Info.png" runat="server" />
                            </ItemTemplate>

                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="Numeric" />
                </asp:GridView>

                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadByDeleteState"
                    TypeName="DAL.ActivitySectionUserManagement">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpActivitySection" Name="ActivitySectionId" PropertyName="SelectedValue" Type="String" />
                        <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
