<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="PageSectionList.aspx.cs" Inherits="System.Backend.Pages.PageSectionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="صفحات الموقع" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="محتوي صفحات الموقع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                <asp:HyperLink runat="server" class="addnew" ID="btnAddNew" NavigateUrl="~/System/Backend/Pages/PageSectionManage.aspx"
                    Text="اضافة فقرة جديدة ..."></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:DropDownList ID="drpPages" CssClass="drplist" runat="server" AutoPostBack="True"
                    DataSourceID="odsPages" DataTextField="PageName" DataValueField="PageId">
                </asp:DropDownList>
                <asp:DropDownList ID="drpViews" CssClass="drplist drpRecords" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="True" Selected="True" Text="نوع العرض"></asp:ListItem>
                    <asp:ListItem Value="True" Text="البيانات الحالية"></asp:ListItem>
                    <asp:ListItem Value="False" Text="البيانات المحذوفة"></asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsSection" runat="server" SelectMethod="LoadByDeleteState"
                    TypeName="DAL.PageSectionManagement">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpViews" DefaultValue="true" Name="Active" PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="drpPages" DefaultValue="" Name="PageId" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="odsPages" runat="server" SelectMethod="LoadPages" TypeName="DAL.PageSectionManagement"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <table width="96%" style="margin: auto" dir="rtl">
                    <asp:Repeater ID="rptSections" DataSourceID="odsSection" runat="server"
                        OnItemCommand="rptSections_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td class="tdHeader2" runat="server" visible='<%#!String.IsNullOrEmpty(Eval("SectionName").ToString()) %>'>
                                    <asp:Label runat="server" ID="lblTitle" Text='<%#Eval("SectionName") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible='<%#!String.IsNullOrEmpty(Eval("ImageFile").ToString())&&String.IsNullOrEmpty(Eval("SectionContent").ToString()) %>'>
                                <td align="center">
                                    <asp:Image runat="server" Visible='<%#!String.IsNullOrEmpty(Eval("ImageFile").ToString()) %>'
                                        ID="imgSection" ImageUrl='<%# "~/images/S500_500/"+Eval("ImageFile") %>' />
                                </td>
                            </tr>
                            <tr runat="server" visible='<%#!String.IsNullOrEmpty(Eval("SectionContent").ToString()) %>'>
                                <td>
                                    <table cellpadding="3" width="100%">
                                        <tr>
                                            <td valign="top">
                                                <asp:Label runat="server" ID="lblDesc" Text='<%#Eval("SectionContent") %>'></asp:Label>
                                            </td>
                                            <td runat="server" visible='<%#!String.IsNullOrEmpty(Eval("ImageFile").ToString()) %>' valign="top">
                                                <asp:Image runat="server" style="float:left" ID="imgSmall" ImageUrl='<%# "~/images/S150_150/"+Eval("ImageFile") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" align="center" visible='<%#!String.IsNullOrEmpty(Eval("VideoUrl").ToString()) %>'>
                                <td>
                                    <asp:Literal runat="server" ID="ltrYoutube" Text='<%# DAL.GeneralMethods.GetYoutube(Eval("VideoUrl")) %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <table style="float: left">
                                        <tr>
                                            <td>
                                                <div id="dvUp" style='width: 16px; height: 16px; float: left; display: <%# ShowArrow(Eval("RecOrder"), "U") %>'>
                                                    <asp:ImageButton ID="ImgArrowUp" CssClass="btnArrUp" runat="server" CommandName="ArrowUp"
                                                        CommandArgument='<%# Eval("PageSectionId") %>' ImageUrl="~/App_Themes/Default/Images/Up.png"></asp:ImageButton>
                                                </div>
                                                <div id="dvDown" style='width: 16px; height: 16px; float: left; display: <%# ShowArrow(Eval("RecOrder"), "D") %>;'>
                                                    <asp:ImageButton ID="ImgArrowDown" runat="server" CssClass="btnArrDown" CommandName="ArrowDown"
                                                        CommandArgument='<%# Eval("PageSectionId") %>' ImageUrl="~/App_Themes/Default/Images/Down.png"></asp:ImageButton>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Image ID="imgInfo" CssClass="imgIcon3 tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),Eval("ModifiedOn"),Eval("ModifiedBy")) %>'
                                                    ImageUrl="../lib/img/Info.png" runat="server" />
                                                <asp:ImageButton ID="imgRestore" runat="server" CssClass="imgIcon3 tip_right_top"
                                                    class="tip_right_top" ImageUrl="../lib/img/Restor.png" CommandName="restoreitem"
                                                    CommandArgument='<%# Eval("PageSectionId") %>' Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"true") %>'
                                                    ToolTip="Restore Item"></asp:ImageButton>
                                                <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/Delete.png"
                                                    CommandName="deleteitem" CommandArgument='<%# Eval("PageSectionId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                                    Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"false") %>'
                                                    title="Delete Item"></asp:ImageButton>
                                                <asp:HyperLink ID="imgEdit" runat="server" CssClass="imgIcon3 tip_right_top" NavigateUrl='<%#"PageSectionManage.aspx?id="+Eval("PageSectionId")%>'
                                                    ImageUrl="~/System/Backend/lib/img/Edit.png" title="Edit"></asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <center>
                                <hr style="padding: 0; margin: 0; border: 0px; background-color: Gray; width: 50%" />
                            </center>
                        </SeparatorTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
