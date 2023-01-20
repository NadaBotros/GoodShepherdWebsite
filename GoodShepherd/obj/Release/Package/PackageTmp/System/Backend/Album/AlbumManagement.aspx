<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="AlbumManagement.aspx.cs" Inherits="System.Backend.AlbumManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="البوم الذكريات" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <div class="dvtabs">
        <table width="99%">
            <tr>
                <td id="msg" runat="server">
                    <asp:Label ID="lblMessge" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TabContainer ID="TabContainer" runat="server" Width="99%"
                        CssClass="TabContainer" AutoPostBack="False" OnDemand="False" ActiveTabIndex="0">
                        <asp:TabPanel HeaderText="بيانات الالبوم" ID="tabAlbum" runat="server">
                            <ContentTemplate>
                                <table cellpadding="5">
                                    <tr>
                                        <td>اسم الالبوم
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>يظهر لكل الناس
                                        </td>
                                        <td>
                                            <asp:RadioButtonList runat="server" ID="radForAll" RepeatDirection="Horizontal">
                                                
                                                <asp:ListItem Value="1">نعم</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="0">لا</asp:ListItem>
                                                
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تاريخ الالبوم</td>
                                        <td>
                                            <asp:CalendarExtender Format="d/M/yyyy" ID="ceMarriageDate" TargetControlID="txtAlbumDate" runat="server" Enabled="True"></asp:CalendarExtender>
                                            <asp:TextBox runat="server" ID="txtAlbumDate"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1" ControlToValidate="txtAlbumDate"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:HyperLink runat="server" ID="lnkPamflet" Target="_blank" Text="تحميل البامفلت"></asp:HyperLink></td>
                                    </tr>
                                    <tr>
                                        <td>بامفلت المؤتمر</td>
                                        <td>
                                            <asp:FileUpload runat="server" ID="fpldPamflet" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Image ID="imgMain" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td>صورة غلاف الالبوم</td>
                                        <td>
                                            <asp:FileUpload runat="server" ID="fpldImage" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>نبذة عن الالبوم</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDesc" SkinID="txtmult" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel HeaderText="صور الالبوم" ID="tabImages" runat="server">
                            <ContentTemplate>
                                <asp:Literal runat="server" ID="frmImages"></asp:Literal>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="tdbtns">
                    <asp:Button ID="btnBack" runat="server" Text="رجوع للصفحة السابقة" OnClick="btnBack_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="تفريغ الخانات" OnClick="btnClear_Click" />
                    <asp:Button ID="btnSaveAndNew" ValidationGroup="1" runat="server" Text="حفظ البيانات واضافة جديد"
                        OnClick="btnSaveAndNew_Click" />
                    <asp:Button ID="btnSave" ValidationGroup="1" runat="server" Text="حفظ البيانات" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
