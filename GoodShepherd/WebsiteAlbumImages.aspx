<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="WebsiteAlbumImages.aspx.cs" Inherits="GoodShepherd.WebsiteAlbumImages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <link rel="stylesheet" href="themes/Default/Gallery/css/prettyPhoto.css" type="text/css" media="screen" title="prettyPhoto main stylesheet" charset="utf-8" />
    <script src="themes/Default/Gallery/js/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>
    <style type="text/css" media="screen">
        ul li {
            display: inline;
        }

        .wide {
            border-bottom: 1px #000 solid;
            width: 4000px;
        }

        .fleft {
            float: left;
          
        }
        .cboth {
            clear: both;
        }
    </style>
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        <asp:Label runat="server" ID="lblTitle"></asp:Label>
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblDate" CssClass="SiteText"></asp:Label></td>
        </tr>
        
        <tr>
            <td>
                <asp:Label runat="server" ID="lblDESC" CssClass="SiteText"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.AlbumImageManagement">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="AlbumId" QueryStringField="id" Type="String" />
                        <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div style="padding-left:20px" >
                    <ul class="gallery clearfix">
                        <asp:Repeater DataSourceID="ods" runat="server" ID="rptImages">
                            <ItemTemplate>
                                <li><a href='<%# "images/S600_600/" + Eval("ImageFile") %>' rel="prettyPhoto[gallery2]">
                                    <div class="dvimg">
                                        <img class="imgGallery" style="background-color: white; margin-top: 2px" src='<%# "images/S70_60/" + Eval("ImageFile") %>' width="70" height="60" />
                                    </div>
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <script type="text/javascript" charset="utf-8">
                        $(document).ready(function () {
                            $("area[rel^='prettyPhoto']").prettyPhoto();
                            $(".gallery:first a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'fast', slideshow: 10000, hideflash: true, autoplay_slideshow: true });
                            $(".gallery:gt(0) a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'fast', slideshow: 10000, hideflash: true });

                            $("#custom_content a[rel^='prettyPhoto']:first").prettyPhoto({
                                custom_markup: '<div id="map_canvas" style="width:260px; height:265px"></div>',
                                changepicturecallback: function () { initialize(); }
                            });
                            $("#custom_content a[rel^='prettyPhoto']:last").prettyPhoto({
                                custom_markup: '<div id="bsap_1259344" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6"></div><div id="bsap_1237859" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6" style="height:260px"></div><div id="bsap_1251710" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6"></div>',
                                changepicturecallback: function () { _bsap.exec(); }
                            });
                        });
                    </script>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
