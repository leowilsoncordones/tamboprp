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
    
    <script type="text/javascript" src="/js/flot/jquery.flot.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header"><i class="menu-icon fa fa-tachometer"></i> Tablero</h1>

    <script type="text/javascript">
            $(function () {
                var grasa = [[0, 1819.00], [2, 1866.70], [4, 2238.20], [6, 2393.90], [8, 2343.60]];
                var leche = [[0, 11367.40], [2, 11621.43], [4, 12999.80], [6, 14070.20], [8, 15831.20]];
                $.plot($("#placeholder"), [grasa, leche]);

            });
    </script>
    
    <div id="placeholder" style="width:50%;height:200px;"></div>
    <div>Controles de producción año 2014</div>


</asp:Content>
