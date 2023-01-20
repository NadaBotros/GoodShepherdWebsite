<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="ImagesSizeManagement.aspx.cs" Inherits="System.Backend.ImagesSizeManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="Images Size" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="Images Size  Management" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="msg" runat="server">
                <asp:Label ID="lblMessge" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" class="tblMain" cellspacing="2">
                    <tr>
                        <td>
                            Section Name <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSection" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
                                ControlToValidate="txtSection"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Width <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWidth" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqTitle" runat="server" ValidationGroup="1" ControlToValidate="txtWidth"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reqWidth" ValidationGroup="1" ControlToValidate="txtWidth"
                                runat="server" ValidationExpression="[0-9]+"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Height<span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHeight" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqHieight" runat="server" ValidationGroup="1" ControlToValidate="txtHeight"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reqH" ValidationGroup="1" ControlToValidate="txtHeight"
                                runat="server" ValidationExpression="[0-9]+"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Folder Name <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFolderName" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Curved Corners
                        </td>
                        <td>
                            <asp:RadioButtonList ID="radCurved" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True">Allow Curving</asp:ListItem>
                                <asp:ListItem Value="False" Selected="True">No Curving</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Image Curve Radius
                        </td>
                        <td>
                            <asp:TextBox ID="txtCurveWidth" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Convert Image to Gary Scale
                        </td>
                        <td>
                            <asp:RadioButtonList ID="radGrayScale" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True">Yes</asp:ListItem>
                                <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Resize Image Width
                        </td>
                        <td>
                            <asp:RadioButtonList ID="radResizeWidth" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="True">Yes</asp:ListItem>
                                <asp:ListItem Value="False">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Resize Image Height
                        </td>
                        <td>
                            <asp:RadioButtonList ID="radResizeHeight" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="True">Yes</asp:ListItem>
                                <asp:ListItem Value="False">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Allow Crop Images
                        </td>
                        <td>
                            <asp:RadioButtonList ID="radCrop" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True">Yes</asp:ListItem>
                                <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Description
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Height="80px" Width="200px"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdbtns">
                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />                            
                            <asp:Button ID="btnSaveAndNew" ValidationGroup="1" runat="server" Text="Save And New"
                                OnClick="btnSaveAndNew_Click" />
                                <asp:Button ID="btnSave" ValidationGroup="1" runat="server" Text="Save" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
