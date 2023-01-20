<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="Activitydetails.aspx.cs" Inherits="Activitydetails" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHHead" ID="contHead">
     <meta property="og:image" content="http://shepherdmeeting.com/shepherdmeeting.com/themes/Default/img/Logo.png" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
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
            <td align="center">
                <asp:Image runat="server" Width="60%" ID="img" />
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="5">
                    <tr>
                        <td style="font-weight: bold; width: 100px">تاريخ النشاط
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblDate"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">تفاصيل النشاط
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblDesc"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">المكان
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblPlace"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">مدة النشاط
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblPeriod"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">قيمة الاشتراك
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblPrice"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">الخادم المسؤل
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblServant"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">رقم موبايل الخادم
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMobile"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">اخر ميعاد الحجز
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLastTime"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">اسباب الاعتذار
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblRefuse"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Literal runat="server" ID="ltrVideo"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 20px; padding-bottom: 20px">
                <div style="margin: auto" class="a2a_kit a2a_kit_size_32 a2a_default_style">
                    <a class="a2a_dd" href="http://www.addtoany.com/share_save"></a>
                    <a class="a2a_button_facebook"></a>
                    <a class="a2a_button_twitter"></a>
                    <a class="a2a_button_google_plus"></a>
                    <a class="a2a_button_email"></a>
                    <a class="a2a_button_linkedin"></a>
                </div>
                <script type="text/javascript" src="//static.addtoany.com/menu/page.js"></script>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; padding-bottom: 20px">
                <asp:Literal runat="server" ID="lblFaceBookComment"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
