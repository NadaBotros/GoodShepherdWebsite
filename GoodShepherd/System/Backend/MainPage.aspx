<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="System.Backend.Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="http://fonts.googleapis.com/earlyaccess/notokufiarabic.css" rel="stylesheet" type="text/css" />
    <link href="lib/css/lib/bootstrap.css" rel="stylesheet">
    <link href="lib/css/lib/bootstrap-responsive.css" rel="stylesheet">
    <link href="lib/css/extension.css" rel="stylesheet">
    <link href="lib/css/boo.css" rel="stylesheet">
    <link href="lib/css/boo-coloring.css" rel="stylesheet">
    <link href="lib/css/boo-utility.css" rel="stylesheet">
    <script src="lib/js/lib/jquery.js"></script>
    <script src="lib/js/lib/jquery-ui.js"></script>
    <link href="lib/css/admin.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#ascrail2000').css("display", "none");
            $('#frmMain').css("height", $(window).height() - 80);
            $('.fontello-icon-cw').click(function (e) {
                e.preventDefault();
                document.getElementById('frmMain').contentWindow.location.reload();
            });
            $('.accordion-content a,.TopMenu a,.lnkfrm, .notification').click(function (e) {
                e.preventDefault();
                $("#frmMain").attr("src", $(this).attr("href"));
            }); //.,ASFGHJKL;'
            $('.TopMenu li').click(function () {
                $('.TopMenu li').removeClass("active");
                $(this).addClass("active");
            });
        });
    </script>
