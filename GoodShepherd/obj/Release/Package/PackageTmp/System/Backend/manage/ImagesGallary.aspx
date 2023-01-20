<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImagesGallary.aspx.cs"
    Inherits="system.backend.ImagesGallary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../lib/GallaryLightBox/example1/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="../lib/GallaryLightBox/jquery.colorbox.js" type="text/javascript"></script>
    <script src="../lib/tooltip/jquery.tipTip.js" type="text/javascript"></script>
    <script src="../lib/tooltip/jquery.tipTip.minified.js" type="text/javascript"></script>
    <link href="../lib/tooltip/tipTip.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function ajaxFileUpload1_ClientUploadComplete(sender, e) {
            if (sender._filesInQueue[sender._filesInQueue.length - 1]._isUploaded) {
                //__doPostBack('updatePanelAttachments', ''); // Do post back only after all files have been uploaded
                location.reload();
            }

        }
        $(function () {
            $(".tip_right_top").tipTip({ maxWidth: "auto", edgeOffset: 2 });
        });
        function load() { Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); }
        function EndRequestHandler() {
            $(".tip_right_top").tipTip({ maxWidth: "auto", edgeOffset: 2 });
        }
        $(document).ready(function () {
            $(".gallery").colorbox({ rel: 'Image', slideshow: true });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <asp:UpdatePanel runat="server" ID="updatePanelAttachments" UpdateMode="Conditional"
                OnPreRender="updatePanelAttachments_PreRender">
                <ContentTemplate>
                    <table width="98%" class="tblMain" cellpadding="2" cellspacing="2">
                        <tr>
                            <td>
                                <asp:DropDownList ID="drpViews" CssClass="drplist drpRecords" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="True" Selected="True" Text="Select View"></asp:ListItem>
                                    <asp:ListItem Value="True" Text="Active"></asp:ListItem>
                                    <asp:ListItem Value="False" Text="Deleted"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListView ID="lstview" runat="server" GroupItemCount="5" DataSourceID="objImages"
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
                                            <div class="imgIcon">
                                                <a class="gallery" href='<%#"../../../Images/S300_300/"+ Eval("ImageFile") %>'>
                                                    <asp:Image ID="img" ImageUrl='<%#"~/Images/S150_150/"+ Eval("ImageFile") %>'
                                                        runat="server" /></a>
                                            </div>
                                            <div>
                                                <asp:Image ID="Image1" CssClass="imgIcon tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),Eval("ModifiedOn"),Eval("ModifiedBy")) %>'
                                                    ImageUrl="../lib/img/Info.png" runat="server" />
                                                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="imgIcon tip_right_top"
                                                    class="tip_right_top" ImageUrl="~/System/Backend/lib/img/Restor.png" CommandName="restoreitem"
                                                    CommandArgument='<%# Eval("ImageId") %>' Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"true") %>'
                                                    ToolTip="Restore Item"></asp:ImageButton>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CssClass="imgIcon tip_right_top"
                                                    ImageUrl="~/System/Backend/lib/img/Delete.png" CommandName="deleteitem" CommandArgument='<%# Eval("ImageId") %>'
                                                    OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                                    Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"false") %>'
                                                    title="Delete Item"></asp:ImageButton>
                                            </div>
                                        </td>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <center>
                                            There are no files</center>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                <asp:ObjectDataSource ID="objImages" runat="server" SelectMethod="LoadByDeleteState"
                                    TypeName="DAL.AlbumImageManagement">
                                    <SelectParameters>
                                        <asp:Parameter Name="AlbumId" Type="String" />
                                        <asp:ControlParameter ControlID="drpViews" Name="Active" PropertyName="SelectedValue"
                                            Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:AjaxFileUpload ID="AjaxFileUpload1" MaximumNumberOfFiles="20" OnClientUploadComplete="ajaxFileUpload1_ClientUploadComplete"
                                    runat="server" OnUploadComplete="AjaxFileUpload1_UploadComplete" />
                            </td>
                        </tr>                   
                        <tr>
                            <td class="msg" runat="server">
                                <asp:Label ID="lblMessge" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
