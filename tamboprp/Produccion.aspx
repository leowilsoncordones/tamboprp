<%@ Page Title="tamboprp | producción" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Produccion.aspx.cs" Inherits="tamboprp.Produccion" %>
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
        <h1><i class="menu-icon fa fa-cogs"></i> Producción</h1>
    </div>
    
    <div class="row">
       <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="NuevoEvento.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-edit bigger-200"></i><br/>
                    Ingreso de nuevo evento
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="ControlProdUltimo.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-cogs bigger-200"></i><br/>
                    Último Control
	            </a>
            </div>
        </div>
       <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Lactancias.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-refresh bigger-200"></i><br/>
                    Lactancias
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="ListVitalicias.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-star-o bigger-200"></i><br/>
                    Vitalicias
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="GraficasProd.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-bar-chart-o bigger-200"></i><br/>
                    Gráficas de producción
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="ImportControl.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-upload bigger-200"></i><br/>
                    Importar control
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    
    <asp:Panel ID="pnlLinks" Visible="false" runat="server">
        <ul>
            <li><i class="menu-icon fa fa-edit blue"></i><asp:HyperLink ID="hypNuevoEvento" NavigateUrl="NuevoEvento.aspx" runat="server">  Ingreso de nuevo evento</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-cogs blue"></i><asp:HyperLink ID="hypControlProdUltimo" NavigateUrl="ControlProdUltimo.aspx" runat="server"> Último Control de Producción</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-refresh blue"></i><asp:HyperLink ID="hypLactancias" NavigateUrl="Lactancias.aspx" runat="server"> Lactancias</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-star-o blue"></i><asp:HyperLink ID="hypVitalicias" NavigateUrl="ListVitalicias.aspx" runat="server"> Vitalicias</asp:HyperLink></li>
        </ul>
    </asp:Panel>

</asp:Content>
