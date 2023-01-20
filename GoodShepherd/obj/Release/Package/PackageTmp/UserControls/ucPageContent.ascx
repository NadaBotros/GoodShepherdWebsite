<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPageContent.ascx.cs" Inherits="ucPageContent" %>
<table width="100%">
    <tr>
        <td>
            <div class="dvMainPageTitle">
                <div class="dvTitle">
                    <asp:Label runat="server" ID="lblPageTitle"></asp:Label>
                </div>
                <div class="dvTitleBg"></div>
            </div>
            <asp:ObjectDataSource ID="odsSection" runat="server" SelectMethod="LoadByDeleteState"
                TypeName="DAL.PageSectionManagement">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                    <asp:Parameter DefaultValue="" Name="PageId" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
                <asp:Repeater ID="rptSections" DataSourceID="odsSection" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td id="Td1" class="siteTitle" runat="server" visible='<%#!String.IsNullOrEmpty(Eval("SectionName").ToString()) %>'>
                                <asp:Label runat="server" ID="lblTitle" Text='<%#Eval("SectionName") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr id="Tr1" runat="server" visible='<%#!String.IsNullOrEmpty(Eval("ImageFile").ToString())&&String.IsNullOrEmpty(Eval("SectionContent").ToString()) %>'>
                            <td align="center">
                                <asp:Image runat="server" Visible='<%#!String.IsNullOrEmpty(Eval("ImageFile").ToString()) %>'
                                    ID="imgSection" ImageUrl='<%# "~/images/S500_500/"+Eval("ImageFile") %>' />
                            </td>
                        </tr>
                        <tr id="Tr2" runat="server" visible='<%#!String.IsNullOrEmpty(Eval("SectionContent").ToString()) %>'>
                            <td>
                                <table cellpadding="3" width="100%">
                                    <tr>
                                        <td valign="top">
                                            <asp:Label runat="server" CssClass="SiteText" style="line-height: 140%" ID="lblDesc" Text='<%#Eval("SectionContent").ToString().Replace("font-size","") %>'></asp:Label>
                                        </td>
                                        <td id="Td2" runat="server" visible='<%#!String.IsNullOrEmpty(Eval("ImageFile").ToString()) %>' valign="top">
                                            <asp:Image runat="server" Style="float: left" ID="imgSmall" ImageUrl='<%# "~/images/S150_150/"+Eval("ImageFile") %>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="Tr3" runat="server" align="center" visible='<%#!String.IsNullOrEmpty(Eval("VideoUrl").ToString()) %>'>
                            <td>
                                <asp:Literal runat="server" ID="ltrYoutube" Text='<%# DAL.GeneralMethods.GetYoutubeBig(Eval("VideoUrl")) %>'></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <SeparatorTemplate>
                        <tr>
                            <td>
                                <center>
                                   
                                    <img width="350px" src="../themes/Default/img/paragraph_separator.gif" />
                                </center>
                            </td>
                        </tr>
                    </SeparatorTemplate>
                </asp:Repeater>

            </table>
        </td>
    </tr>
</table>
