<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSmallSearch.ascx.cs" Inherits="System.Backend.UserControls.ucSmallSearch" %>
<table>
    <tr>
        <td>بحث باكواد الحاضرين<span class="reqstar"> *</span>
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtCodes" TextMode="MultiLine" SkinID="txtmult"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtCodes" ID="RequiredFieldValidator1" ValidationGroup="2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Button runat="server" ID="btnCodesSearch" Text="بحث" ValidationGroup="2" OnClick="btnCodesSearch_Click" />
        </td>
    </tr>
    <tr>
        <td>بحث عام<span class="reqstar"> *</span>
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtSearch"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtSearch" ID="RequiredFieldValidator4" ValidationGroup="3" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Button runat="server" ID="btnGeneralSearch" Text="بحث" ValidationGroup="3" OnClick="btnGeneralSearch_Click" />
        </td>
    </tr>
    <tr>
        <td>نتائج البحث
        </td>
        <td>
           <asp:ListBox DataTextField="PersonName" DataValueField="PersonId" runat="server" Height="70px" Width="300px" ID="lstResult">

           </asp:ListBox>
        </td>
        <td>
          
        </td>
    </tr>
</table>
