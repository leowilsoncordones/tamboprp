<%@ Page Title="tamboprp | reproducción" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reproduccion.aspx.cs" Inherits="tamboprp.Reproduccion" %>
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
        <h1><i class="menu-icon fa fa-flask"></i> Reproducción</h1>
    </div>
    
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="CalendarioPartos.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-calendar bigger-200"></i><br/>
                    Calendario de Partos
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="DiagEcograficos.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-briefcase bigger-200"></i><br/>
                    Diag. ecográficos
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="ServiciosSinDiag.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-bullhorn bigger-200"></i><br/>
                    Servicios sin diagnóstico
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="LactanciasSinServ80.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-lightbulb-o bigger-200"></i><br/>
                    En lact. sin servicio
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Inseminaciones.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-hand-o-right bigger-200"></i><br/>
                    Preñez confirmada
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="ListPartos.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-list bigger-200"></i><br/>
                    Listado de partos
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="AnalisisToros.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-flask bigger-200"></i><br/>
                    Toros y su efectividad
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="ListAnimIndRechazo.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-shield bigger-200"></i><br/>
                    Indicación de rechazo
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    
    <asp:Panel ID="pnlLinks" Visible="false" runat="server">
        <ul>
            <li><i class="menu-icon fa fa-calendar blue"></i><asp:HyperLink ID="hypCalendarioPartos" NavigateUrl="CalendarioPartos.aspx" runat="server">  Calendario de Partos</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-briefcase blue"></i><asp:HyperLink ID="hypDiagEcograficos" NavigateUrl="DiagEcograficos.aspx" runat="server"> Diagnósticos ecográficos</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-lightbulb-o blue"></i><asp:HyperLink ID="hypServiciosSinDiag" NavigateUrl="ServiciosSinDiag.aspx" runat="server">  Servicios sin diagnóstico</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-bullhorn blue"></i><asp:HyperLink ID="hyp80DiasLactSinServ" NavigateUrl="LactanciasSinServ80.aspx" runat="server">  Vacas con 80 días en lactancia y sin servicio</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-hand-o-right blue"></i><asp:HyperLink ID="hypInseminaciones" NavigateUrl="Inseminaciones.aspx" runat="server">  Inseminaciones</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-list blue"></i><asp:HyperLink ID="hypListPartos" NavigateUrl="ListPartos.aspx" runat="server">  Listado de partos</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-flask blue"></i><asp:HyperLink ID="hypListToros" NavigateUrl="ListTorosUtilizados.aspx" runat="server">  Toros utilizados y su efectividad</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-shield blue"></i><asp:HyperLink ID="hypListAnimConRechazo" NavigateUrl="ListAnimIndRechazo.aspx" runat="server">  Animales con indicación de rechazo</asp:HyperLink></li>
        </ul>
    </asp:Panel>
</asp:Content>
