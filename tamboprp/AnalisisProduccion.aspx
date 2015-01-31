<%@ Page Title="tamboprp | análisis productivo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisisProduccion.aspx.cs" Inherits="tamboprp.AnalisisProduccion" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-eye"></i> Análisis de producción y ordeñe</h1>
    </div>
    <!-- comienza tabbable -->
    <div class="tabbable">
		<ul class="nav nav-tabs" id="myTab">
			<li class="active"><a data-toggle="tab" href="#datosGenerales"><i class="blue ace-icon fa fa-list bigger-120"></i> Datos generales </a></li>
            <li><a data-toggle="tab" href="#tablaAnalitica"><i class="blue ace-icon fa fa-plug bigger-120"></i> Análisis de ordeñe <asp:Label ID="badgeCantOrdene" runat="server" cssClass="badge badge-success"></asp:Label></a></li>
		</ul>
        <!-- comienza contenido de tabbable -->
		<div class="tab-content">
		    
		    <!-- PESTANA 1: Datos generales -->
			<div id="datosGenerales" class="tab-pane fade in active">
			    <div class="page-header">
                    <h1><i class="menu-icon fa fa-list"></i> Datos generales de producción</h1>
                </div>
                <div class="row">
                <div class="col-md-6">
                    <div class="well">
						<h4 class="blue smaller">Datos del rodeo <asp:Label ID="lblDatos" runat="server" ></asp:Label></h4>
                        <ul class="list-unstyled spaced2">
                        <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                            <asp:Label ID="titCAntOrdene" runat="server" Text="Vacas en ordeñe:" ></asp:Label>&nbsp;&nbsp;
                            <strong><asp:Label ID="lblCantOrdene" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                        <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                            <asp:Label ID="titCantEntoradas" runat="server" Text="Vacas entoradas:" ></asp:Label>&nbsp;&nbsp;
                            <strong><asp:Label ID="lblCantEntoradas" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                        <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                            <asp:Label ID="titCantSecas" runat="server" Text="Vacas secas:" ></asp:Label>&nbsp;&nbsp;
                            <strong><asp:Label ID="lblCantSecas" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                        <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                            <asp:Label ID="titCantAbortos" runat="server" Text="Abortos este año:" ></asp:Label>&nbsp;&nbsp;
                            <strong><asp:Label ID="lblAbortosEsteAno" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                        </ul>
					</div>
                </div>
                </div>
                <div class="row">
                <div class="col-md-6">
                    <div class="well">
                        <h4 class="blue smaller">Último control lechero: <asp:Label ID="lblFechaUltControl" runat="server" ></asp:Label></h4>
						<ul class="list-unstyled spaced2">
                        <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                            <asp:Label ID="titCantAnControl" runat="server" Text="Cantidad de animales en último control lechero:" ></asp:Label>&nbsp;&nbsp;
                            <strong><asp:Label ID="lblCantAnUltControl" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                        <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                            <asp:Label ID="titSumLecheUltControl" runat="server" Text="Producción total de leche en último control:" ></asp:Label>&nbsp;&nbsp;
                            <strong><asp:Label ID="lblSumLecheUltControl" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                        <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                            <asp:Label ID="titPromLecheUltControl" runat="server" Text="Producción promedio de leche en último control:" ></asp:Label>&nbsp;&nbsp;
                            <strong><asp:Label ID="lblPromLecheUltControl" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                        <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                            <asp:Label ID="titSumGrasaUltControl" runat="server" Text="Producción total de grasa en último control:" ></asp:Label>&nbsp;&nbsp;
                            <strong><asp:Label ID="lblSumGrasaUltControl" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                        <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                            <asp:Label ID="titPromGrasaUltControl" runat="server" Text="Producción promedio de grasa en último control:" ></asp:Label>&nbsp;&nbsp;
                            <strong><asp:Label ID="lblPromGrasaUltControl" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                        </ul>
					</div>
                </div>
                </div>
			</div> <!-- fin Datos Generales -->

            <!-- PESTANA 2: Tabla analitica de vacas en ordeñe -->
			<div id="tablaAnalitica" class="tab-pane fade">
			    <div class="page-header">
			        <h1><i class="menu-icon fa fa-plug"></i> Análisis de ordeñe</h1>
                </div>
			    <div class="row">
                <div class="col-md-6">
                    <div class="well">
                        <h4 class="blue smaller">Tabla analítica de vacas en ordeñe</h4>
						<ul class="list-unstyled spaced2">
                            <li><h5><i class="ace-icon fa fa-caret-right blue fa-lg"></i>
                                <asp:Label ID="titCant" runat="server" Text="Cantidad en la categoría:" ></asp:Label>&nbsp;&nbsp;
                                <strong><asp:Label ID="lblCant" CssClass="text-success" runat="server" Text="" ></asp:Label></strong></h5></li>
                            <li><h5><i class="ace-icon fa fa-caret-right blue fa-lg"></i>
                                <asp:Label ID="titPromProdLecheLts" runat="server" Text="Promedio de producción de leche (litros):" ></asp:Label>&nbsp;&nbsp;
                                <strong><asp:Label ID="lblPromProdLecheLts" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                            <li><h5><i class="ace-icon fa fa-caret-right blue fa-lg"></i>
                                <asp:Label ID="titCantL1" runat="server" Text="Cantidad en lactancia 1:"></asp:Label>&nbsp;&nbsp;
                                <strong><asp:Label ID="lblCantL1" CssClass="text-success" runat="server" ></asp:Label>&nbsp;&nbsp;-&nbsp;
                                <asp:Label ID="lblPorcL1" CssClass="blue" runat="server" ></asp:Label></strong></h5></li>
                            <li><h5><i class="ace-icon fa fa-caret-right blue fa-lg"></i>
                                <asp:Label ID="titCantL2" runat="server" Text="Cantidad en lactancia 2:"></asp:Label>&nbsp;&nbsp;
                                <strong><asp:Label ID="lblCantL2" CssClass="text-success" runat="server" ></asp:Label>&nbsp;&nbsp;-&nbsp;
                                <asp:Label ID="lblPorcL2" CssClass="blue" runat="server" ></asp:Label></strong></h5></li>
                            <li><h5><i class="ace-icon fa fa-caret-right blue fa-lg"></i>
                                <asp:Label ID="titCantOtrasL" runat="server" Text="Cantidad en otras lactancias:"></asp:Label>&nbsp;&nbsp;
                                <strong><asp:Label ID="lblCantOtrasL" CssClass="text-success" runat="server" ></asp:Label>&nbsp;&nbsp;-&nbsp;
                                <asp:Label ID="lblPorcOtrasL" CssClass="blue" runat="server" ></asp:Label></strong></h5></li>
                            <li><h5><i class="ace-icon fa fa-caret-right blue fa-lg"></i>
                                <asp:Label ID="titConSSP" runat="server" Text="Con servicios y sin preñez:"></asp:Label>&nbsp;&nbsp;
                                <strong><asp:Label ID="lblConSSP" CssClass="text-success" runat="server" ></asp:Label>&nbsp;&nbsp;-&nbsp;
                                <asp:Label ID="lblPorcConSSP" CssClass="blue" runat="server" ></asp:Label></strong></h5></li>
                            <li><h5><i class="ace-icon fa fa-caret-right blue fa-lg"></i>
                                <asp:Label ID="titPrenezConf" runat="server" Text="Preñez confirmada:"></asp:Label>&nbsp;&nbsp;
                                <strong><asp:Label ID="lblPrenezConf" CssClass="text-success" runat="server" ></asp:Label>&nbsp;&nbsp;-&nbsp;
                                <asp:Label ID="lblPorcPrenezConf" CssClass="blue" runat="server" ></asp:Label></strong></h5></li>
                            <li><h5><i class="ace-icon fa fa-caret-right blue fa-lg"></i>
                                <asp:Label ID="titPromL" runat="server" Text="Promedio de lactancias (días):"></asp:Label>&nbsp;&nbsp;
                                <strong><asp:Label ID="lblPromL" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-6">
                </div>
            </div>
            </div> <!-- fin Tabla Analitica de vacas en ordeñe -->
        </div>
	</div> <!-- fin tabbable -->
    
</asp:Content>
