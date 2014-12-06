<%@ Page Title="tamboprp | tablero" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tablero.aspx.cs" Inherits="tamboprp.Tablero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >
    <div class="page-header">
        <h1><i class="menu-icon fa fa-tachometer"></i> Tablero <small><i class="ace-icon fa fa-angle-double-right"></i> información general, notificaciones e indicadores</small></h1>
    </div>
  <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />  

    
   <div class="row">
    <div class="col-md-10" id="sales-charts"></div>
    <div class="col-md-2"></div>
   </div>
    <br/>
    
    <form onsubmit="testFunction()" name="testForm">
        <input type="text" name="tb_test" id="idTest" value="0"/>
        <input type="text" name="tb_test" id="idTest1" value="1"/>
        <input type="submit" class="btn" value="test button" />
        <br/>
    </form>
    <br/>
    <input type="button" class="btn" value="otro button" onclick="testFunction()"/>
            <div name="divTest">
                <label id="testLabelId">Inicio</label>
            </div>
    
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
            PageMethods.ControlTotalGetAll(OnSuccess);
        }

        function OnSuccess(response){
            var leche3 = [];
        var list = response;
            for (var i = 0; i < list.length; i++) {
                leche3.push([gd1(list[i].Fecha), list[i].Leche]);
            }
            imprimir(leche3);
            
        }


        var sales_charts = $('#sales-charts').css({ 'height': '360px' });
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

        function testFunction() {
            var x = $('#idTest').val();
            var y = $('#idTest1').val();
            alert("HOLAAA " + x + " " + y);

            $.ajax({
                type: "post",
                url: "Tablero.aspx/TestAjax",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ dato1: x, dato2: y }),
                success: function (result) {
                    OnSuccess1(result.d);
                },
                error: function (xhr, status, error) {
                    OnFailure1(error);
                }
            });
        }

        function OnSuccess1(data) {
            if (data) {
                $('#testLabelId').text(data);
            }
        }
        function OnFailure1(error) {
            alert(error);
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
