<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="System.Backend._default" %>

<%@ Register Src="lib/Capcha/MyCaptcha.ascx" TagName="MyCaptcha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>لوحة تحكم الموقع</title>    
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
<body id="login-body">
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
                                    <li class="active lnkfrm"><a href="default.aspx">تسجيل الدخول</a> </li>
                                    <li class="lnkfrm"><a href="ForgetPassword.aspx">هل نسيت كلمة المرور ؟</a> </li>
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
                            <li><a href="javascript:void(0);"><i class="fontello-icon-home f12"></i>تسجيل الدخول</a>
                        </ul>
                    </div>
                    <!-- // breadcrumbs -->
                </div>
                <!-- // drawer -->
                <div id="dvLogin">
                    <table cellpadding="5" dir="rtl">
                        <tr>
                            <td>
                            </td>
                            <td class="tdHeader">
                                لوحـــة تــحــكم الـمـوقع
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="tdLogin">
                                <img width="80px" src="lib/img/lock.png" style="padding-left: 30px; padding-right: 10px" />
                                <div style="padding-top: 55px;text-align:right">
                                    <asp:CheckBox Style="padding-right: 5px;padding-top:3px; float: right" ID="chkRemberMe" runat="server" /><span
                                        style="display: block;padding-right:5px; float: right">تذكرني</span><br />
                                    <a href="ForgetPassword.aspx" style="color: #22878E; padding-top: 5px;float:right" id="forget-password">
                                        نسيت كلمة المرور?</a>
                                </div>
                            </td>
                            <td>
                                <table cellpadding="0" class="tableLogin" style="vertical-align: top" width="205px" cellspacing="0">
                                    <tr>
                                        <td valign="top">
                                            <img class="loginIcon" src="lib/img/avatar.png" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" SkinID="12" CssClass="txtLogin" ID="txtUserName"></asp:TextBox>
                                            <asp:TextBoxWatermarkExtender WatermarkText="اسم المستخدم" ID="txtUserName_TextBoxWatermarkExtender"
                                                runat="server" TargetControlID="txtUserName">
                                            </asp:TextBoxWatermarkExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <img class="loginIcon" src="lib/img/key.png" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" SkinID="12" CssClass="txtLogin" TextMode="Password" ID="txtPassword"></asp:TextBox>
                                            <asp:TextBoxWatermarkExtender WatermarkText="كلمة المرور" ID="txtPassword_TextBoxWatermarkExtender"
                                                runat="server" TargetControlID="txtPassword">
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
                                                        <asp:Image Height="40px" Width="125px" Style="float: left" runat="server" ID="imgCapatcha" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgChange" ToolTip="تغير رمز التاكيد" ImageUrl="lib/img/Reload.png"
                                                            runat="server" Style="float: left; padding-left: 5px" OnClick="imgChange_Click1" />
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
                                            <asp:TextBox runat="server" SkinID="12" CssClass="txtLogin" ID="txtCapatcha"></asp:TextBox>
                                            <asp:TextBoxWatermarkExtender WatermarkText="رمز التاكيد" ID="txtCapatcha_TextBoxWatermarkExtender"
                                                runat="server" TargetControlID="txtCapatcha">
                                            </asp:TextBoxWatermarkExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-top: 5px">
                                <asp:LinkButton runat="server" ID="btnLogin" CssClass="btnLogin" Text="دخول" OnClick="btnLogin_Click"></asp:LinkButton>
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
