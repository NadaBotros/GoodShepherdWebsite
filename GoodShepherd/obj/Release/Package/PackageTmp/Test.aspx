<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="GoodShepherd.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/QR.ashx?QRText=http://google.com&width=200&height=200" />
    <asp:Button runat="server" ID="btnChangeImages" Text="رفع صور الاشخاص" OnClick="btnChangeImages_Click"/>
    </div>
    </form>
</body>
</html>