</head>
<body class="sidebar-left panel-side">
    <form id="form1" runat="server">
        <div class="page-container">
            <div id="header-container">
                <div id="header">
                    <div class="navbar navbar-inverse navbar-fixed-top">
                        <div class="navbar-inner">
                            <div class="container-fluid">
                                <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
                                    <span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
                                </button>

                                <div class="nav-collapse collapse">
                                    <ul class="nav user-menu visible-desktop">
                                        <li><a class="btn-glyph fontello-icon-mail-1 tip-bc" target="_blank" href="http://webmail.secureserver.net"
                                            title="البريد الالكتروني"></a></li>
                                    </ul>
                                    <ul class="nav TopMenu">
                                        <li class="active" runat="server">
                                            <asp:HyperLink ID="lnkSearch" runat="server" NavigateUrl="dashboard.aspx">لوحة التحكم</asp:HyperLink>
                                        </li>
                                       <%-- <li>
                                            <asp:HyperLink ID="lnkUsers" runat="server" NavigateUrl="manage/UsersList.aspx"><span class="fontello-icon-users"></span>مستخدمين الموقع</asp:HyperLink>
                                        </li>--%>
                                        <li>
                                            <asp:HyperLink ID="lnkPassword" runat="server" NavigateUrl="~/System/Backend/manage/ChangeInfo.aspx">تعديل بياناتي</asp:HyperLink>
                                        </li>
                                        <li>
                                            <asp:HyperLink ID="lnkContacts" runat="server" NavigateUrl="manage/Contactus.aspx">مبرمج الموقع</asp:HyperLink>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="header-drawer">
                        <div class="mobile-nav text-center visible-phone">
                            <a href="javascript:void(0);" class="mobile-btn" data-toggle="collapse" data-target=".sidebar">
                                <i class="aweso-icon-chevron-down"></i>Components</a>
                        </div>
                        <!-- // Resposive navigation -->
                        <div class="breadcrumbs-nav hidden-phone">
                            <ul id="breadcrumbs" class="breadcrumb">
                                <li><a href="javascript:void(0);"><i class="fontello-icon-home f12"></i>لوحة تحكم الموقع</a>
                            </ul>
                        </div>
                        <!-- // breadcrumbs -->
                    </div>
                </div>
            </div>
            <div id="main-container">
                <div id="main-sidebar" class="sidebar sidebar-inverse">
                    <div class="sidebar-item ">
                        <div class="media profile">
                            <div class="media-thumb media-left thumb-bordereb">
                                <a class="img-shadow lnkfrm" href="manage/ChangeInfo.aspx">
                                    <asp:Image runat="server" ImageUrl="lib/img/avatar.jpg" ID="imgUser" />
                                </a>
                            </div>
                            <div class="media-body">
                                <h5 class="media-heading">
                                    <asp:Label runat="server" ID="lblUserName"></asp:Label>
                                </h5>
                                <p class="data">
                                    البريد الالكتروني :
                                <asp:Label runat="server" ID="lblEmail"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                    <ul id="mainSideMenu" class="nav nav-list nav-side">
                        <li class="accordion-group">
                            <div class="accordion-heading active">
                                <a href="#accDash" data-parent="#mainSideMenu" data-toggle="collapse" class="accordion-toggle">
                                    <span class="item-icon fontello-icon-monitor"></span><i ></i>لوحة تحكم الموقع </a>
                            </div>
                        </li>
                       
                        <asp:Repeater DataSourceID="odsMainMenu" runat="server" OnItemDataBound="rptMenu_ItemDataBound" ID="rptMenu">
                            <ItemTemplate>
                                <li class="accordion-group">
                                    <asp:Label runat="server" ID="lblId" Visible="false" Text='<%#Eval("SiteTreeId") %>'></asp:Label>
                                    <div class="accordion-heading">
                                        <a href='<%# "#"+Eval("SiteTreeId") %>' data-parent="#mainSideMenu" data-toggle="collapse" class="accordion-toggle">
                                            <span class="item-icon fontello-icon-chart"></span><i class="chevron fontello-icon-left-open-3"></i><%#Eval("PageTitle") %></a>
                                    </div>
                                    <ul class="accordion-content nav nav-list collapse" id='<%#Eval("SiteTreeId") %>'>
                                        <asp:Literal runat="server" ID="ltrEmpty"></asp:Literal>
                                        <asp:Repeater runat="server" ID="rptSubMenu">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:HyperLink ID="lnkSubMenu" runat="server" NavigateUrl='<%#Eval("PageFile") %>'><i class="fontello-icon-left-dir"></i>
                             <%#Eval("PageTitle") %></asp:HyperLink>
                                                </li>
                                            </ItemTemplate>
                                            
                                        </asp:Repeater>
                                        <asp:ObjectDataSource TypeName="DAL.SiteTreeManage" SelectMethod="LoadByDeleteState" runat="server" ID="odsSubMenu">
                                            <SelectParameters>
                                                <asp:Parameter Name="Active" Type="String" DefaultValue="True" />
                                                <asp:Parameter Name="ParentSiteTreeId" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </ul>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:ObjectDataSource TypeName="DAL.SiteTreeManage" SelectMethod="LoadByDeleteState" runat="server" ID="odsMainMenu">
                            <SelectParameters>
                                <asp:Parameter Name="Active" Type="String" DefaultValue="True" />
                                <asp:Parameter Name="ParentSiteTreeId" Type="String" DefaultValue="" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                    </ul>
                    <!-- // sidebar menu -->
                    <div class="sidebar-item">
                    </div>
                    <!-- // sidebar item -->
                </div>
                <!-- // sidebar -->
                <div id="main-content" class="main-content container-fluid">
                    <iframe id="frmMain" src="dashboard.aspx" scrolling="auto" height="600px" onload="iframeLoaded()"
                        frameborder="0" style="padding: 0; margin: 0;" width="100%"></iframe>
                </div>
                <!-- // main-content -->
            </div>
            <!-- // main-container  -->
            <footer id="footer-fix">
        <div id="footer-sidebar" class="footer-sidebar">
            <div class="navbar">
                <div class="btn-toolbar"> <a class="btn btn-glyph btn-link" href="javascript:void(0);"><i class="fontello-icon-up-open-1"></i></a> </div>
            </div>
        </div>        
        <div id="footer-content" class="footer-content">
            <div class="navbar navbar-inverse">
                <div class="navbar-inner">
                    <ul class="nav pull-left">
                        <li class="divider-vertical hidden-phone"></li>
                        <li><a id="btnToggleSidebar" class="btn-glyph fontello-icon-resize-full-2 tip hidden-phone" href="javascript:void(0);" title="اخفاء واظهار القائمة"></a></li>
                        <li class="divider-vertical hidden-phone"></li>
                        <li><a id="btnChangeSidebar" class="btn-glyph fontello-icon-login tip hidden-phone" href="javascript:void(0);" title="تغير مكان القائمة"></a></li>
                        <li class="divider-vertical"></li>
                        <li><a id="btnChangeSidebarColor" class="btn-glyph fontello-icon-palette tip" href="javascript:void(0);" title="تبديل لون الخلفية"></a></li>
                        <li class="divider-vertical"></li>
                        <li><a class="btn-glyph  fontello-icon-cw tip" id="lnkreload" title="اعادة تحميل الصفحة" href="#" ></a></li>
                        <li class="divider-vertical"></li>
                    
                    </ul>
                    <ul class="nav pull-right">
                        <li class="divider-vertical"></li>
                        <li><a class="btn-glyph lnkfrm fontello-icon-help-2 tip" href="manage/help.aspx" title="مساعدة"></a></li>
                        <li class="divider-vertical"></li>
                        <li><a id="btnLogout" class="btn-glyph fontello-icon-logout-1 tip" href="Logout.aspx" title="خروج"></a></li>
                        <li class="divider-vertical"></li>
                        <li><a id="btnScrollup" class="scrollup btn-glyph fontello-icon-up-open-1" href="javascript:void(0);"><span class="hidden-phone">الى الاعلي</span></a></li>
                    </ul>
                </div>
            </div>
        </div>       
    </footer>
        </div>
       <script src="lib/plugins/bootstrap-wysihtml5/lib/js/wysihtml5-0.3.0.min.js"></script>
        <script src="lib/js/lib/bootstrap.js"></script>
        <script src="lib/js/lib/jquery.cookie.js"></script>
        <!-- Plugins Bootstrap -->
        <script src="lib/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="lib/plugins/bootstrap-timepicker/js/bootstrap-timepicker.js"></script>
        <script src="lib/plugins/bootstrap-daterangepicker/js/date.js"></script>
        <script src="lib/plugins/bootstrap-daterangepicker/js/bootstrap-daterangepicker.js"></script>
        <script src="lib/plugins/bootstrap-rowlink/js/bootstrap-rowlink.js"></script>
        <script src="lib/plugins/bootstrap-progressbar/js/bootstrap-progressbar.js"></script>
        <script src="lib/plugins/bootstrap-wysihtml5/src/bootstrap-wysihtml5.js"></script>
        <script src="lib/plugins/bootstrap-select/bootstrap-select.js"></script>
        <script src="lib/plugins/bootstrap-bootbox/bootbox.min.js"></script>
        <script src="lib/plugins/bootstrap-modal/js/bootstrap-modalmanager.js"></script>
        <script src="lib/plugins/bootstrap-modal/js/bootstrap-modal.js"></script>
        <script src="lib/plugins/bootstrap-wizard/js/bootstrap-wizard.js"></script>
        <script src="lib/plugins/bootstrap-toggle-buttons/js/bootstrap-toggle-buttons.js"></script>
        <!-- Plugins Custom -->
        <script src="lib/plugins/google-code-prettify/prettify.js"></script>
        <script src="lib/plugins/nicescroll/jquery.nicescroll.min.js"></script>
        <script src="lib/plugins/qtip2/dist/jquery.qtip.min.js"></script>
        <!-- Plugins Forms -->
        <script src="lib/plugins/uniform/jquery.uniform.js"></script>
        <script src="lib/plugins/select2/select2.min.js"></script>
        <script src="lib/plugins/counter/jquery.counter.js"></script>
        <script src="lib/plugins/elastic/jquery.elastic.js"></script>
        <script src="lib/plugins/inputmask/jquery.inputmask.js"></script>
        <script src="lib/plugins/inputmask/jquery.inputmask.extensions.js"></script>
        <script src="lib/plugins/xbreadcrumbs/xbreadcrumbs.js"></script>
        <!-- main js -->
        <script src="lib/js/application.js"></script>
        <!-- Only This Demo Page -->
        <script src="lib/js/demo/demo-dashboard1.js"></script>
    </form>
</body>
</html>
