<%@ Page Title="tamboprp | remitos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Remitos.aspx.cs" Inherits="tamboprp.Remitos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-truck"></i> Remitos a planta</h1>
    </div>
    
    <!-- comienza tabbable -->
    <div class="tabbable">
		<ul class="nav nav-tabs" id="myTab">
			<li class="active"><a data-toggle="tab" href="#listadoRemitos"><i class="blue ace-icon fa fa-list bigger-120"></i> Listado </a></li>
            <li><a data-toggle="tab" href="#graficaRemitos"><i class="blue ace-icon fa fa-bar-chart-o bigger-120"></i> Gráfica </a></li>
		</ul>
        <!-- comienza contenido de tabbable -->
		<div class="tab-content">
		    
		    <!-- PESTANA 1: LISTADO DE REMITOS -->
			<div id="listadoRemitos" class="tab-pane fade in active">
			    <div class="page-header">
                    <h1><i class="menu-icon fa fa-list"></i> Listado de remitos a planta</h1>
                </div>
                <div class="row">
                <div class="col-md-10">
					<h4 class="smaller">
					    <asp:Label ID="titListado" Text="Empresa: " runat="server" ></asp:Label>
                        <asp:Label ID="lblTituloListado" Text="" runat="server" ></asp:Label>
					</h4>
                    <!-- GRIDVIEW -->
                    <asp:GridView ID="gvRemitos" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                            CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                        <RowStyle HorizontalAlign="Left"  />
                        <Columns>
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Factura" HeaderText="Factura" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />   
                            <asp:BoundField DataField="Matricula" HeaderText="Matricula" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
                            <asp:BoundField DataField="Litros" HeaderText="Litros" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Encargado" HeaderText="Encargado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Temp_1" HeaderText="Temp_1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Temp_2" HeaderText="Temp_2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <FooterStyle />
                        <PagerStyle HorizontalAlign="Left" />
                        <SelectedRowStyle />
                        <HeaderStyle />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                </div>
                <div class="col-md-2">
                </div>
                </div>
			</div> <!-- fin LISTADO de remitos -->
            
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" /> 

            <!-- PESTANA 2: GRAFICA DE REMITOS -->
			<div id="graficaRemitos" class="tab-pane fade">
			    <div class="page-header">
			        <h1><i class="menu-icon fa fa-bar-chart-o"></i> Gráfica de remitos a planta</h1>
                </div>
                <h4 class="smaller">
                        <asp:Label ID="titGrafica" Text="Empresa: " runat="server" ></asp:Label>
                        <asp:Label ID="lblTituloGrafica" Text="" runat="server" ></asp:Label>
                    </h4>
			    <div class="row">
					<!-- GRAFICA -->
                    
                    <div class="col-md-9" id="grafRemitos"></div>
                    <div class="col-md-3">
                        <div class="well">
                        <h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i>Fechas</h4>
                        <div class="row">
                            <div class="input-group">
                                <span class="input-group-btn">
                                    <asp:Button ID="btnListar" runat="server" Text="Mostrar" onclick="btnListar_Click" CssClass="btn btn-white btn-default" />
                                </span>
                                <asp:DropDownList ID="ddlFechasGraf" cssClass="form-control" runat="server"  AutoPostBack="False"></asp:DropDownList>
                            </div>
                        </div>
                        <hr/>
                        <div class="row">
                    
                            <asp:Panel ID="pnlFechasGraf" runat="server" class="input-group">
                                <h5 class="header smaller lighter blue">Entre dos fechas</h5> 
                                <div class="input-group">                 
                                <span class="input-group-btn">                   
                                <input type="button" onclick="cargarGraficas(document.getElementById('fechasGraficas').value)" value="Mostrar" class="btn btn-white btn-default"/>
                                </span>
                                <input type="text" id="fechasGraficas" placeholder="Fechas" class="form-control"/>  
                                    </div>                			                         
                            </asp:Panel>                   
                        </div> 
                        </div> 
                    </div>
                  </div>
                    <script src="js/ace-extra.js" ></script>
                <script src="js/date-time/moment.js"></script>
               <script src="js/date-time/daterangepicker.js"></script>

                    <link href="css/daterangepicker.css" rel="stylesheet" />
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
                    <script type="text/javascript">


                        $("#fechasGraficas").daterangepicker({
                            locale: {
                                applyLabel: 'Confirma',
                                cancelLabel: 'Cancela',
                                fromLabel: 'Desde',
                                toLabel: 'Hasta',
                            }
                        });

                        $(document).ready(function() {
                           // GetValoreLeche();          
                        });

                        function gd1(date) {
                            return new Date(date).getTime();
                        }

                        function GetValoreLeche(data) {
                            if (data == 0) PageMethods.RemitosGetAnioCorriente(OnSuccess);
                            if (data == 1) PageMethods.RemitosGetUltimoAnio(OnSuccess);
                        }

                        function OnSuccess(response){
                            var leche3 = [];
                        var list = response;
                            for (var i = 0; i < list.length; i++) {
                                leche3.push([gd1(list[i].Fecha), list[i].Leche]);
                            }
                            imprimir(leche3);
            
                        }

                        var grafRemitos = $('#grafRemitos').css({
                            'height': '360px' , 'width': '800px'  //tengo que ponerle el ancho porque queda de 83px ¿?
                        });
                        function imprimir(totalLeche) {
        
                            var tick = Math.round(totalLeche.length / 10);
                            $.plot("#grafRemitos", [
                            { label: "Leche", data: totalLeche }

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
                                tickSize: [tick, "month"]
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
                            },
                            colors: ["#008115"]
                        });

                        }

                        function cargarGraficas(data) {

                            var res = data.split(" - ");
                            var fecha1 = formatoFecha(res[0]);
                            var fecha2 = formatoFecha(res[1]);
                            PageMethods.GetRemitosEntreDosFechas(fecha1, fecha2, OnSuccess);
                        };

                        function formatoFecha(fecha) {
                            var res = fecha.split("/");
                            var salida = "";
                            salida = res[2] + "-" + res[0] + "-" + res[1];
                            return salida;
                        }


                    </script>
                    <script src="js/html5shiv.js"></script>
                    <script src="js/respond.js"></script>
                    <script src="js/jquery.js"></script>
                    <script src="js/jquery1x.js"></script>
                    <script src="js/bootstrap.js"></script>
                    <script src="js/excanvas.js"></script>
                    <script src="js/flot/jquery.flot.js"></script>
                    <script src="/js/flot/jquery.flot.time.js"></script>
                    <script src="/js/flot/jquery.flot.symbol.js"></script>
                    <script src="/js/flot/jquery.flot.axislabels.js"></script>
                    <script src="/js/flot/jshashtable-3.0.js"></script>
                    <script src="/js/flot/jquery.flot.tooltip.js"></script>
                    
                
            </div> <!-- fin GRAFICA de remitos -->
		</div>
	</div> <!-- fin tabbable -->

</asp:Content>
