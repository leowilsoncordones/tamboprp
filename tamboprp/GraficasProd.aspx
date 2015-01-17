<%@ Page Title="tamboprp | gráficas de producción" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GraficasProd.aspx.cs" Inherits="tamboprp.GraficasProd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >
    <div class="page-header">
        <h1><i class="menu-icon fa fa-bar-chart-o"></i> Gráficas de Producción <small><i class="ace-icon fa fa-angle-double-right"></i> leche, grasa y porcentaje de grasa</small></h1>
    </div>
  <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />  

    
   <div class="row">
       <h4 class="widget-title blue lighter">Leche <asp:Label ID="Label1" runat="server" ></asp:Label></h4>
        <div class="col-md-9" id="graficaLeche"></div>
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
    
    <hr/>
   <div class="row">
        <h4 class="widget-title blue lighter">Grasa <asp:Label ID="Label2" runat="server" ></asp:Label></h4>
        <div class="col-md-9" id="graficaGrasa"></div>
        <div class="col-md-3"></div>
   </div>
    <hr/>
   <div class="row">
        <h4 class="widget-title blue lighter">Porcentaje de Grasa <asp:Label ID="Label3" runat="server" ></asp:Label></h4>
        <div class="col-md-9" id="graficaPorcGrasa"></div>
        <div class="col-md-3"></div>
   </div>

    <br/>
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
            },
            format: 'DD/MM/YYYY'
        });

        $(document).ready(function() {
            //GetValoreLeche();
            
        });


        

        function gd1(date) {
            return new Date(date).getTime();
        }

        // ---------- Se traen valores de la consulta sql para generar graficas -------------- //

        function GetValoreLeche(data) {
            if (data == 0) PageMethods.ControlTotalGetAnioCorriente(OnSuccess);
            if (data == 1) PageMethods.ControlTotalGetUltimoAnio(OnSuccess);
        }

        function OnSuccess(response){
            var listaLeche = [];
            var listaGrasa = [];
        var list = response;
            for (var i = 0; i < list.length; i++) {
                listaLeche.push([gd1(list[i].Fecha), list[i].Leche]);
                listaGrasa.push([gd1(list[i].Fecha), list[i].Grasa]);
            }
            imprimirLeche(listaLeche);
            imprimirGrasa(listaGrasa);
        }

        // ----------  GRAFICA  LECHE -------------- //
        var grafLeche = $('#graficaLeche').css({ 'height': '360px' });

        function imprimirLeche(totalLeche) {
        
            var tick = Math.round(totalLeche.length / 10);
        $.plot("#graficaLeche", [
            { label: "Leche", data: totalLeche }
            //{ label: "Grasa", data: grasa }

        ], {
            hoverable: true,
            shadowSize: 0,
            series: {
                lines: { show: true },
                points: {
                    show: true,
                    fill: true
                }
            },
            xaxis: {
                tickLength: 0,
                mode: "time",
                timeformat: "%m/%Y",
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
            }

        });

        }



        // ----------  GRAFICA  GRASA -------------- //

        var grafLeche = $('#graficaGrasa').css({ 'height': '360px' });

        function imprimirGrasa(totalGrasa) {
            var tick = Math.round(totalGrasa.length / 10);
            $.plot("#graficaGrasa", [
                //{ label: "Leche", data: totalGrasa }
                { label: "Grasa", data: totalGrasa }

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
                    timeformat: "%m/%Y",
                    tickSize: [tick, "month"]
                },
                yaxis: {
                    ticks: 10,
                    min: 0,
                    max: 4000,
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
                colors: ["#ff2b00"]
            });

        }

        function cargarGraficas(data) {
            
            var res = data.split(" - ");
            var fecha1 = formatoFecha(res[0]);
            var fecha2 = formatoFecha(res[1]);
            PageMethods.GetControlesTotalesEntreDosFechas(fecha1, fecha2, OnSuccess);
        };

        function formatoFecha(fecha) {
            var res = fecha.split("/");
            var salida = "";
            salida = res[2] + "-" + res[1] + "-" + res[0];
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
</asp:Content>
