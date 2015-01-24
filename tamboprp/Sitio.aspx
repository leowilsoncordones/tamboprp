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
        <h1><i class="menu-icon fa fa-sitemap"></i> Mapa del Sitio</h1>
    </div>
    
    <a href="Default.aspx"><i class="menu-icon fa fa-home"></i><span class="menu-text"> Home </span></a> <i class="fa fa-check text-success"></i><br/>
    <a href="Login.aspx" class="dropdown-toggle"><i class="menu-icon fa fa-unlock"></i><span class="menu-text"> Login </span></a> <i class="fa fa-check text-success"></i><br/>

    <a href="Tablero.aspx"><i class="menu-icon fa fa-tachometer"></i><span class="menu-text"> Tablero </span></a> <i class="fa fa-check text-success"></i><br/>
    <a href="Animales.aspx" class="dropdown-toggle"><i class="menu-icon fa fa-folder-open"></i><span class="menu-text"> Animales </span></a>
        <ul class="submenu">
            <li><a href="FichaAnimal.aspx">Ficha de animal</a></li>
            <li><a href="NuevoAnimal.aspx">Nuevo Animal</a></li>
            <li><a href="ListPorCategoria.aspx">Listado Por Categoría</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Categorias.aspx">Categorías</a> <i class="fa fa-check text-success"></i></li>
            </ul>
    <a href="Produccion.aspx"><i class="menu-icon fa fa-cogs"></i><span class="menu-text"> Producción </span></a>
        <ul class="submenu">
            <li><a href="NuevoEvento.aspx">Nuevo Evento</a></li>
            <li><a href="ControlProdUltimo.aspx">Último Control de Producción</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Lactancias.aspx">Lactancias</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="ListVitalicias.aspx">Vitalicias</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="ImportControl.aspx">Importar archivo de controles de producción</a></li>
        </ul>
    <a href="Analisis.aspx"><i class="menu-icon fa fa-eye"></i><span class="menu-text"> Análisis </span></a><br/>
        <ul class="submenu">
            <li><a href="AnalisisProduccion.aspx">Productivo</a></li>
            <li><a href="AnalisisReprod.aspx">Reproductivo</a></li>
            <li><a href="AnalisisVentas.aspx">Ventas</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="AnalisisMuertes.aspx">Muertes</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="AnalisisToros.aspx">Toros utilizados y su efectividad</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="AnalisisInseminadores.aspx">Ranking de inseminadores</a></li>
        </ul>
    <a href="Reportes.aspx"><i class="menu-icon fa fa-bar-chart-o"></i><span class="menu-text"> Reportes </span></a>
        <ul class="submenu">
            <li><a href="GraficasProd.aspx">Gráficas de producción</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Remitos.aspx">Remitos a planta</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="NuevoRemito.aspx">Nuevo remito</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="EmpresasRemisoras.aspx">Empresas Remisoras</a> <i class="fa fa-check text-success"></i></li>
        </ul>
    <a href="Reproduccion.aspx"><i class="menu-icon fa fa-flask"></i><span class="menu-text"> Reproducción </span></a>
        <ul class="submenu">
            <li><a href="CalendarioPartos.aspx">Calendario de Partos</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="DiagEcograficos.aspx">Diagnósticos ecográficos</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="ServiciosSinDiag.aspx">Servicios sin diagnóstico</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="LactanciasSinServ80.aspx">En lactancia y sin servicio</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Inseminaciones.aspx">Inseminaciones con preñez confirmada</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="ListPartos.aspx">Listado de partos</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="AnalisisToros.aspx">Toros utilizados y su efectividad</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="ListAnimIndRechazo.aspx">Animales con indicación de rechazo</a></li>
        </ul>
    <a href="Cabana.aspx"><i class="menu-icon fa fa-trophy"></i><span class="menu-text"> Cabaña </span></a>
        <ul class="submenu">
            <li><a href="Genealogia.aspx">Genealogía</a></li>
            <li><a href="Calificaciones.aspx">Calificaciones</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Concursos.aspx">Concursos</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="CategConcurso.aspx">Categorías de concurso</a> <i class="fa fa-check text-success"></i></li>
        </ul>
    <a href="Sanidad.aspx"><i class="menu-icon fa fa-stethoscope"></i><span class="menu-text"> Sanidad </span></a>
        <ul class="submenu">
            <li><a href="Enfermedades.aspx">Enfermedades</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="AnalisisMuertes.aspx">Muertes</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Cmt.aspx">C.M.T.</a> <i class="fa fa-check text-success"></i></li>
        </ul>
    <a href="Personal.aspx"><i class="menu-icon fa fa-users"></i><span class="menu-text"> Personal </span></a><br/>
        <ul class="submenu">
            <li><a href="NuevoEmpleado.aspx">Nuevo empleado</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="ListPersonal.aspx">Lista de empleados</a> <i class="fa fa-check text-success"></i></li>
        </ul>
    <a href="Notificaciones.aspx"><i class="menu-icon fa fa-newspaper-o"></i><span class="menu-text"> Notificaciones </span></a><br/>
    <a href="Sistema.aspx"><i class="menu-icon fa fa-desktop"></i><span class="menu-text"> Sistema </span></a>
        <ul class="submenu">
            <li><a href="MiPerfil.aspx">Mi perfil</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Corporativo.aspx">Corporativo</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="GestionUsuarios.aspx">Gestión de usuarios</a></li>
            <li><a href="NuevoUsuario.aspx">Nuevo usuario</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Sitio.aspx">Mapa del Sitio</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Auditoria.aspx">Auditoría</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Help.aspx">Ayuda y soporte</a> <i class="fa fa-check text-success"></i></li>
            <li><a href="Logoff.aspx">Salir</a> <i class="fa fa-check text-success"></i></li>
        </ul>
    <a href="Contact.aspx"><i class="menu-icon fa fa-envelope"></i><span class="menu-text"> Contacto </span></a><i class="fa fa-check text-success"></i><br/>
    <a href="About.aspx"><i class="menu-icon fa fa-check-square-o"></i><span class="menu-text"> Acerca de </span><i class="fa fa-check text-success"></i></a>
    <br/>
</asp:Content>
