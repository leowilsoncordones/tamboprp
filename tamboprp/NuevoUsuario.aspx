<%@ Page Title="tamboprp | nuevo usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoUsuario.aspx.cs" Inherits="tamboprp.NuevoUsuario" %>
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
        <h1><i class="menu-icon fa fa-user"></i> Ingreso de un nuevo usuario </h1>
    </div>

    <div class="row">
        <div class="col-sm-12">
            
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
                <!-- Cargar foto -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Foto </label>
                    <div class="col-sm-3">
                        <label class="ace-file-input"><input type="file" id="id-input-file-2">
                            <span class="ace-file-container" data-title="Elegir">
                                <span class="ace-file-name" data-title="..."><i class=" ace-icon fa fa-upload"></i></span>
                            </span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a></label>
                    </div>
                    <div class="col-sm-12"></div>
		         </div>
                 <!-- Email -->
                 <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Email </label>
                    <div class="col-sm-3">
			            <input type="email" runat="server" id="fEmail" placeholder="Ej. usuario@dominio.com" class="form-control col-xs-10 col-sm-5" />
			        </div>
                </div>
                <!-- Rol de usuario -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Rol de usuario </label>
			        <div class="col-sm-3">
			            <asp:DropDownList ID="ddlRolUsuario" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="True" OnSelectedIndexChanged="ddlRolUsuario_SelectedIndexChanged" runat="server" ></asp:DropDownList>
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- Nickname y Password-->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Usuario </label>
			        <div class="col-sm-3">
			            <input type="text" runat="server" id="username" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Contraseña </label>
                    <div class="col-sm-3">
			            <input type="password" runat="server" id="password" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <!-- Chequeo de fortaleza de password-->
                    <div id="messages" class="col-sm-3">
                        <!-- string password checker -->
                    </div>
		        </div>
                <!-- Recomendación -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Importante! </label>
			        <div class="col-sm-6">
			            <small>Por su seguridad, utilice contraseñas de al menos 8 caracteres de largo.<br/> 
                            Combine el uso de mayúsculas, minúsculas, números y caracteres especiales.</small>
			        </div>
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
    </div>

</asp:Content>
