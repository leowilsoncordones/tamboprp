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
        <div class="col-md-3"></div>
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

        // ---------- Se traen valores de la consulta sql para generar graficas -------------- //

        function GetValoreLeche() {
            PageMethods.ControlTotalGetAll(OnSuccess);
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
        
        $.plot("#graficaLeche", [
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

        // ----------  GRAFICA  GRASA -------------- //

        var grafLeche = $('#graficaGrasa').css({ 'height': '360px' });

        function imprimirGrasa(totalGrasa) {

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
                    timeformat: "%Y/%m",
                    tickSize: [2, "month"]
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
                colors: ["#ff2b00"]
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
</asp:Content>
