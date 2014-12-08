<%@ Page Title="tamboprp | sitio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sitio.aspx.cs" Inherits="tamboprp.Sitio" %>
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
    <script src="js/excanvas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-sitemap"></i> Sitio</h1>
    </div>
    
    <a href="Default.aspx"><i class="menu-icon fa fa-home"></i><span class="menu-text"> Home </span></a><br/>
    <a href="Login.aspx" class="dropdown-toggle"><i class="menu-icon fa fa-unlock"></i><span class="menu-text"> Login </span></a><br/>

    <a href="Tablero.aspx"><i class="menu-icon fa fa-tachometer"></i><span class="menu-text"> Tablero </span></a><br/>
        <ul class="submenu">
            <li><a href="Indicadores.aspx">Indicadores (temporal)</a></li>
        </ul>
    <a href="Animales.aspx" class="dropdown-toggle"><i class="menu-icon fa fa-folder-open"></i><span class="menu-text"> Animales </span></a>
        <ul class="submenu">
            <li><a href="Animales.aspx">Fichas</a></li>
            <li><a href="Lactancias.aspx">Lactancias</a></li>
        </ul>
    <a href="Produccion.aspx"><i class="menu-icon fa fa-cogs"></i><span class="menu-text"> Producción </span></a>
        <ul class="submenu">
            <li><a href="NuevoEvento.aspx">Nuevo Evento</a></li>
            <li><a href="Remitos.aspx">Remitos a planta</a></li>
            <li><a href="ControlProduccionUltimo.aspx">Ultimo Control de Producción</a></li>
            <li><a href="Tambo.aspx">Tambo (?)</a></li>
        </ul>
    <a href="Analisis.aspx"><i class="menu-icon fa fa-eye"></i><span class="menu-text"> Análisis </span></a>
        <ul class="submenu">
            <li><a href="VacaEnOrdene.aspx">Vacas en ordeñe (no va)</a></li>
        </ul>
    <a href="Reportes.aspx"><i class="menu-icon fa fa-bar-chart-o"></i><span class="menu-text"> Reportes </span></a>
        <ul class="submenu">
            <li><a href="GraficaLeche.aspx">Producción Leche</a></li>
        </ul>
    <a href="Calendario_Partos.aspx"><i class="menu-icon fa fa-flask"></i><span class="menu-text"> Reproducción </span></a>
        <ul class="submenu">
            <li><a href="Calendario_Partos.aspx">Calendario de Partos</a></li>
            <li><a href="Servicios_Sin_DiagP_35.aspx">Vacas con 35 días de servicio y sin diagnostico de preñez</a></li>
            <li><a href="Servicios_Sin_DiagP_70.aspx">Vacas con 70 días servicios y sin diagnostico de preñez</a></li>
            <li><a href="LactanciasSinServ80.aspx">Vacas con 80 días en lactancia y sin servicio</a></li>
        </ul>
    <a href="Cabana.aspx"><i class="menu-icon fa fa-trophy"></i><span class="menu-text"> Cabaña </span></a>
        <ul class="submenu">
            <li><a href="Genealogia.aspx">Genealogía</a></li>
            <li><a href="Default.aspx">Concursos</a></li>
        </ul>
    <a href="Personal.aspx"><i class="menu-icon fa fa-users"></i><span class="menu-text"> Personal </span></a><br/>
    <a href="Notificaciones.aspx"><i class="menu-icon fa fa-bell-o"></i><span class="menu-text"> Notificaciones </span></a><br/>
    <a href="Sistema.aspx"><i class="menu-icon fa fa-desktop"></i><span class="menu-text"> Sistema </span></a>
        <ul class="submenu">
            <li><a href="Enfermedades.aspx">Enfermedades</a></li>
            <li><a href="Default.aspx">Categorías</a></li>
            <li><a href="Default.aspx">Usuarios</a></li>
            <li><a href="Sitio.aspx">Sitio</a></li>
            <li><a href="Default.aspx">Ayuda</a></li>
        </ul>
    <a href="Contact.aspx"><i class="menu-icon fa fa-envelope"></i><span class="menu-text"> Contacto </span></a><br/>
    <a href="About.aspx"><i class="menu-icon fa fa-copyright"></i><span class="menu-text"> About </span></a>

</asp:Content>
