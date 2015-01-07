<%@ Page Title="tamboprp | personal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Personal.aspx.cs" Inherits="tamboprp.Personal" %>
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
<asp:Content ID="ContentPersonal" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-male"></i> Personal</h1>
    </div>
    
    <div class="row">
       <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="NuevoEmpleado.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-edit bigger-200"></i><br/>
                    Ingreso de empleado
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="ListPersonal.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-male bigger-200"></i><br/>
                    Lista de empleados
	            </a>
            </div>
        </div>
       <div class="col-md-1"></div>
    </div>

</asp:Content>
