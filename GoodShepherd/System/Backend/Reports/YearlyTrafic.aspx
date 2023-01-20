<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="YearlyTrafic.aspx.cs" Inherits="System.Backend.Reports.YearlyTrafic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script src="lib/js/highcharts.js" type="text/javascript"></script>
    <script src="lib/js/modules/exporting.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#container').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: '  '
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: [
                    'يناير',
                    'فبراير',
                    'مارس',
                    'ابريل',
                    'مايو',
                    'يونيو',
                    'يوليو',
                    'اغسطس',
                    'سبتمبر',
                    'اكتوبر',
                    'نوفمبر',
                    'ديسمبر'
                ]
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'عدد الزارات '
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0"> </td>' +
                    '<td style="padding:0"><b>{point.y:.1f} User</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series: [{
                    name: 'الشهر',
                    data: [<asp:Literal id="lblNuberinMonth" runat="server"></asp:Literal>]
                }]
            });
        });
		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img class="imgIcon" src="../lib/icons/32/Reports.PNG" /><asp:Label runat="server"
        Text="احصائيات الزوار" CssClass="tdMainTitle" ID="Label1"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/ChartReport.png" />
                <asp:Label runat="server" Text="احصائيات زوار سنة" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:DropDownList runat="server" CssClass="drplist" ID="drpYear" AutoPostBack="true"
                                OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div id="container" class="tablepadding" style="min-width: 400px; height: 400px; margin: 0 auto">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
