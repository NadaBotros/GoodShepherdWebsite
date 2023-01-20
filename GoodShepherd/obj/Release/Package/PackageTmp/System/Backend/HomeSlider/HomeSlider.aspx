<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="HomeSlider.aspx.cs" Inherits="System.Backend.HomeAlbumList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Window.png" class="imgIcon" /><asp:Label runat="server"
        Text="Website Backend" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table class="tblwithoutborder" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="Slider Images" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:DropDownList ID="drpViews" CssClass="drplist drpRecords" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="True" Selected="True" Text="Select View"></asp:ListItem>
                    <asp:ListItem Value="True" Text="Active"></asp:ListItem>
                    <asp:ListItem Value="False" Text="Deleted"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="lstview" runat="server" GroupItemCount="4" DataSourceID="objImages"
                    OnItemCommand="lstview_ItemCommand">
                    <LayoutTemplate>
                        <table>
                            <asp:PlaceHolder ID="GroupPlaceHolder" runat="server"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <td>
                            <div style="padding-right: 30px;">
                                <asp:Image ID="img" ImageUrl='<%#"~/Images/S150_150/"+ Eval("ImageFile") %>' runat="server" />
                            </div>
                            <div>
                                <asp:Image ID="imgInfo" CssClass="imgIcon tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),Eval("ModifiedOn"),Eval("ModifiedBy")) %>'
                                    ImageUrl="~/System/Backend/lib/img/Info.png" runat="server" />
                                <asp:ImageButton ID="imgRestore" runat="server" CssClass="imgIcon tip_right_top"
                                    class="tip_right_top" ImageUrl="~/System/Backend/lib/img/Restor.png" CommandName="restoreitem"
                                    CommandArgument='<%# Eval("ImageId") %>' Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"true") %>'
                                    ToolTip="Restore Item"></asp:ImageButton>
                                <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon tip_right_top" ImageUrl="~/System/Backend/lib/img/Delete.png"
                                    CommandName="deleteitem" CommandArgument='<%# Eval("ImageId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                    Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"false") %>'
                                    title="Delete Item"></asp:ImageButton>
                                <asp:ImageButton ID="imgEdit" runat="server" CssClass="imgIcon tip_right_top" ImageUrl="~/System/Backend/lib/img/Edit.png"
                                    CommandName="edititem" CommandArgument='<%# Eval("ImageId") %>' title="Edit">
                                </asp:ImageButton>
                            </div>
                        </td>
                    </ItemTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="objImages" runat="server" SelectMethod="LoadByDeleteState"
                    TypeName="DAL.HomeGalleryManagement">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpViews" Name="Active" PropertyName="SelectedValue"
                            Type="string" DefaultValue="true" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 15px;">
                <table>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td rowspan="4">
                            <asp:Image ID="img1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Image File
                        </td>
                        <td>
                            <asp:FileUpload ID="fupldImage" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblImgCaption" runat="server" Text="Image Description"></asp:Label>
                        </td>
                        <td>
                            <cc1:Editor Width="400px" ID="EdDesc" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMessge" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Save" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
