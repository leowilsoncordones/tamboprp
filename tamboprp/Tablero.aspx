<%@ Page Title="tamboprp | tablero" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tablero.aspx.cs" Inherits="tamboprp.Tablero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/ace.css" rel="stylesheet" />
    <link href="css/ace-part2.css" rel="stylesheet" />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <link href="css/ace-fonts.css" rel="stylesheet" />
    <link href="css/chosen.css" rel="stylesheet" />
    <link href="css/ui.jqgrid.css" rel="stylesheet" />
    <link href="css/ace-skins.css" rel="stylesheet" />
    <link href="css/ace-rtl.css" rel="stylesheet" />
    <link href="css/ace-ie.css" rel="stylesheet" />

    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/excanvas.js"></script>
    
    <script src="js/flot/jquery.flot.js"></script>
    <script src="/js/flot/jquery.flot.time.js"></script>
    <script src="/js/flot/jquery.flot.symbol.js"></script>
    <script src="/js/flot/jquery.flot.axislabels.js"></script>
    <script src="/js/flot/jshashtable-3.0.js"></script>
    <script src="/js/flot/jquery.flot.tooltip.js"></script>

    <script src="js/flot/jquery.flot.pie.js"></script>
    <script src="js/flot/jquery.flot.resize.js"></script>
    
    <script src="js/jquery.easypiechart.js"></script>
    <script src="js/jquery.sparkline.js"></script>
    <script src="js/ace/ace.widget-box.js"></script>
    <script src="js/ace/ace.widget-on-reload.js"></script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />  

    <div class="page-header">
        <h1><i class="menu-icon fa fa-tachometer"></i> Tablero <small><i class="ace-icon fa fa-angle-double-right"></i> resumen y estadísticas</small></h1>
    </div>
    
    <!------------------- PRIMER FILA --------------------->
    <div class="row">
        
        <!-- PRIMER FILA - PRIMER COLUMNA -->
        <div class="col-sm-6 infobox-container">
	        <!-- #section:pages/dashboard.infobox -->
            <h4 class="smaller lighter blue"><i class="ace-icon fa fa-gears"></i> Indicadores </h4>

            <div class="infobox infobox-green" id="fVacasOrdene" runat="server" >
	        </div>
            <div class="infobox infobox-orange2" id="fPromLeche" runat="server">
	        </div>
	        <div class="infobox infobox-orange" id="fLecheUltControl" runat="server">
	        </div>
            <div class="infobox infobox-light-brown" id="fPromDiasLact" runat="server">
	        </div>
	        <div class="infobox infobox-red" id="fAbortos" runat="server">
	        </div>
            <div class="infobox infobox-blue" id="fPartos" runat="server">
	        </div>
            <!-- /section:pages/dashboard.infobox -->
        
            <div class="space-6"></div>    
	        
	        <!-- #section:pages/dashboard.infobox.dark -->
            <div class="infobox infobox-green infobox-small infobox-dark" id="fNacidos" runat="server">
	        </div>
	        <div class="infobox infobox-blue infobox-small infobox-dark" id="fPrenadas" runat="server">
	        </div>
            <div class="infobox infobox-grey infobox-small infobox-dark" id="fToroMasUsado" runat="server">
	        </div>    
	        <!-- /section:pages/dashboard.infobox.dark -->
        </div>
        
        <!-- PRIMER FILA - SEGUNDA COLUMNA -->
        <div class="col-sm-6">
            <div class="widget-box transparent">
			    <div class="widget-header widget-header-flat">
				    <h4 class="widget-title lighter"><i class="ace-icon fa fa-signal"></i> Producción de leche del último año</h4>
                    <div class="widget-toolbar no-border">
					<div class="inline">
					    <a href="GraficasProd.aspx" class="btn btn-minier btn-white btn-default">
                            <i class="ace-icon fa fa-bar-chart-o icon-on-right bigger-110"></i>&nbsp;Gráficas
                        </a>
					</div>
				</div>
			    </div>
                <div class="widget-body">
				    <div class="widget-main padding-4">
					    <div id="sales-charts" class="col-md-12">
					    </div>
				    </div>
                </div>						    
            </div>
        </div>

    </div>
    <!------------------- FIN PRIMER FILA --------------------->
        
    <div class="space-6"></div>
    <hr />
    
    <!------------------- SEGUNDA FILA --------------------->
    <div class="row">
        
        <!-- SEGUNDA FILA - PRIMER COLUMNA -->
        <div class="col-sm-6" id="fAlertas" runat="server">
            <!-- #section:alertas y notificaciones -->
        </div>

        <!-- SEGUNDA FILA - SEGUNDA COLUMNA -->
        <div class="col-sm-6">
            <div class="widget-box">
			<div class="widget-header widget-header-flat widget-header-small">
				<h5 class="widget-title"><i class="ace-icon fa fa-signal"></i>Categorías</h5>
                <div class="widget-toolbar no-border">
					<div class="inline">
					    <a href="ListPorCategoria.aspx" class="btn btn-minier btn-white btn-default">
                            <i class="ace-icon fa fa-list icon-on-right bigger-110"></i>&nbsp;Listados
                        </a>
					</div>
				</div>
			</div>
			<div class="widget-body">
				<div class="widget-main">
					<!-- #section:plugins/charts.flotchart -->
					<div id="piechart-placeholder" style="width: 90%; min-height: 150px; padding: 0px; position: relative;">
					</div>

					<!-- /section:plugins/charts.flotchart -->
					<div class="hr hr8 hr-double"></div>
					<div class="clearfix" id="fExtraGrid" runat="server">
						<!-- #section:custom/extra.grid -->
					</div>
				</div><!-- /.widget-main -->
			</div><!-- /.widget-body -->
		    </div>
        </div>

     </div>
     <!------------------- FIN SEGUNDA FILA --------------------->
    
    <div class="space-6"></div>
   

    <!------------- Script para grafica de puntos --------------->
    <script type="text/javascript">


        $(document).ready(function () {
            GetValoreLeche();

        });

        function gd1(date) {
            return new Date(date).getTime();
        }

        function GetValoreLeche() {
            PageMethods.ControlTotalGetAll(OnSuccess);
        }

        function OnSuccess(response) {
            var leche3 = [];
            var list = response;
            for (var i = 0; i < list.length; i++) {
                leche3.push([gd1(list[i].Fecha), list[i].Leche]);
            }
            imprimir(leche3);

        }


        var sales_charts = $('#sales-charts').css({ 'height': '260px' });
        function imprimir(totalLeche) {

            //var tick = Math.round(totalLeche.length / 10);
            $.plot("#sales-charts", [
                { label: "Leche", data: totalLeche }
                //{ label: "Grasa", data: grasa }

            ], {
                hoverable: true,
                shadowSize: 0,
                series: {
                    lines: { show: true },
                    points: { show: true }
                    /*points: {
                        show: true,
                        fill: true
                    }*/
                },
                xaxis: {
                    tickLength: 0,
                    mode: "time",
                    timeformat: "%m/%Y",
                    tickSize: [1, "month"]
                    //tickSize: [tick, "month"]
                },
                yaxis: {
                    ticks: 10,
                    min: 0,
                    max: 20000,
                    tickDecimals: 0
                },
                grid: {
                    backgroundColor: { colors: ["#fff", "#fff"] },
                    borderWidth: 1,
                    borderColor: '#555',
                    hoverable: true
                },
                tooltip: true,
                tooltipOpts: {
                    content: "<h6><strong>Fecha:</strong> %x</h6><h6><strong>%s:</strong> %y lts</h6>",
                }
            });

        };


        // Grafica de torta

        //jQuery(function($) {
        //    $('.easy-pie-chart.percentage').each(function() {
        //        var $box = $(this).closest('.infobox');
        //        var barColor = $(this).data('color') || (!$box.hasClass('infobox-dark') ? $box.css('color') : 'rgba(255,255,255,0.95)');
        //        var trackColor = barColor == 'rgba(255,255,255,0.95)' ? 'rgba(255,255,255,0.25)' : '#E2E2E2';
        //        var size = parseInt($(this).data('size')) || 50;
        //        $(this).easyPieChart({
        //            barColor: barColor,
        //            trackColor: trackColor,
        //            scaleColor: false,
        //            lineCap: 'butt',
        //            lineWidth: parseInt(size / 10),
        //            animate: /msie\s*(8|7|6)/.test(navigator.userAgent.toLowerCase()) ? false : 1000,
        //            size: size
        //        });
        //    });
			
            $('.sparkline').each(function(){
                var $box = $(this).closest('.infobox');
                var barColor = !$box.hasClass('infobox-dark') ? $box.css('color') : '#FFF';
                $(this).sparkline('html',
                                 {
                                     tagValuesAttribute:'data-values',
                                     type: 'bar',
                                     barColor: barColor ,
                                     chartRangeMin:$(this).data('min') || 0
                                 });
            });
			
			
            //flot chart resize plugin, somehow manipulates default browser resize event to optimize it!
            //but sometimes it brings up errors with normal resize event handlers
            $.resize.throttleWindow = false;
			
            var placeholder = $('#piechart-placeholder').css({'width':'80%' , 'min-height':'150px'});
            var data = [
                { label: "en ordeñe", data: 661, color: "#68BC31" },
                { label: "entoradas", data: 212, color: "#FEE074" },
                { label: "secas", data: 137, color: "#DA5430" },
                { label: "vaquillonas", data: 169, color: "#AF4E96" },
                { label: "terneras", data: 383, color: "#2091CF" }
            ];
            function drawPieChart(placeholder, data, position) {
                $.plot(placeholder, data, {
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
                        margin: [-30, 15]
                    },
                    grid: {
                        hoverable: true,
                        clickable: true
                    }
                });
            }
            drawPieChart(placeholder, data);
			
            /**
            we saved the drawing function and the data to redraw with different position later when switching to RTL mode dynamically
            so that's not needed actually.
            */
            placeholder.data('chart', data);
            placeholder.data('draw', drawPieChart);
			
			
            //pie chart tooltip example
            var $tooltip = $("<div class='tooltip top in'><div class='tooltip-inner'></div></div>").hide().appendTo('body');
            var previousPoint = null;
			
            placeholder.on('plothover', function (event, pos, item) {
                if(item) {
                    if (previousPoint != item.seriesIndex) {
                        previousPoint = item.seriesIndex;
                        var tip = item.series['label'] + " : " + Math.round(item.series['percent'])+'%';
                        $tooltip.show().children(0).text(tip);
                    }
                    $tooltip.css({top:pos.pageY + 10, left:pos.pageX + 10});
                } else {
                    $tooltip.hide();
                    previousPoint = null;
                }
				
            });

    </script>
    

</asp:Content>
