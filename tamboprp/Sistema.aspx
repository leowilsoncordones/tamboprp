<%@ Page Title="tamboprp | sistema" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sistema.aspx.cs" Inherits="tamboprp.Sistema" %>
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
        <h1><i class="menu-icon fa fa-desktop"></i> Sistema</h1>
    </div>
    
    <div class="row">
       <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="NuevoUsuario.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-edit bigger-200"></i><br/>
                    Ingreso nuevo usuario
	            </a>
            </div>
        <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="GestionUsuarios.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-users bigger-200"></i><br/>
                    Gestión de usuarios
	            </a>
            </div>
        </div>
       <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="MiPerfil.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-user bigger-200"></i><br/>
                    Mi perfil
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Corporativo.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-book bigger-200"></i><br/>
                    Datos corporativos
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Auditoria.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-keyboard-o bigger-200"></i><br/>
                    Auditoría
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Help.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-wrench bigger-200"></i><br/>
                    Ayuda y soporte
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Sitio.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-sitemap bigger-200"></i><br/>
                    Mapa del sitio
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Contact.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-envelope bigger-200"></i><br/>
                    Contacto
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row">
       <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="About.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-check-square-o bigger-200"></i><br/>
                    Acerca de
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Logoff.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-sign-out bigger-200"></i><br/>
                    Salir
	            </a>
            </div>
        </div>
       <div class="col-md-1"></div>
    </div>
    
    
    <asp:Panel ID="pnlLinks" Visible="false" runat="server">
    <ul>
        <li><i class="menu-icon fa fa-user blue"></i><asp:HyperLink ID="hypUsuarios" NavigateUrl="Usuarios.aspx" runat="server"> Usuarios del sistema</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-edit blue"></i><asp:HyperLink ID="hypNuevoUsuario" NavigateUrl="NuevoUsuario.aspx" runat="server"> Ingreso de nuevo usuario</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-sitemap blue"></i><asp:HyperLink ID="hypSitio" NavigateUrl="Sitio.aspx" runat="server">  Mapa del sitio</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-keyboard-o blue"></i><asp:HyperLink ID="hypAuditoria" NavigateUrl="Auditoria.aspx" runat="server">  Auditoría de registros de actividad</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-book blue"></i><asp:HyperLink ID="hypCorporativo" NavigateUrl="Corporativo.aspx" runat="server">  Datos corporativos</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-question blue"></i><asp:HyperLink ID="hypAyuda" NavigateUrl="Help.aspx" runat="server">  Ayuda</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-copyright blue"></i><asp:HyperLink ID="hypAbout" NavigateUrl="About.aspx" runat="server">  Acerca de</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-envelope blue"></i><asp:HyperLink ID="hypContact" NavigateUrl="Contact.aspx" runat="server">  Contacto</asp:HyperLink></li>
    </ul>
    </asp:Panel>

</asp:Content>
