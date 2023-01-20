<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="ActivityAttend.aspx.cs" Inherits="System.Backend.ActivityAttend" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="التقارير" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td class="tdHeader">
                        <img class="imgIcon2" src="../../lib/icons/24/Dashboard.png" />
                        <asp:Label runat="server" Text="كشف حضور فقرات الانشطة" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="7">
                            <tr>
                                <td>اختر النشاط</td>
                                <td>
                                    <asp:DropDownList runat="server" SkinID="drpwList" Width="250px" DataTextField="ActivityTitle" DataValueField="ActivityId" ID="drpActivity" DataSourceID="odsActivities"></asp:DropDownList>
                                    <asp:ObjectDataSource ID="odsActivities" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.ActivitiesManage">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>اختر الفقرات</td>
                                <td>
                                    <asp:DropDownCheckBoxes runat="server" Width="250px" SkinID="drpwList" DataTextField="SectionTitle"
                                        DataValueField="ActivitySectionId" ID="drpSections" DataSourceID="odsSections">
                                    </asp:DropDownCheckBoxes>
                                    <asp:ObjectDataSource ID="odsSections" runat="server" SelectMethod="LoadList" TypeName="DAL.ActivitySectionsManagement">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="drpActivity" Name="ActivityId" PropertyName="SelectedValue" Type="String" />
                                            <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                                <td></td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdbtns" style="padding-bottom: 250px">
                        <asp:Button ID="btnReport" Text="عرض التقرير" runat="server" OnClick="btnReport_Click" /></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
