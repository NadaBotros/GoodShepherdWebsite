﻿<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ActivitySectionsList.aspx.cs" EnableEventValidation="false" Inherits="System.Backend.ActivitySectionsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="فقرات الانشطة" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="فقرات الانشطة" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                <asp:HyperLink runat="server" class="addnew" ID="btnAddNew" NavigateUrl="ActivitySectionsManagement.aspx"
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
                <asp:DropDownList ID="drpViews" CssClass="drplist drpRecords" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="True" Selected="True" Text="نوع العرض"></asp:ListItem>
                    <asp:ListItem Value="True" Text="البيانات الحالية"></asp:ListItem>
                    <asp:ListItem Value="False" Text="البيانات المحذوفة"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="drpActivity" CssClass="drplist drpRecords" Width="200px" runat="server" AutoPostBack="True" DataSourceID="odsActivities"
                    DataTextField="ActivityTitle" DataValueField="ActivityId" OnSelectedIndexChanged="drpActivity_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsActivities" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.ActivitiesManage">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Label runat="server" ID="lblsearch" CssClass="lblSearch" Text="بحث : "></asp:Label>
                <asp:TextBox ID="txSearch" SkinID="txtSearch" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdData" Width="100%" runat="server" CssClass="grd" DataSourceID="odsData"
                    AutoGenerateColumns="False" OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">
                    <Columns>

                        <asp:BoundField DataField="SectionTitle" HeaderText="الفقرة" SortExpression="SectionTitle">
                            <HeaderStyle Width="50%" />
                            <ItemStyle Width="50%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="تاريخ الفقره">
                            <ItemTemplate>
                                <%#DAL.GeneralMethods.ConvertToDateString(Eval("SectionDate").ToString()) %>
                            </ItemTemplate>
                             <HeaderStyle Width="30%" />
                            <ItemStyle Width="30%" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="imgEdit" runat="server" CssClass="imgIcon3 tip_right_top" NavigateUrl='<%#"ActivitySectionsManagement.aspx?id="+Eval("ActivitySectionId")%>'
                                    ImageUrl="~/System/Backend/lib/img/Edit.png" title="Edit"></asp:HyperLink>
                                <asp:ImageButton ID="imgRestore" runat="server" CssClass="imgIcon3 tip_right_top"
                                    class="tip_right_top" ImageUrl="../lib/img/Restor.png" CommandName="restoreitem"
                                    CommandArgument='<%# Eval("ActivitySectionId") %>' Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"true") %>'
                                    ToolTip="Restore Item"></asp:ImageButton>
                                <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/Delete.png"
                                    CommandName="deleteitem" CommandArgument='<%# Eval("ActivitySectionId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                    Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"false") %>'
                                    title="Delete Item"></asp:ImageButton>
                                <asp:Image ID="imgInfo" CssClass="imgIcon3 tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),Eval("ModifiedOn"),Eval("ModifiedBy")) %>'
                                    ImageUrl="../lib/img/Info.png" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="Numeric" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadByDeleteState"
                    TypeName="DAL.ActivitySectionsManagement">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpViews" Name="Active" PropertyName="SelectedValue"
                            Type="string" DefaultValue="true" />
                        <asp:ControlParameter ControlID="drpActivity" Name="ActivityId" PropertyName="SelectedValue"
                            Type="string" DefaultValue="true" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>