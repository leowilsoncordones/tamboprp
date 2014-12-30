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

    <script src="js/flot/jquery.flot.pie.js"></script>
    <script src="js/flot/jquery.flot.resize.js"></script>
    
    <script src="js/jquery.easypiechart.js"></script>
    
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
        <h4 class="smaller lighter blue"><i class="ace-icon fa fa-gears"></i> Producción</h4>

        <div class="infobox infobox-green">
		    <div class="infobox-icon"><i class="ace-icon fa fa-gears"></i></div>
		    <div class="infobox-data">
		        <span class="infobox-data-number">661</span>
                <div class="infobox-content">vacas en ordeñe</div>
		    </div>
            <!-- #section:pages/dashboard.infobox.stat -->
		    <div class="stat stat-success">8%</div>
            <!-- /section:pages/dashboard.infobox.stat -->
	    </div>
        
        <div class="infobox infobox-orange">
		    <div class="infobox-icon"><i class="ace-icon fa fa-tint"></i></div>
            <div class="infobox-data">
			    <span class="infobox-data-number">24,31 <small>lts</small></span>
			    <div class="infobox-content">promedio leche</div>
		    </div>
	    </div>

	    <div class="infobox infobox-orange2">
		    <!-- #section:pages/dashboard.infobox.sparkline -->
		    <div class="infobox-chart">
			    <span class="sparkline" data-values="196,128,202,177,154,94,100,170,224">
			        <canvas width="44" height="27" style="display: inline-block; width: 44px; height: 27px; vertical-align: top;"></canvas>
			    </span>
		    </div>
            <!-- /section:pages/dashboard.infobox.sparkline -->
		    <div class="infobox-data">
			    <span class="infobox-data-number">26.251 <small>lts</small></span>
			    <div class="infobox-content">leche último control</div>
		    </div>
	    </div>

	    <div class="infobox infobox-blue2">
		    <div class="infobox-progress">
			    <!-- #section:pages/dashboard.infobox.easypiechart -->
			    <div class="easy-pie-chart percentage" data-percent="42" data-size="46" style="height: 46px; width: 46px; line-height: 45px;">
				    <span class="percent">42</span>%
			    <canvas height="46" width="46"></canvas></div>
			    <!-- /section:pages/dashboard.infobox.easypiechart -->
		    </div>
            <div class="infobox-data">
			    <span class="infobox-text">traffic used</span>
                <div class="infobox-content">
				    <span class="bigger-110">~</span>
				    58GB remaining
			    </div>
		    </div>
	    </div>

	    <!-- /section:pages/dashboard.infobox -->
	    <!-- #section:pages/dashboard.infobox.dark -->
	    <div class="infobox infobox-green infobox-small infobox-dark">
		    <div class="infobox-progress">
			    <!-- #section:pages/dashboard.infobox.easypiechart -->
			    <div class="easy-pie-chart percentage" data-percent="61" data-size="39" style="height: 39px; width: 39px; line-height: 38px;">
				    <span class="percent">61</span>%
			    <canvas height="39" width="39"></canvas></div>
                <!-- /section:pages/dashboard.infobox.easypiechart -->
		    </div>

		    <div class="infobox-data">
			    <div class="infobox-content">Task</div>
			    <div class="infobox-content">Completion</div>
		    </div>
	    </div>

	    <div class="infobox infobox-blue infobox-small infobox-dark">
		    <!-- #section:pages/dashboard.infobox.sparkline -->
		    <div class="infobox-chart">
			    <span class="sparkline" data-values="3,4,2,3,4,4,2,2">
			        <canvas width="39" height="16" style="display: inline-block; width: 39px; height: 16px; vertical-align: top;"></canvas>
			    </span>
		    </div>
            <!-- /section:pages/dashboard.infobox.sparkline -->
		    <div class="infobox-data">
			    <div class="infobox-content">Remito</div>
			    <div class="infobox-content">15.233 lts</div>
		    </div>
	    </div>

	    <div class="infobox infobox-grey infobox-small infobox-dark">
		    <div class="infobox-icon"><i class="ace-icon fa fa-download"></i></div>
            <div class="infobox-data">
			    <div class="infobox-content">Downloads</div>
			    <div class="infobox-content">1,205</div>
		    </div>
	    </div>
	    <!-- /section:pages/dashboard.infobox.dark -->
        </div>
        
        <!-- PRIMER FILA - SEGUNDA COLUMNA -->
        <div class="col-sm-6">
            <div class="widget-box transparent">
			    <div class="widget-header widget-header-flat">
				    <h4 class="widget-title lighter"><i class="ace-icon fa fa-signal"></i> Producción de leche año actual</h4>
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
        
        <div class="space-6"></div>
        <!------------------- SEGUNDA FILA --------------------->

        <div class="row">
        
        <!-- SEGUNDA FILA - PRIMER COLUMNA -->
        <div class="col-sm-6 infobox-container">
	    <!-- #section:pages/dashboard.infobox -->
        <h4 class="smaller lighter blue"><i class="ace-icon fa fa-flask"></i> Reproducción</h4>

	    <div class="infobox infobox-blue">
		    <div class="infobox-icon"><i class="ace-icon fa fa-tag"></i></div>
            <div class="infobox-data">
			    <span class="infobox-data-number">11</span>
			    <div class="infobox-content">partos este mes</div>
		    </div>
		    <div class="stat stat-important">17%</div>
	    </div>
        
        <div class="infobox infobox-red">
		    <div class="infobox-icon"><i class="ace-icon fa fa-thumbs-o-down"></i></div>
            <div class="infobox-data">
			    <span class="infobox-data-number">7%</span>
			    <div class="infobox-content">índice de aborto</div>
		    </div>
	    </div>

        <div class="infobox infobox-light-brown">
		    <div class="infobox-icon"><i class="ace-icon fa fa-binoculars"></i></div>
            <div class="infobox-data">
			    <span class="infobox-data-number">9</span>
			    <div class="infobox-content">lact sin servicio (80d)</div>
		    </div>
		    <div class="badge badge-success">+12%<i class="ace-icon fa fa-arrow-up"></i></div>
	    </div>

	    <div class="infobox infobox-pink">
	        <div class="infobox-icon"><i class="ace-icon fa fa-flask"></i></div>
            <div class="infobox-data">
			    <span class="infobox-data-number">8</span>
			    <div class="infobox-content">servicios sin diag (70d)</div>
		    </div>
		    <div class="stat stat-important">4%</div>
	    </div>

	    <div class="infobox infobox-orange2">
		    <!-- #section:pages/dashboard.infobox.sparkline -->
		    <div class="infobox-chart">
			    <span class="sparkline" data-values="196,128,202,177,154,94,100,170,224">
			        <canvas width="44" height="27" style="display: inline-block; width: 44px; height: 27px; vertical-align: top;"></canvas>
			    </span>
		    </div>
            <!-- /section:pages/dashboard.infobox.sparkline -->
		    <div class="infobox-data">
			    <span class="infobox-data-number">6,251</span>
			    <div class="infobox-content">pageviews</div>
		    </div>
            <div class="badge badge-success">7.2%<i class="ace-icon fa fa-arrow-up"></i></div>
	    </div>

	    <div class="infobox infobox-blue2">
		    <div class="infobox-progress">
			    <!-- #section:pages/dashboard.infobox.easypiechart -->
			    <div class="easy-pie-chart percentage" data-percent="42" data-size="46" style="height: 46px; width: 46px; line-height: 45px;">
				    <span class="percent">42</span>%
			    <canvas height="46" width="46"></canvas></div>
			    <!-- /section:pages/dashboard.infobox.easypiechart -->
		    </div>
            <div class="infobox-data">
			    <span class="infobox-text">traffic used</span>
                <div class="infobox-content">
				    <span class="bigger-110">~</span>
				    58GB remaining
			    </div>
		    </div>
	    </div>
        </div>
        <!-- SEGUNDA FILA - SEGUNDA COLUMNA -->
        <div class="col-sm-6">
            
            
            <div class="widget-box">
			<div class="widget-header widget-header-flat widget-header-small">
				<h5 class="widget-title"><i class="ace-icon fa fa-signal"></i>Categorías</h5>
                <div class="widget-toolbar no-border">
					<div class="inline dropdown-hover">
						<button class="btn btn-minier btn-primary">This Week<i class="ace-icon fa fa-angle-down icon-on-right bigger-110"></i></button>
                        <ul class="dropdown-menu dropdown-menu-right dropdown-125 dropdown-lighter dropdown-close dropdown-caret">
							<li class="active"><a href="#" class="blue"><i class="ace-icon fa fa-caret-right bigger-110">&nbsp;</i>This Week</a></li>
                            <li><a href="#"><i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>Last Week</a></li>
							<li><a href="#"><i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>This Month</a></li>
                            <li><a href="#"><i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>Last Month</a></li>
						</ul>
					</div>
				</div>
			</div>
			<div class="widget-body">
				<div class="widget-main">
					<!-- #section:plugins/charts.flotchart -->
					<div id="piechart-placeholder" style="width: 90%; min-height: 150px; padding: 0px; position: relative;">
					    <canvas class="flot-base" width="351" height="150" style="direction: ltr; position: absolute; left: 0px; top: 0px; width: 351px; height: 150px;"></canvas>
                        <canvas class="flot-overlay" width="351" height="150" style="direction: ltr; position: absolute; left: 0px; top: 0px; width: 351px; height: 150px;"></canvas>
                        <div class="legend"><div style="position: absolute; width: 90px; height: 110px; top: 15px; right: -30px; opacity: 0.85; background-color: rgb(255, 255, 255);"> </div>
                            <table style="position:absolute;top:15px;right:-30px;;font-size:smaller;color:#545454">
                                <tbody>
                                    <tr><td class="legendColorBox"><div style="border:1px solid null;padding:1px"><div style="width:4px;height:0;border:5px solid #68BC31;overflow:hidden"></div></div></td><td class="legendLabel">en ordeñe</td></tr>
                                    <tr><td class="legendColorBox"><div style="border:1px solid null;padding:1px"><div style="width:4px;height:0;border:5px solid #2091CF;overflow:hidden"></div></div></td><td class="legendLabel">vaquillonas</td></tr>
                                    <tr><td class="legendColorBox"><div style="border:1px solid null;padding:1px"><div style="width:4px;height:0;border:5px solid #AF4E96;overflow:hidden"></div></div></td><td class="legendLabel">entoradas</td></tr>
                                    <tr><td class="legendColorBox"><div style="border:1px solid null;padding:1px"><div style="width:4px;height:0;border:5px solid #DA5430;overflow:hidden"></div></div></td><td class="legendLabel">terneras</td></tr>
                                    <tr><td class="legendColorBox"><div style="border:1px solid null;padding:1px"><div style="width:4px;height:0;border:5px solid #FEE074;overflow:hidden"></div></div></td><td class="legendLabel">otros</td></tr>
                                </tbody>
                            </table>
                        </div>
					</div>

					<!-- /section:plugins/charts.flotchart -->
					<div class="hr hr8 hr-double"></div>
					<div class="clearfix">
						<!-- #section:custom/extra.grid -->
						<div class="grid3">
							<span class="grey"><i class="ace-icon fa fa-filter fa-2x orange"></i>&nbsp; Secas</span>
                            <h4 class="bigger pull-right">31</h4>
						</div>
                        <div class="grid3">
							<span class="grey"><i class="ace-icon fa fa-stethoscope fa-2x red"></i>&nbsp; Muertes</span>
							<h4 class="bigger pull-right">6</h4>
						</div>
                        <div class="grid3">
							<span class="grey"><i class="ace-icon fa fa-money fa-2x green"></i>&nbsp; Ventas</span>
							<h4 class="bigger pull-right">13</h4>
						</div>

						<!-- /section:custom/extra.grid -->
					</div>
				</div><!-- /.widget-main -->
			</div><!-- /.widget-body -->
		</div>


        </div>
    </div>
    
    <div class="space-6"></div>
    <!------------------- TERCER FILA --------------------->

    <div class="row">
        <!-- TERCER FILA - PRIMERA COLUMNA -->
        <div class="col-sm-4">
            <h4 class="smaller lighter blue"><i class="ace-icon fa fa-eye"></i> Otros indicadores de interés</h4>
            <h6 class="smaller"> Tasa de concepción promedio <strong>26%</strong></h6>
            <div id="progressbar0" class="ui-progressbar ui-widget ui-widget-content ui-corner-all progress progress-striped active" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="26">
                <div class="ui-progressbar-value ui-widget-header ui-corner-left progress-bar progress-bar-warning" style="width: 26%;"></div>
            </div>
            <h6 class="smaller"> Intervalo PPC <strong>67%</strong></h6>
            <div id="progressbar1" class="ui-progressbar ui-widget ui-widget-content ui-corner-all progress progress-striped active" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="67">
                <div class="ui-progressbar-value ui-widget-header ui-corner-left progress-bar progress-bar-success" style="width: 67%;"></div>
            </div>
            <h6 class="smaller"> Indice de abortos <strong>8%</strong></h6>
            <div id="progressbar2" class="ui-progressbar ui-widget ui-widget-content ui-corner-all progress progress-striped active" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="8">
                <div class="ui-progressbar-value ui-widget-header ui-corner-left progress-bar progress-bar-danger" style="width: 8%;"></div>
            </div>
        </div>
        <div class="col-sm-2"></div>
        <!-- TERCER FILA - SEGUNDA COLUMNA -->
        <div class="col-sm-6">
            <h4 class="smaller lighter blue"><i class="ace-icon fa fa-bell-o"></i> Alertas y notificaciones</h4>
			<div class="alert alert-danger">
				<button class="close" data-dismiss="alert"><i class="ace-icon fa fa-times"></i></button>
                <strong>5</strong> vacas con 70 días de servicio y sin diagnóstico!
			</div>
            <div class="alert alert-warning">
				<button class="close" data-dismiss="alert"><i class="ace-icon fa fa-times"></i></button>
                <strong>16</strong> vacas con 35 días de servicio y sin diagnóstico!
			</div>
            <div class="alert alert-info">
				<button class="close" data-dismiss="alert"><i class="ace-icon fa fa-times"></i></button>
                <strong> 9</strong> vacas con 80 días o más en lactancia y sin servicio!
			</div>
        </div>
    </div>
    
    <div class="space-6"></div> 
    <!------------------- CUARTA FILA --------------------->

    <div class="row">
        <div class="col-sm-12">
        </div>
        <!-- CUARTA FILA - PRIMERA COLUMNA -->
        <div class="col-sm-6">
            
        </div>
        <!-- CUARTA FILA - SEGUNDA COLUMNA -->
        <div class="col-sm-6">
            
        </div>
    </div>
    
    
    <!-- Script para grafica de puntos -->
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


        var sales_charts = $('#sales-charts').css({ 'height': '200px' });
        function imprimir(totalLeche) {

            $.plot("#sales-charts", [
                { label: "Leche", data: totalLeche }
                //{ label: "Grasa", data: grasa }

            ], {
                hoverable: true,
                shadowSize: 0,
                series: {
                    lines: { show: true },
                    points: { show: true }
                },
                xaxis: {
                    tickLength: 0,
                    mode: "time",
                    timeformat: "%Y/%m",
                    tickSize: [2, "month"]
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
                }
            });

        }


    </script>
    
    


</asp:Content>
