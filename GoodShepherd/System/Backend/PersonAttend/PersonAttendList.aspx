<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="PersonAttendList.aspx.cs" EnableEventValidation="false" Inherits="System.Backend.PersonAttendList" %>

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
    <asp:Label runat="server" Text="تسجيل حضور الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="قائمة حضور اجتماع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                <asp:HyperLink runat="server" class="addnew" ID="btnAddNew" NavigateUrl="~/System/Backend/PersonAttend/PersonAttendManagement.aspx"
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
                <asp:DropDownList runat="server"  Style="float: left" CssClass="drplist" AutoPostBack="True" DataTextField="AttendDate" DataValueField="AttendDate" ID="drpDates" DataSourceID="odsDates" OnSelectedIndexChanged="drpDates_SelectedIndexChanged"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsDates" runat="server" SelectMethod="AttendDates" TypeName="DAL.PersonAttendManagement"></asp:ObjectDataSource>
                <asp:Label runat="server" ID="lblsearch" CssClass="lblSearch" Text="بحث : "></asp:Label>
                <asp:TextBox ID="txSearch" SkinID="txtSearch" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="padding-top:10px;padding-bottom:10px" class="tdbtns">
                <asp:Button Text="الغاء حضور الاسماء المختاره" runat="server" ID="btnDelete" OnClick="btnDelete_Click" />
                <asp:Label runat="server" Text="" ID="lblAttendNo" Style="float:right;padding-right:10px"></asp:Label>
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
                                <asp:Label runat="server" Text='<%#Eval("AttendId") %>' ID="lblId" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" onclick="CheckAll(this)" ID="chkAll" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PersonCode" HeaderText="الكود" SortExpression="PersonCode">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PersonName" HeaderText="الاسم" SortExpression="PersonName">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="موبايل" SortExpression="Mobile">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AreaName" HeaderText="المنطقة" SortExpression="AreaName">
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

                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="PersonsAttendDate"
                    TypeName="DAL.PersonAttendManagement">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpDates" Name="AttendDate" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
