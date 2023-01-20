<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyCaptcha.ascx.cs" Inherits="MyCaptcha" %>
<table>
    <tr>
        <td colspan="2" style="text-align: left">
            <asp:Image ID="ImgCaptcha" Width="150px" runat="server" />
        </td>
    </tr>
    <tr>
        <td valign="middle">
            <asp:TextBox ID="TxtCpatcha" runat="server" Text=""></asp:TextBox>
        </td>
        <td>
            <asp:ImageButton ID="imgChange" ToolTip="Change Image Code" ImageUrl="~/App_Themes/Default/Images/change.jpg"
                runat="server" OnClick="imgChange_Click" />
        </td>
    </tr>
</table>
