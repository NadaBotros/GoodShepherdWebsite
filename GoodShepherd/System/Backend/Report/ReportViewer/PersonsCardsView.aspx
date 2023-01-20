<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonsCardsView.aspx.cs" Inherits="System.Backend.PersonsCardsView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/barcode/stylesheet.css" rel="stylesheet" />
    <style type="text/css">
        body {
            margin: auto;
            padding: 0px;
            margin: 0px;
            font-family: tahoma;
            font-size: 15px;
            text-align: right;
            
        }

        .dvMain {
            margin: auto;
            width: 702px;
        }

        @media print {
            .dvMain {
                margin: 10px;
            }
        }

        .break {
            page-break-after: always;
        }

        .dvRight 
        {
            float: right;
            height: 215px;
            vertical-align: middle;
            overflow: hidden;
            width: 332px;
        }

        .dvLeft {
            float: right;
            margin-right: 38px;
            height: 215px;
            vertical-align: middle;
            overflow: hidden;
            width: 332px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
  
        <div class="dvMain" style="page-break-before: always">
            <asp:Literal runat="server" ID="ltrInfo"></asp:Literal>
        </div>
    </form>
</body>
</html>
