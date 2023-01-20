<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="BrowserTrafic.aspx.cs" Inherits="System.Backend.Reports.BrowserTrafic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script src="lib/js/highcharts.js" type="text/javascript"></script>
    <script src="lib/js/modules/exporting.js" type="text/javascript"></script>
    <script type="text/javascript">
		    $(function () {
		        $('#container').highcharts({
		            chart: {
		                plotBackgroundColor: null,
		                plotBorderWidth: null,
		                plotShadow: false
		            },
		            title: {
		                text: '   '
		            },
		            tooltip: {
		                pointFormat: '{series.name}: <b>{point.percentage:0f}%</b>',
		                percentageDecimals: 2
		            },
		            plotOptions: {
		                pie: {
		                    allowPointSelect: true,
		                    cursor: 'pointer',
		                    dataLabels: {
		                        enabled: true,
		                        color: '#000000',
		                        connectorColor: '#000000',
		                        formatter: function () {
		                            return '<b>' + this.point.name + '</b>: ' + this.percentage + ' %';
		                        }
		                    }
		                }
		            },
		            series: [{
		                type: 'pie',
		                name: 'Browser share',
		                data: [<asp:Repeater id="rptbrowser" runat="server"> <ItemTemplate>
                  ['<%#Eval("BrowserName")%> ',<%#Eval("Visits")%> ],</ItemTemplate> </asp:Repeater>
                   
                ]
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
                <img class="imgIcon" src="../lib/icons/24/Radiochart.png" />
                <asp:Label runat="server" Text="متصفح زوار الموقع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="container" class="tablepadding" style="min-width: 400px; height: 400px; margin: 0 auto">
            </td>
        </tr>
    </table>
</asp:Content>
