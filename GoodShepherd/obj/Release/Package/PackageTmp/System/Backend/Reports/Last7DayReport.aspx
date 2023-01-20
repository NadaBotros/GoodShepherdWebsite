<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="Last7DayReport.aspx.cs" Inherits="System.Backend.Reports.Last7DayReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script src="lib/js/highcharts.js" type="text/javascript"></script>
    <script src="lib/js/modules/exporting.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#container').highcharts({
                chart: {
                    type: 'line'
                },
                title: {
                    text: '   '
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: [<asp:Literal id="lblDays" runat="server"></asp:Literal>]
                },
                yAxis: {
                    title: {
                        text: ''
                    }
                },
                tooltip: {
                    enabled: false,
                    formatter: function () {
                        return '<b>' + "الزوار" + '</b><br/>' +
                        this.x + ': ' + this.y + 'Users';
                    }
                },
                plotOptions: {
                    line: {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: true
                    }
                },
                series: [{
                    name: 'Hide',
                    data: [<asp:Literal id="lblVisitDaialy" runat="server"></asp:Literal>]
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
                <img class="imgIcon" src="../lib/icons/24/DialyChart.png" />
                <asp:Label runat="server" Text="زيارات اخر 7 ايام" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="container" class="tablepadding" style="min-width: 400px; height: 400px; margin: 0 auto">
            </td>
        </tr>
    </table>
</asp:Content>
