<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="PersonAttendManagement.aspx.cs" Inherits="System.Backend.manage.PersonAttendManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="حضور الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="تسجيل حضور الاجتماع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="msg" runat="server">
                <asp:Label ID="lblMessge" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" cellspacing="2">
                    <tr>
                        <td>تاريخ حضور الاجتماع<span class="reqstar"> *</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDate" ></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" Format="d/M/yyyy" TargetControlID="txtDate" runat="server"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ControlToValidate="txtCodes" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>بحث باكواد الحاضرين<span class="reqstar"> *</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCodes" TextMode="MultiLine" SkinID="txtmult"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtCodes" ID="RequiredFieldValidator1" ValidationGroup="2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnCodesSearch" Text="بحث" ValidationGroup="2" OnClick="btnCodesSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>بحث عام<span class="reqstar"> *</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSearch" ></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtSearch" ID="RequiredFieldValidator4" ValidationGroup="1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnGeneralSearch" Text="بحث" ValidationGroup="1" OnClick="btnGeneralSearch_Click" />
                        </td>
                    </tr>


                </table>
            </td>
        </tr>
        <tr>
            <td class="tdbtns">
                <asp:Button runat="server" ID="btnSave" Text="تسجيل حضور الاشخاص المختارة" ValidationGroup="11" OnClick="btnSave_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdData" Width="100%" AllowPaging="true" runat="server" CssClass="grd"
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
                        <asp:BoundField DataField="PersonCode" HeaderText="الكود" SortExpression="PersonCode">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PersonName" HeaderText="الاسم" SortExpression="PersonName">
                            <HeaderStyle Width="33%" />
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="موبايل" SortExpression="Mobile">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AreaName" HeaderText="المنطقة" SortExpression="AreaName">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>

                    </Columns>
                    <PagerSettings Mode="Numeric" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
