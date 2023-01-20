<%@ Page Title="اجتماع الراعي الصالح | خدام الاجتماع" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="MeetingServants.aspx.cs" Inherits="MeetingServants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        <asp:Label runat="server" ID="lblTitle">خدام الاجتماع</asp:Label>
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataList Width="99%" ID="dtlstServants" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="0px" CellPadding="4" DataSourceID="odsServants" ForeColor="Black" GridLines="None">
                    <AlternatingItemStyle BackColor="White" />
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#F7F7DE" />
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td style="font-weight:bold"><%#Eval("PersonName") %></td>
                            </tr>
                            <tr>
                                <td style="font-style:italic;font-size:13px"><%#Eval("Services") %></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
                <asp:ObjectDataSource ID="odsServants" runat="server" SelectMethod="LoadAll" TypeName="DAL.ServantAftkadManagement"></asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
