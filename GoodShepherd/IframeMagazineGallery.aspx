<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IframeMagazineGallery.aspx.cs" Inherits="IframeMagazineGallery" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="themes/Default/MagazineSlider/owl-carousel/owl.carousel.css" rel="stylesheet">
    <link href="themes/Default/MagazineSlider/owl-carousel/owl.theme.css" rel="stylesheet">
    <script src="themes/Default/MagazineSlider/assets/js/jquery-1.9.1.min.js"></script>
    <script src="themes/Default/MagazineSlider/owl-carousel/owl.carousel.js"></script>
     <asp:Literal runat="server" ID="ltrCurrentTheme"></asp:Literal>
    
    <link href="Styles/fonts/fonts.css" rel="stylesheet" />
    <style>
        #demo {
            width: 466px;
        }

        body {
            direction: ltr;
            background-color:white;
            background-image:none;
        }
    </style>
    <script>
        $(document).ready(function () {
            var owl = $("#owl-demo");
            owl.owlCarousel({
                items: 4, //10 items above 1000px browser width      
                itemsMobile: false // itemsMobile disabled - inherit from itemsTablet option

            });
            // Custom Navigation Events
            $("#dvGalleryNext").click(function () {
                owl.trigger('owl.next');
            })
            $("#dvGalleryBack").click(function () {
                owl.trigger('owl.prev');
            })
            $(".owl-item").css("width", "115px");
        });
            </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0"  cellspacing="0" width="466px">
            <tr>
                <td>
                    <div class="dvMainPageTitle">
                        <div class="dvPageTitle">
                            <table dir="rtl" cellpadding="0" cellspacing="0" style="float: right;">
                                <tr>
                                    <td>مجلة لقاء الراعي</td>
                                    <td style="padding-right: 213px">
                                        <div id="dvGalleryNext">
                                        </div>
                                    </td>
                                    <td>
                                        <div id="dvGalleryBack"></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="dvPageTitleBg">
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding-top:5px">
                    <div id="demo" style="width:466px;overflow-x:hidden">
                        <div class="container">
                            <div class="row">
                                <div class="span12">
                                    <div id="owl-demo" class="owl-carousel">
                                        <asp:Repeater runat="server" ID="rptMagazine" DataSourceID="odsMagazines">
                                            <ItemTemplate>
                                                <div class="item">
                                                    <table style="padding-top: 13px; width: 110px; padding-bottom: 3px; padding-right: 5px; padding-left: 5px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Image runat="server" ID="imgMagazine" ImageUrl='<%# "images/S75_106/"+Eval("MagazineCover") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label CssClass="SiteText" Text='<%#DAL.GeneralMethods.GetDate(Eval("MagazineYear"),Eval("MagazineMonth")) %>' runat="server" ID="lblMagTitle"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <a target="_blank" class="dvMagSee" href='<%#"Magazinedetails.aspx?id="+Eval("magazineid") %>'></a>
                                                                <a target="_blank" class="dvMagDownlaod" href='<%#"files/magazines/" +Eval("MagazinePDF") %>'></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:ObjectDataSource ID="odsMagazines" runat="server" SelectMethod="MagazineInHome" TypeName="DAL.HomeManage"></asp:ObjectDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
