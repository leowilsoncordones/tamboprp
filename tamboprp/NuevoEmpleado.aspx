<%@ Page Title="tamboprp | nuevo empleado" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoEmpleado.aspx.cs" Inherits="tamboprp.NuevoEmpleado" %>
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

    <script src="js/js_tamboprp/PasswordStrongChecker.js"></script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />  
    <div class="page-header">
        <h1><i class="menu-icon fa fa-user"></i> Ingreso de un nuevo empleado </h1>
    </div>

    <div class="row">
        <div class="col-sm-12">
            <br/>
            <!-- FORMULARIO -->
            <div id="formulario" class="form-horizontal">
                <!-- Nombre y Apellido -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Nombre </label>
			        <div class="col-sm-3">
			            <input type="text" runat="server" id="fNombre" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Apellido </label>
			        <div class="col-sm-3">
			            <input type="text" runat="server" id="fApellido" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Iniciales </label>
			        <div class="col-sm-1">
			            <input type="text" runat="server" id="fIniciales" class="form-control col-xs-10 col-sm-1" style="border-color: #72aec2;" />
			        </div>
                    <label class="col-sm-2 control-label"> * únicas en el sistema </label>
		        </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Activo </label>
                    <div class="col-sm-2">
					    <label>
					        <input id="checkActivo" name="switchMuerto" class="ace ace-switch ace-switch-6" type="checkbox" checked runat="server"/>
						    <span class="lbl"></span>
					    </label>
				    </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- Botones -->
                <div class="form-group">
                    <div class="col-md-offset-3 col-md-9">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Guardar" OnClick="btn_GuardarEvento" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>
