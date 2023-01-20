<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="Sound.aspx.cs" Inherits="Sound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <div id="fb-root"></div>
    <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=448197185202052";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
    <script src="//platform.linkedin.com/in.js" type="text/javascript">
 lang: en_US
    </script>
    <script>                                                    !function (d, s, id) {
                                                        var js, fjs = d.getElementsByTagName(s)[0];
                                                        if (!d.getElementById(id)) {
                                                            js = d.createElement(s);
                                                            js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs);
                                                        }
                                                    }
 (document, "script", "twitter-wjs");</script>
    <table>
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        <asp:Label runat="server" ID="lblTitle">عظات الاجتماع</asp:Label>
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView SkinID="grd320" AllowPaging="true" PageSize="12" Style="border-collapse: separate; width: 100%" runat="server" 
                    ID="grd" AutoGenerateColumns="False" DataSourceID="odsData" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                     CellPadding="4" PagerSettings-Mode="Numeric"  GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="عنوان العظة">
                            <ItemTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="font-weight: bold"><a style="color: black" href='<%#"MeetingDetails.aspx?mid="+Eval("MeetingId") %>'><%#Eval("MeetingTitle") %></a></td>
                                    </tr>
                                    <tr>
                                        <td style="font-style: italic"><%#Eval("SpeakerName") %> - <%#Eval("ChurchName") %></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="float: left">
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <a target="_blank" href="https://twitter.com/share" class="twitter-share-button" data-url='<%#"http://shepherdmeeting.com/shepherdmeeting.com/MeetingDetails.aspx?mid="+Eval("MeetingId") %>'></a>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="fb-share-button" data-href='<%#"http://shepherdmeeting.com/shepherdmeeting.com/MeetingDetails.aspx?mid="+Eval("MeetingId") %>' data-type="button_count"></div>
                                                    </td>

                                                    <td>
                                                        <div>
                                                            <script type="IN/Share" data-url='<%#"http://shepherdmeeting.com/shepherdmeeting.com/MeetingDetails.aspx?mid="+Eval("MeetingId") %>'></script>
                                                        </div>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="تاريخ العظة" ItemStyle-HorizontalAlign="Center" SortExpression="MeetingDate" DataField="MeetingDate" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="حجم الملف">
                            <ItemTemplate>
                                <span style="direction: rtl"><%# FileSize(Eval("SoundFile").ToString()) %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تحميل الملف" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <table style="margin: auto">
                                    <tr>
                                        <td>
                                            <asp:HyperLink runat="server" NavigateUrl='<%# "files/audio/" +Eval("SoundFile")  %>' ImageUrl="~/themes/Default/img/download22.png" ID="HyperLink1"></asp:HyperLink></td>
                                        <td>
                                            <asp:HyperLink runat="server" ImageUrl="~/themes/Default/img/Sound.png" NavigateUrl='<%# "MeetingDetails.aspx?mid=" +Eval("MeetingId")  %>' ID="lnkVideo"></asp:HyperLink></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                  
                    <HeaderStyle BackColor="#6B696B" Font-Size="12px" Font-Names="ge_ss_twomedium" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                    <PagerStyle  CssClass="Grid-Paging" />
                    <RowStyle BackColor="#F7F7DE" Font-Size="11px" Font-Names="ge_ss_twomedium" />
               
                </asp:GridView>
                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadMeetingsSite"
                    TypeName="DAL.MeetingManage">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="SpeakerId" QueryStringField="speakerId" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>

</asp:Content>
