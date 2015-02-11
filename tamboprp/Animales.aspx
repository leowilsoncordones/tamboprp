<%@ Page Title="tamboprp | animales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Animales.aspx.cs" Inherits="tamboprp.Animales" %>
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
        <h1><i class="menu-icon fa fa-github-alt" ></i> Animales</h1>
    </div>
    
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="#selectModal" role="button" data-toggle="modal" class="bigger-160">
		            <i class="ace-icon fa fa-edit bigger-200"></i><br/>
                    Ingreso de animales
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="FichaAnimal.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-github-alt bigger-200"></i><br/>
                    Ficha de animal
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
  
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="ListPorCategoria.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-list bigger-200"></i><br/>
                    Listado Por Categoría
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Categorias.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-tags bigger-200"></i><br/>
                    Categorías de animales
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    
    <!-- SELECCIONAR MODAL -->
    <div id="selectModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-trash"></i> Seleccione</h4>
                </div>
                <div class="modal-body">
                    <span id="bodySelectModal" class="text-warning center">
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-11 container">
                                <div class="col-md-5 jumbotron align-center lighter">
                                    <a href="NuevoAnimalParto.aspx" class="bigger-130">
		                                <i class="ace-icon fa fa-tags bigger-180"></i><br/>
                                         Parto y crías
	                                </a>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-5 jumbotron align-center lighter">
                                    <a href="NuevoAnimal.aspx" class="bigger-130">
		                                <i class="ace-icon fa fa-usd bigger-180"></i><br/>
                                        Adquisición
	                                </a>
                                </div>
                            </div>
                        </div>
                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->
    
    
    

</asp:Content>
