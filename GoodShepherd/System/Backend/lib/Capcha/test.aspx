<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="MyCaptcha.ascx" tagname="MyCaptcha" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <uc1:MyCaptcha ID="MyCaptcha1" runat="server" />
        <br />
        <asp:Label ID="lblCheckResult" runat="server" Text="?"></asp:Label>
        <br />
        <asp:Button ID="btnCheck" runat="server" onclick="btnCheck_Click" 
            Text="Check it!" />
    </div>
    </form>
</body>
</html>
