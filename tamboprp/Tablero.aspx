<%@ Page Title="tamboprp | tablero" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tablero.aspx.cs" Inherits="tamboprp.Tablero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <link href="css/ace-fonts.css" rel="stylesheet" />
    <link href="css/chosen.css" rel="stylesheet" />
    <link href="css/ui.jqgrid.css" rel="stylesheet" />
    <link href="css/ace.css" rel="stylesheet" />
    <link href="css/ace-part2.css" rel="stylesheet" />
    <link href="css/ace-skins.css" rel="stylesheet" />
    <link href="css/ace-rtl.css" rel="stylesheet" />
    <link href="css/ace-ie.css" rel="stylesheet" />
    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/excanvas.js"></script>
    
    <script type='text/javascript'>
    var data = [
    { label: "social networks", data: 38.7, color: "#68BC31" },
    { label: "search engines", data: 24.5, color: "#2091CF" },
    { label: "ad campaigns", data: 8.2, color: "#AF4E96" },
    { label: "direct traffic", data: 18.6, color: "#DA5430" },
    { label: "other", data: 10, color: "#FEE074" }
        ]

        $('#piechart-placeholder').css({ 'width': '90%', 'min-height': '150px' });
    var my_chart = $.plot('#piechart-placeholder', data, {
        series: {
            pie: {
                show: true,
                tilt: 0.8,
                highlight: {
                    opacity: 0.25
                },
                stroke: {
                    color: '#fff',
                    width: 2
                },
                startAngle: 2
            }
        },
        legend: {
            show: true,
            position: position || "ne",
            labelBoxBorderColor: null,
            margin: [-30, 15] //some offsetting
        },
        grid: {
            hoverable: true,
            clickable: true
        }
    });

    var $tooltip = $("<div class='tooltip top in'><div class='tooltip-inner'></div></div>").hide().appendTo('body');

    var lastIndex = null;
    $('#piechart-placeholder').on('plothover', function (event, pos, item) {
        if (item) {
            if (lastIndex != item.seriesIndex) {
                lastIndex = item.seriesIndex;
                var tooltip_text = item.series['label'] + " : " + item.series['percent'] + '%';
                $tooltip.show().children(0).text(tooltip_text);
                //or
                //$tooltip.find('.tooltip-inner').text(tooltip_text);
            }
            $tooltip.css({ top: pos.pageY + 10, left: pos.pageX + 10 });
        } else {
            $tooltip.hide();
            lastIndex = null;
        }
    });


    $('.spark').sparkline('html',
    {
        tagValuesAttribute: 'data-values',//the attribute which has data
        type: 'bar',
        barColor: '#FF0000',
        chartRangeMin: 0
    }
    );


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <div id="piechart-placeholder" style="width: 90%; min-height: 150px; padding: 0px; position: relative;">
        <canvas class="flot-base" width="351" height="150" style="direction: ltr; position: absolute; left: 0px; top: 0px; width: 351px; height: 150px;"></canvas>
        <canvas class="flot-overlay" width="351" height="150" style="direction: ltr; position: absolute; left: 0px; top: 0px; width: 351px; height: 150px;"></canvas>
        <div class="legend">
            <div style="position: absolute; width: 90px; height: 110px; top: 15px; right: -30px; opacity: 0.85; background-color: rgb(255, 255, 255);"> </div>
            <table style="position:absolute;top:15px;right:-30px;;font-size:smaller;color:#545454">
            <tbody>
                <tr>
                    <td class="legendColorBox">
                        <div style="border:1px solid null;padding:1px">
                            <div style="width:4px;height:0;border:5px solid #68BC31;overflow:hidden"></div>
                        </div>
                    </td>
                    <td class="legendLabel">social networks</td>
                </tr>
                <tr>
                    <td class="legendColorBox">
                        <div style="border:1px solid null;padding:1px">
                            <div style="width:4px;height:0;border:5px solid #2091CF;overflow:hidden"></div>
                        </div>
                    </td>
                <td class="legendLabel">search engines</td>
                </tr>
                <tr>
                    <td class="legendColorBox">
                        <div style="border:1px solid null;padding:1px">
                            <div style="width:4px;height:0;border:5px solid #AF4E96;overflow:hidden"></div>
                        </div>
                    </td><td class="legendLabel">ad campaigns</td>
                </tr>
                <tr>
                    <td class="legendColorBox">
                        <div style="border:1px solid null;padding:1px">
                            <div style="width:4px;height:0;border:5px solid #DA5430;overflow:hidden"></div>
                        </div>
                    </td><td class="legendLabel">direct traffic</td>
                </tr>
                <tr>
                    <td class="legendColorBox">
                        <div style="border:1px solid null;padding:1px">
                            <div style="width:4px;height:0;border:5px solid #FEE074;overflow:hidden"></div>
                        </div>
                    </td><td class="legendLabel">other</td>
                </tr>
            </tbody>
        </table>
        </div>
    </div>

</asp:Content>
