<%@ Page Title="tamboprp | cabaña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cabana.aspx.cs" Inherits="tamboprp.Cabana" %>
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
        <h1><i class="menu-icon fa fa-trophy"></i> Cabaña</h1>
    </div>
    
        <div class="row">
       <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Genealogia.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-puzzle-piece bigger-200"></i><br/>
                    Genealogía
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Calificaciones.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-sort-numeric-desc bigger-200"></i><br/>
                    Calificaciones
	            </a>
            </div>
        </div>
       <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Concursos.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-trophy bigger-200"></i><br/>
                    Concursos
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="CategConcurso.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-tags bigger-200"></i><br/>
                    Categorías de concurso
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    
    <asp:Panel ID="pnlLinks" Visible="false" runat="server">
        <ul>
            <li><i class="menu-icon fa fa-puzzle-piece blue"></i><asp:HyperLink ID="hypGenealogia" NavigateUrl="Genealogia.aspx" runat="server">  Genealogía</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-sort-numeric-desc blue"></i><asp:HyperLink ID="hypCalificaciones" NavigateUrl="Calificaciones.aspx" runat="server"> Calificaciones</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-trophy blue"></i><asp:HyperLink ID="hypConcursos" NavigateUrl="Concursos.aspx" runat="server">  Concursos</asp:HyperLink></li>
            <li><i class="menu-icon fa fa-tags blue"></i><asp:HyperLink ID="hypCategConcursos" NavigateUrl="CategConcurso.aspx" runat="server">  Categorías de concurso</asp:HyperLink></li>
        </ul>
    </asp:Panel>
</asp:Content>
