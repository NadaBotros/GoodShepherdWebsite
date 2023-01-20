<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ActivitySectionUserManagement.aspx.cs" Inherits="System.Backend.manage.ActivitySectionUserManagement" %>

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
    <asp:Label runat="server" Text=" حضور فقرات الانشطة" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="تسجيل حضور فقرات الانشطة" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
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
                        <td>اختر النشاط<span class="reqstar"> *</span>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" SkinID="drpwlist" CssClass="drpwlist" AutoPostBack="True" DataTextField="ActivityTitle" DataValueField="ActivityId" ID="drpActivity"
                                DataSourceID="odsActivities">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="odsActivities" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.ActivitiesManage">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:RequiredFieldValidator ControlToValidate="drpActivity" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>اختر الفقرة<span class="reqstar"> *</span>
                        </td>
                        <td>
                            <asp:DropDownList SkinID="drpwlist" runat="server" ID="drpActivitySection" Style="float: left" CssClass="drplist" AutoPostBack="True"
                                 DataSourceID="odsSections" DataTextField="SectionTitle" DataValueField="ActivitySectionId" />
                            <asp:ObjectDataSource ID="odsSections" runat="server" SelectMethod="LoadList" TypeName="DAL.ActivitySectionsManagement">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="drpActivity" Name="ActivityId" PropertyName="SelectedValue" Type="String" />
                                    <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:RequiredFieldValidator ControlToValidate="drpActivitySection" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                     <tr>
                        <td>ملاحظات
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNotes" SkinID="txtmult" TextMode="MultiLine"></asp:TextBox>
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
                        <td>بحث بالاسم / الموبايل / الغرفة<span class="reqstar"> *</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSearch"></asp:TextBox>
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
                                <asp:Label runat="server" Text='<%#Eval("ActivityUserId") %>' ID="lblId" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" onclick="CheckAll(this)" ID="chkAll" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Code" HeaderText="الكود" SortExpression="Code">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FullName" HeaderText="الاسم" SortExpression="FullName">
                            <HeaderStyle Width="33%" />
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="موبايل" SortExpression="Mobile">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RoomNo" HeaderText="الغرفة" SortExpression="RoomNo">
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
