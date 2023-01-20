<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="themes/Default/Slider/css/feature-carousel.css" charset="utf-8" />

    <script src="themes/Default/Slider/js/jquery.featureCarousel.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="themes/Default/ReadMore/readmore.js"></script>
    <script src="themes/Default/MagazineSlider/owl-carousel/owl.carousel.js"></script>
  
    <script type="text/javascript">
        $(document).ready(function () {
            var carousel = $("#carousel").featureCarousel({
                // include options like this:
                // (use quotes only for string values, and no trailing comma after last option)
                // option: value,
                // option: value
            });
            $('.readmore').readmore({
                speed: 75,
                maxHeight: 153
            });

        });
        </script>
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="sitecontent" runat="server">
    <div class="dvFullWidth" >
        <div class="dvSite" id="dvGallery">
            <div class="carousel-container" style="margin-top: -10px; margin-bottom: 10px">
                <div id="carousel">
                    <asp:Repeater runat="server" DataSourceID="odsNews" ID="rptGallery">
                        <ItemTemplate>
                            <div class="carousel-feature">
                                <a href='<%#"newsdetails.aspx?id="+Eval("NewsId") %>'>
                                    <img class="carousel-image" alt="Image Caption" src='<%#"images/s450_220/"+Eval("NewsImage") %>'></a>
                                <div class="carousel-caption">
                                    <p style="font-weight: bold;">
                                        <%#Eval("NewsTitle") %><br />
                                    </p>
                                    <%-- <p style="font-weight: normal;">
                                <%#DAL.GeneralMethods.CutString(Eval("NewsContent"),100) %>
                            </p>--%>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:ObjectDataSource runat="server" ID="odsNews" SelectMethod="NewsGalleries" TypeName="DAL.HomeManage"></asp:ObjectDataSource>
                </div>
                <div id="carousel-left">
                    
                </div>
                <div id="carousel-right">
                    <%--<img src="themes/Default/img/GalleryNext.png" />--%>
                </div>
            </div>
        </div>
        <div class="dvFullWidth dvFullProgram" >
            <div class="dvSite dvJesus" style="width: 972px">
                <table width="100%">
                    <tr>
                        <td>
                            <table style="float: right; padding-right: 14px; padding-top: 19px">
                                <tr>
                                    <td>
                                        <div class="dvProgram">
                                            
                                        </div>
                                       
                                    </td>
                                    <td style="padding-right: 10px">
                                        <span class="title2">برنامج المتكلمين لهذا الشهر</span>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView SkinID="grdsite" Style="border-collapse: separate;" runat="server" ID="grdProgram" AutoGenerateColumns="False" DataSourceID="odsMeeting">
                                <Columns>
                                    <asp:BoundField DataField="SpeakerName" ItemStyle-Width="25%" HeaderText="اسم المتكلم" />
                                    <asp:BoundField DataField="MeetingTitle" ItemStyle-Width="30%" HeaderText="العظة" />
                                    <asp:TemplateField HeaderText="التاريخ" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#DAL.GeneralMethods.ConvertToArabicDateString(Eval("MeetingDate")) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="البث المباشر">
                                        <ItemTemplate>
                                            <table style="margin: auto">
                                                <tr>
                                                    <td>
                                                        <asp:HyperLink runat="server" ImageUrl="~/themes/Default/img/Sound.png" NavigateUrl='<%# "MeetingDetails.aspx?mid=" +Eval("MeetingId")  %>' ID="lnkVideo"></asp:HyperLink></td>

                                                    <td>
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# "MeetingDetails.aspx?mid=" +Eval("MeetingId")  %>' ImageUrl="~/themes/Default/img/video.png" ID="HyperLink1"></asp:HyperLink></td>
                                                    <td></td>
                                                </tr>
                                            </table>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsMeeting" runat="server" SelectMethod="MeetingInHomes" TypeName="DAL.HomeManage"></asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="dvFullWidth dvWhite" id="dvMainSite">
            <div class="dvSite">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td valign="top" class="tdVertical">
                            <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlMyInfoTitle">
                                <span>بياناتي</span>
                                <asp:Image ID="imgMyInfo" CssClass="imgCollapsible" runat="server" />
                            </asp:Panel>
                            <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlMyInfo">
                                <table cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="lnkChangePassword" runat="server" NavigateUrl="~/ChangePassword.aspx" Text="- تعديل كلمة المرور" CssClass="txt"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="lnkChangeImage" runat="server" NavigateUrl="~/ChangeMyPicture.aspx" Text="- تعديل الصورة الشخصية" CssClass="txt"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="lnkChangeMyInfo" runat="server" NavigateUrl="~/MyInfo.aspx" Text="- تعديل بياناتي" CssClass="txt"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="lnkMotamarat" NavigateUrl="~/Albums.aspx" runat="server" Text="- صور المؤتمرات" CssClass="txt"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="lnkMyMeeting" NavigateUrl="~/MeetingAttendance.aspx" runat="server" Text="- حضور الاجتماعات" CssClass="txt"></asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div class="dvVertical" style="float: right">
                                <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlFllowUsTitle">
                                    <span>تتبعنا</span>
                                    <asp:Image ID="imgFllowUs" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlFllowUsContent">
                                    <table style="margin: auto">
                                        <tr>
                                            <td>
                                                <a href="https://twitter.com/AlraeiAlsaleh" target="_blank">
                                                    <img src="themes/Default/img/Twitter.png" /></a></td>
                                            <td style="padding-right: 19px">
                                                <a href="https://www.facebook.com/alraei.alsaleh" target="_blank">
                                                    <img src="themes/Default/img/Facebook.png" /></a></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnLiveStreamTitle">
                                    <span>بث الاجتماع</span>
                                    <asp:Image ID="imgLiveStream" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnLiveStreamContent">
                                    <table cellpadding="2" style="margin: auto; width: 180px; text-align: center">
                                        <tr>
                                            <td align="center">الصوت</td>
                                            <td align="center">الفيديو
                                            </td>
                                        </tr>
                                        <tr>

                                            <td align="center">
                                                <asp:HyperLink Style="display: block; padding: 3px; width: 60px" ID="lnkAudioStream" runat="server" NavigateUrl="~/AudioStream.aspx"></asp:HyperLink>
                                            </td>
                                            <td align="center">
                                                <asp:HyperLink Style="display: block; padding: 3px; width: 60px" ID="lnkVideoStream" runat="server" NavigateUrl="~/VideoStream.aspx"></asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblStreamAudio"></asp:Label></td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblStreamVideo"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlTravelTitle">
                                    <span>رحلات ومؤتمرات</span>
                                    <asp:Image ID="imgTravel" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlTravelContent">
                                    <asp:Repeater runat="server" DataSourceID="odsActivities" ID="rptActivities">
                                        <ItemTemplate>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label runat="server" CssClass="txt3" ID="lblTitle" Text='<%# Eval("ActivityTitle") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label runat="server" CssClass="txt3" ID="lblActivityPlace" Text='<%# Eval("ActivityPlace") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label runat="server" CssClass="txt3" ID="lblDaysNo" Text='<%# Eval("DaysNo") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label runat="server" CssClass="txt3" ID="lblActivityDate" Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:HyperLink runat="server" CssClass="txt3" ID="Label3" Text="المزيد" NavigateUrl='<%#"Activitydetails.aspx?id=" +Eval("ActivityId") %>'></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <SeparatorTemplate>
                                            <hr style="padding: 0px; margin: 0px; border: 0px; height: 1px; background-color: gray; width: 60%; margin: auto" />
                                        </SeparatorTemplate>
                                    </asp:Repeater>
                                    <asp:ObjectDataSource runat="server" ID="odsActivities" SelectMethod="Activities" TypeName="DAL.HomeManage"></asp:ObjectDataSource>
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlZakryatTitle">
                                    <span>ذكريات</span>
                                    <asp:Image ID="imgZakryat" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlZakryatContent">
                                    <table style="margin: auto">
                                        <tr>
                                            <td align="center">
                                                <asp:HyperLink runat="server" ID="lnkspeakerimage"></asp:HyperLink></td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:HyperLink runat="server" CssClass="lnk2" ID="lnkspeakername"></asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlAzwagTitle">
                                    <span>أعياد زواج اليوم</span>
                                    <asp:Image ID="imgAzwag" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlAzwagContent">
                                    <asp:Repeater runat="server" DataSourceID="odsAgwaz" ID="rpt">
                                        <ItemTemplate>
                                            <table style="padding-bottom: 6px;">
                                                <tr>
                                                    <td class="txt3">الزوج :<%# "- "+ Eval("Husband") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="txt3">الزوجة : <%# "- "+ Eval("Wife") %></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:ObjectDataSource ID="odsAgwaz" runat="server" SelectMethod="MarriageDate" TypeName="DAL.HomeManage"></asp:ObjectDataSource>
                                </asp:Panel>
                            </div>
                        </td>
                        <td id="tdPageContent" valign="top" style="padding-top: 26px;">
                            <table class="dvFullWidth">
                                <tr>
                                    <td>
                                        <div class="dvMainPageTitle">
                                            <div class="dvPageTitle">
                                                اللقاء اليومي مع المسيح
                                            </div>
                                            <div class="dvPageTitleBg"></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="dvFullWidth">
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" CssClass="siteTitle" ID="lblArticleTitle"></asp:Label></td>
                                                <td align="left">
                                                    <asp:Label runat="server" CssClass="siteTitle" ID="lblArticleWriter" Style="float: left; text-align: left"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label runat="server" CssClass="SiteText" ID="lblArticleAya"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label runat="server" CssClass="SiteText readmore" ID="lblArticleText"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="dvMainPageTitle">
                                            <div class="dvPageTitle">
                                                قصة من المجلة
                                            </div>
                                            <div class="dvPageTitleBg"></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label runat="server" ID="lblStoryTitle" Style="text-align: center" CssClass="siteTitle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblStoryText" CssClass="SiteText readmore"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <iframe src="IframeMagazineGallery.aspx" frameborder="0" height="260px" width="464px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" class="tdVertical">
                            <div class="dvVertical" style="float: left">
                                <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlAyaTitle">
                                    <span>آية اليوم</span>
                                    <asp:Image ID="imgAya" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlAyaContent">
                                    <asp:Label runat="server" ID="lblAya" CssClass="txt3"></asp:Label>
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlA2walTitle">
                                    <span>أقوال الآباء</span>
                                    <asp:Image ID="imgA2wal" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlA2walContent">
                                    <table class="dvFullWidth" style="margin: auto">
                                        <tr>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblA2walTitle" CssClass="title3"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblA2walDesc" CssClass="txt3"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlTadrebTitle">
                                    <span>تدريب روحي</span>
                                    <asp:Image ID="imgTadreb" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlTadrebContent">
                                    <table class="dvFullWidth" style="margin: auto">
                                        <tr>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblSpiritualTrainingTitle" CssClass="title3"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblSpiritualTrainingDesc" CssClass="txt3"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--   <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlMosab2aTitle">
                                    <span>مسابقة أون لاين</span>
                                    <asp:Image ID="imgMosab2a" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlMosab2aContent">
                                    <table style="margin: auto">
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>
                                </asp:Panel>--%>
                                <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlAwalMosab2aTitle">
                                    <span>أوائل المسابقة</span>
                                    <asp:Image ID="imgAwalMosab2a" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlAwalMosab2aContent">
                                    <asp:Repeater runat="server" DataSourceID="odsWinners" ID="rptWinners">
                                        <ItemTemplate>
                                            <asp:Label runat="server" CssClass="txt3" ID="lblWinners" Text='<%# Eval("PersonName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                                <asp:ObjectDataSource runat="server" ID="odsWinners" SelectMethod="QuizWinners" TypeName="DAL.HomeManage"></asp:ObjectDataSource>
                                <asp:Panel CssClass="pnlCollapsible" runat="server" ID="pnlBirthDateTitle">
                                    <span>أعياد ميلاد اليوم</span>
                                    <asp:Image ID="imgBirthdate" CssClass="imgCollapsible" runat="server" />
                                </asp:Panel>
                                <asp:Panel CssClass="pnlCollapsibleContent" runat="server" ID="pnlBirthDateontent">
                                    <asp:Repeater runat="server" DataSourceID="odsBirthdays" ID="Repeater1">
                                        <ItemTemplate>
                                            <asp:Label runat="server" CssClass="txt3" ID="lblWinners" Text='<%# "- "+ Eval("PersonName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:ObjectDataSource runat="server" ID="odsBirthdays" SelectMethod="BirthDays" TypeName="DAL.HomeManage"></asp:ObjectDataSource>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:CollapsiblePanelExtender ID="CPEFllowUs" CollapseControlID="pnlFllowUsTitle" ExpandControlID="pnlFllowUsTitle" TargetControlID="pnlFllowUsContent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgFllowUs"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="false"
            runat="server">
        </asp:CollapsiblePanelExtender>
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" CollapseControlID="pnLiveStreamTitle" ExpandControlID="pnLiveStreamTitle" TargetControlID="pnLiveStreamContent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgLiveStream"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="false"
            runat="server">
        </asp:CollapsiblePanelExtender>
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" CollapseControlID="pnlTravelTitle" ExpandControlID="pnlTravelTitle" TargetControlID="pnlTravelContent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgTravel"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="true"
            runat="server">
        </asp:CollapsiblePanelExtender>
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" CollapseControlID="pnlBirthDateTitle" ExpandControlID="pnlBirthDateTitle" TargetControlID="pnlBirthDateontent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgBirthdate"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="false"
            runat="server">
        </asp:CollapsiblePanelExtender>
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" CollapseControlID="pnlZakryatTitle" ExpandControlID="pnlZakryatTitle" TargetControlID="pnlZakryatContent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgZakryat"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="false"
            runat="server">
        </asp:CollapsiblePanelExtender>
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" CollapseControlID="pnlAyaTitle" ExpandControlID="pnlAyaTitle" TargetControlID="pnlAyaContent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgAya"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="false"
            runat="server">
        </asp:CollapsiblePanelExtender>
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" CollapseControlID="pnlA2walTitle" ExpandControlID="pnlA2walTitle" TargetControlID="pnlA2walContent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgA2wal"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="false"
            runat="server">
        </asp:CollapsiblePanelExtender>
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender7" CollapseControlID="pnlTadrebTitle" ExpandControlID="pnlTadrebTitle" TargetControlID="pnlTadrebContent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgTadreb"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="true"
            runat="server">
        </asp:CollapsiblePanelExtender>
        <%--   <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender8" CollapseControlID="pnlMosab2aTitle" ExpandControlID="pnlMosab2aTitle" TargetControlID="pnlMosab2aContent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgMosab2a"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="true"
            runat="server">
        </asp:CollapsiblePanelExtender>--%>
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender9" CollapseControlID="pnlAwalMosab2aTitle" ExpandControlID="pnlAwalMosab2aTitle" TargetControlID="pnlAwalMosab2aContent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgAwalMosab2a"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="False"
            runat="server">
        </asp:CollapsiblePanelExtender>
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender10" CollapseControlID="pnlAzwagTitle" ExpandControlID="pnlAzwagTitle" TargetControlID="pnlAzwagContent"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgAzwag"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="false"
            runat="server">
        </asp:CollapsiblePanelExtender>
        <asp:CollapsiblePanelExtender ID="clpsMyInfo" CollapseControlID="pnlMyInfoTitle" ExpandControlID="pnlMyInfoTitle" TargetControlID="pnlMyInfo"
            CollapsedImage="~/themes/Default/img/CloseDiv.png" ImageControlID="imgMyInfo"
            ExpandedImage="~/themes/Default/img/OpenDiv.png" Collapsed="false"
            runat="server">
        </asp:CollapsiblePanelExtender>
     </div>
</asp:Content>
