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
                    
                    <div class="col-md-10" id="grafRemitos"></div>
                    <div class="col-md-2"></div>

                    <script src="js/ace-extra.js" ></script>
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


                        $(document).ready(function() {
                            GetValoreLeche();          
                        });

                        function gd1(date) {
                            return new Date(date).getTime();
                        }

                        function GetValoreLeche() {
                            PageMethods.RemitosGraficasGetAll(OnSuccess);
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
                            'height': '360px' , 'width': '720px'  //tengo que ponerle el ancho porque queda de 83px ¿?
                        });
                        function imprimir(totalLeche) {
        
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
                                tickSize: [1, "month"]
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
                            colors: ["#008115"]
                        });

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
                    
                </div>
            </div> <!-- fin GRAFICA de remitos -->
		</div>
	</div> <!-- fin tabbable -->

</asp:Content>
