<%@ Page Title="tamboprp | analisis" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Analisis.aspx.cs" Inherits="tamboprp.Analisis" %>
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
    <!-- comienza tabbable -->
    <div class="tabbable">
		<ul class="nav nav-tabs" id="myTab">
			<li class="active"><a data-toggle="tab" href="#datosGenerales"><i class="blue ace-icon fa fa-list bigger-120"></i> Datos generales </a></li>
            <li><a data-toggle="tab" href="#tablaAnalitica"><i class="blue ace-icon fa fa-plug bigger-120"></i> Tabla analítica en ordeñe <asp:Label ID="badgeCantOrdene" runat="server" cssClass="badge badge-success"></asp:Label></a></li>
            <li class="dropdown"><a data-toggle="dropdown" class="dropdown-toggle" href="#">Dropdown &nbsp;<i class="ace-icon fa fa-caret-down bigger-110 width-auto"></i></a>
				<ul class="dropdown-menu dropdown-info">
					<li><a data-toggle="tab" href="#dropdown1">@fat</a></li>
					<li><a data-toggle="tab" href="#dropdown2">@mdo</a></li>
				</ul>
			</li>
		</ul>
        <!-- comienza contenido de tabbable -->
		<div class="tab-content">
			<div id="datosGenerales" class="tab-pane fade in active">
				<h2 class="page-header">Datos Generales de producción</h2>
                    <h3 class="widget-title">Datos del rodeo</h3>
                    <p>
                        <asp:Label ID="titCAntOrdene" runat="server" Text="Vacas en ordeñe: " ></asp:Label><asp:Label ID="lblCantOrdene" runat="server" ></asp:Label><br/>
                        <asp:Label ID="titCantEntoradas" runat="server" Text="Vacas entoradas: " ></asp:Label><asp:Label ID="lblCantEntoradas" runat="server" ></asp:Label><br/>
                        <asp:Label ID="titCantSecas" runat="server" Text="Vacas secas: " ></asp:Label><asp:Label ID="lblCantSecas" runat="server" ></asp:Label><br/>
                        <asp:Label ID="titCantAbortos" runat="server" Text="Abortos este año: " ></asp:Label><asp:Label ID="lblAbortosEsteAno" runat="server" ></asp:Label><br/>
                    </p>
                    <h3 class="widget-title">Último control lechero <asp:Label ID="lblFechaUltControl" runat="server" ></asp:Label></h3>
                    <p>
                        <asp:Label ID="titCantAnControl" runat="server" Text="Cantidad de animales en último control lechero: " ></asp:Label><asp:Label ID="lblCantAnUltControl" runat="server" ></asp:Label><br/>
                        <asp:Label ID="titSumLecheUltControl" runat="server" Text="Producción total de leche en último control: " ></asp:Label><asp:Label ID="lblSumLecheUltControl" runat="server" ></asp:Label><br/>
                        <asp:Label ID="titPromLecheUltControl" runat="server" Text="Producción promedio de leche en último control: " ></asp:Label><asp:Label ID="lblPromLecheUltControl" runat="server" ></asp:Label><br/>
                        <asp:Label ID="titSumGrasaUltControl" runat="server" Text="Producción total de grasa en último control: " ></asp:Label><asp:Label ID="lblSumGrasaUltControl" runat="server" ></asp:Label><br/>
                        <asp:Label ID="titPromGrasaUltControl" runat="server" Text="Producción promedio de grasa en último control: " ></asp:Label><asp:Label ID="lblPromGrasaUltControl" runat="server" ></asp:Label><br/>
                    </p>
			</div> <!-- fin Datos Generales -->
			<div id="tablaAnalitica" class="tab-pane fade">
				<h2 class="page-header">Tabla analítica de vacas en ordeñe</h2>
                <p>
                    <asp:Label ID="titCant" runat="server" Text="Cantidad en la categoría: " ></asp:Label><asp:Label ID="lblCant" runat="server" Text="" ></asp:Label><br/>
                    <asp:Label ID="titPromProdLecheLts" runat="server" Text="Promedio de producción de leche (litros): " ></asp:Label><asp:Label ID="lblPromProdLecheLts" runat="server" ></asp:Label><br/>
                    <asp:Label ID="titCantL1" runat="server" Text="Cantidad en lactancia 1: "></asp:Label><asp:Label ID="lblCantL1" runat="server" ></asp:Label><br/>
                    <asp:Label ID="titCantL2" runat="server" Text="Cantidad en lactancia 2: "></asp:Label><asp:Label ID="lblCantL2" runat="server" ></asp:Label><br/>
                    <asp:Label ID="titCantOtrasL" runat="server" Text="Cantidad en otras lactancias: "></asp:Label><asp:Label ID="lblCantOtrasL" runat="server" ></asp:Label><br/>
                    <asp:Label ID="titConSSP" runat="server" Text="Con servicios y sin preñez: "></asp:Label><asp:Label ID="lblConSSP" runat="server" ></asp:Label><br/>
                    <asp:Label ID="titPrenezConf" runat="server" Text="Preñez confirmada: "></asp:Label><asp:Label ID="lblPrenezConf" runat="server" ></asp:Label><br/>
                    <asp:Label ID="titPromL" runat="server" Text="Promedio de lactancias (días): "></asp:Label><asp:Label ID="lblPromL" runat="server" ></asp:Label><br/>
                </p>
			</div> <!-- fin Tabla Analitica de vacas en ordeñe -->

			<div id="dropdown1" class="tab-pane fade">
				<p>DD1</p>
			</div>
			<div id="dropdown2" class="tab-pane fade">
				<p>DD2</p>
			</div>
		</div>
	</div> <!-- fin tabbable -->
    
</asp:Content>
