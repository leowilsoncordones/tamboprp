<%@ Page Title="tamboprp | tablero" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tablero.aspx.cs" Inherits="tamboprp.Tablero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    
   <div class="row">
    <div class="col-md-6" id="sales-charts"></div>
    <div class="col-md-6"></div>
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
        
       
/*
        var controlesleche = [10087.60, 8978.60, 9915.60, 11367.40, 11621.43, 12999.80, 14070.20, 15831.20];

        var leche = [];
        for (var i = 0; i < 8; i ++) {
            leche.push([i, controlesleche[i]]);
        }
        */
        var leche1 = [[gd(2014, 2, 4), 10087.60], [gd(2014, 3, 5), 8978.60], [gd(2014, 4, 5), 9915.60], [gd(2014, 5, 5), 11367.40], [gd(2014, 6, 3), 11621.43], [gd(2014, 7, 2), 12999.80], [gd(2014, 8, 4), 14070.20], [gd(2014, 9, 2), 15831.20]];

        function gd(year, month, day) {
            return new Date(year, month-1, day).getTime();
        }


        /*
        FECHA	LECHE	GRASA
        2014-02-04	10087.60	1489.20
        2014-03-05	8978.60	1562.85
        2014-04-05	9915.60	1974.00
        2014-05-05	11367.40	1819.00
        2014-06-03	11621.43	1866.70
        2014-07-02	12999.80	2238.20
        2014-08-04	14070.20	2393.90
        2014-09-02	15831.20	2343.60
        
         */
        var controlesgrasa = [1489.20, 1562.85, 1974.00, 1819.00, 1866.70, 2238.20, 2393.90, 2343.60];
        var grasa = [];
        for (var i = 0; i < 8; i++) {
            grasa.push([i, controlesgrasa[i]]);
        }

       

        /*  var d3 = [];
        for (var i = 0; i < Math.PI * 2; i += 0.2) {
            d3.push([i, Math.tan(i)]);
        }
       */

        var sales_charts = $('#sales-charts').css({'height': '360px' });
        $.plot("#sales-charts", [
            { label: "Leche", data: leche1 }
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
                borderColor: '#555'
            }
        });
    </script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/excanvas.js"></script>
    <script src="js/flot/jquery.flot.js"></script>
</asp:Content>
