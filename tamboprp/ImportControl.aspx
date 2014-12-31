<%@ Page Title="tamboprp | importar control" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImportControl.aspx.cs" Inherits="tamboprp.ImportControl" %>
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
    <script src="js/jquery-ui.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/excanvas.js"></script>
    <script src="js/bootstrap.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />  
    <div class="page-header">
        <h1><i class="menu-icon fa fa-upload"></i> Importar archivo de controles de producción </h1>
    </div>

    <div class="row">
        <div class="col-sm-12">
            
            <!-- FORMULARIO -->
            <div id="formulario" class="form-horizontal">
                <!-- Cargar foto -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Seleccionar archivo de datos </label>
                    <div class="col-sm-3">
                        <label class="ace-file-input"><input type="file" id="id-input-file-2">
                            <span class="ace-file-container" data-title="Elegir">
                                <span class="ace-file-name" data-title="..."><i class=" ace-icon fa fa-upload"></i></span>
                            </span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a></label>
                    </div>
                    <div class="col-sm-12"></div>
		         </div>
                <!-- Botones -->
                <div class="form-group">
                    <div class="col-md-offset-3 col-md-9">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Importar datos" OnClick="btn_GuardarEvento" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
