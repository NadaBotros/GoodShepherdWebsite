<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="MonthlyReport.aspx.cs" Inherits="System.Backend.Reports.MonthlyReport" %>

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
                        text: '  '
                    }
                },
                tooltip: {
                    enabled: false,
                    formatter: function () {
                        return '<b>' + "Users" + '</b><br/>' +
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
                    data: [<asp:Literal id="lblVisitMonthly" runat="server"></asp:Literal>]
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
                <asp:Label runat="server" Text="عدد زوار شهر" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:DropDownList runat="server"  ID="drpYear" CssClass="drplist" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged"
                                EnableTheming="True">
                            </asp:DropDownList>
                            <asp:DropDownList runat="server" ID="drpMonth" CssClass="drplist drpRecords" AutoPostBack="true" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged">
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
