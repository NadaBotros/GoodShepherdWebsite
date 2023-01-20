<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs"
    Inherits="System.Backend.ForgetPassword" %>
   <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>نسيت كلمة المرور</title>
   <link href="lib/css/lib/bootstrap.css" rel="stylesheet">
    <link href="lib/css/lib/bootstrap-responsive.css" rel="stylesheet">
    <link href="lib/css/extension.css" rel="stylesheet">
    <link href="lib/css/boo.css" rel="stylesheet">
    <link href="lib/css/boo-coloring.css" rel="stylesheet">
    <link href="lib/css/boo-utility.css" rel="stylesheet">
    <script src="lib/js/lib/jquery.js"></script>
    <script src="lib/js/lib/jquery-ui.js"></script>
    <link href="lib/css/admin.css" rel="stylesheet" type="text/css" />
  
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="page-container">
        <div id="header-container">
            <div id="header">
               <div class="navbar navbar-inverse navbar-fixed-top">
                    <div class="navbar-inner">
                        <div class="container-fluid">
                            <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
                                <span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar">
                                </span>
                            </button>
                          
                            <div class="nav-collapse collapse">
                                <ul class="nav user-menu visible-desktop">
                                    <li><a class="btn-glyph fontello-icon-mail-1 tip-bc" href="http://webmail.secureserver.net"
                                        title="الدخول على البريد الالكتروني"></a></li>
                                </ul>
                                <ul class="nav TopMenu">
                                    <li class="lnkfrm"><a href="default.aspx">تسجيل الدخول</a> </li>
                                    <li class="active lnkfrm"><a href="ForgetPassword.aspx">هل نسيت كلمة المرور ؟</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- // navbar -->
                <div class="header-drawer">
                    <div class="mobile-nav text-center visible-phone">
                        <a href="javascript:void(0);" class="mobile-btn" data-toggle="collapse" data-target=".sidebar">
                            <i class="aweso-icon-chevron-down"></i>Components</a>
                    </div>
                    <!-- // Resposive navigation -->
                    <div class="breadcrumbs-nav hidden-phone">
                        <ul id="breadcrumbs" class="breadcrumb">
                            <li><a href="javascript:void(0);"><i class="fontello-icon-home f12"></i>نسيت كلمة المرور</a>
                        </ul>
                    </div>
                    <!-- // breadcrumbs -->
                </div>
                <!-- // drawer -->
                <div id="dvforgetpassword">
                    <table cellpadding="5" style="direction:rtl">
                        <tr>
                            <td>
                            </td>
                            <td class="tdHeader">
                                نسـيـت كـلـمـة الــمــرور
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="tdLogin">
                                <img width="80px" src="lib/img/lock.png" style="padding:20px 10px 0px 10px" />
                               
                            </td>
                            <td>
                                <table  cellpadding="0" class="tableLogin" style="vertical-align: top" width="205px" cellspacing="0">
                                    <tr>
                                        <td valign="top">
                                            <img class="loginIcon" src="lib/img/avatar.png" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" CssClass="textforget" SkinID="12" ID="txtEmail"></asp:TextBox>
                                            <asp:TextBoxWatermarkExtender WatermarkText="ادخل البريد الالكتروني" ID="txtEmail_TextBoxWatermarkExtender"
                                                runat="server" TargetControlID="txtEmail">
                                            </asp:TextBoxWatermarkExtender>
                                        </td>
                                    </tr>                                   
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td valign="middle">
                                                        <asp:Image Height="40px" Style="float: left" runat="server" ID="imgCapatcha" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgChange" ToolTip="تغير رمز التاكيد" ImageUrl="lib/img/Reload.png"
                                                            runat="server" Style="float: left; padding-left: 5px" OnClick="imgChange_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="loginIcon" src="lib/img/key.png" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" CssClass="textforget" SkinID="12" ID="txtCapatcha"></asp:TextBox>
                                            <asp:TextBoxWatermarkExtender WatermarkText="ادخل رمز التاكيد" ID="txtCapatcha_TextBoxWatermarkExtender"
                                                runat="server" TargetControlID="txtCapatcha">
                                            </asp:TextBoxWatermarkExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-top: 5px">
                                <asp:LinkButton runat="server" ID="btnSend" CssClass="btnLogin" Text="ارسال اسم المستخدم وكلمة المرور" OnClick="btnSend_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblMsg" Style="color: Red; text-align: center" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
