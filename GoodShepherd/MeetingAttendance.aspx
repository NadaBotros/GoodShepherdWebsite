<%@ Page Title=" حضور الاجتماعات" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeBehind="MeetingAttendance.aspx.cs" Inherits="MeetingAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        حضور الاجتماعات
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 15px">
                <asp:GridView SkinID="grd320" AllowPaging="True" PageSize="12" Style="border-collapse: separate; width: 100%" runat="server"
                    ID="grd" AutoGenerateColumns="False" DataSourceID="odsData" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    CellPadding="6" PagerSettings-Mode="Numeric" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="تاريخ الاجتماع">
                            <ItemTemplate>
                                <%# DateTime.Parse(Eval("AttendDate").ToString()).ToString("yyyy/MM/dd") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="الحضور" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# Eval("attendid")==null?"غـ":"√" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <HeaderStyle BackColor="#6B696B" Font-Size="12px" Font-Names="ge_ss_twomedium" HorizontalAlign="Right" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="Grid-Paging" />
                    <RowStyle BackColor="#F7F7DE" Font-Size="11px"  />

                </asp:GridView>
                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="PersonAttend"
                    TypeName="DAL.PersonAttendManagement">
                    <SelectParameters>
                        <asp:CookieParameter CookieName="PersonId" Name="PersonId" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
