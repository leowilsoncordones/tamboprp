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
    
    <script>
        $('#fileInput').ace_file_input();
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />  
    <div class="page-header">
        <h1><i class="menu-icon fa fa-user"></i> Ingreso de un nuevo usuario </h1>
    </div>
    <br/>
    <div class="row">
        
        <div class="col-xs-12 col-sm-3 center">
	    <div>
		    <!-- #section:pages/profile.picture -->
		    <span class="profile-picture">
		        <a href="#cambiarImagen" id="id-btn-ModifData" role="button" data-toggle="modal" >
                    <img id="avatar" runat="server" class="editable img-responsive editable-click editable-empty" alt="Mi Foto" src="avatars/user_silhouette.png" style="display: block;" />
                </a>
		    </span>

		    <!-- /section:pages/profile.picture -->
		    <div class="space-4"></div>

		    <div class="width-80 label label-info label-xlg arrowed-in arrowed-in-right">
			    <div class="inline position-relative">
				    <span class="white" id="fNomFoto" runat="server">Foto</span>
			    </div>
		    </div>
	    </div>

	    <div class="space-6"></div>
        </div>

        <div class="col-sm-9">
            
            <!-- FORMULARIO -->
            <div id="formulario" class="form-horizontal">
                <!-- Nombre y Apellido -->
                <div class="form-group">
		            <label class="col-sm-2 control-label no-padding-right"> Nombre </label>
			        <div class="col-sm-4">
			            <input type="text" runat="server" id="fNombre" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label no-padding-right"> Apellido </label>
			        <div class="col-sm-4">
			            <input type="text" runat="server" id="fApellido" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                 <!-- Email -->
                 <div class="form-group">
                    <label class="col-sm-2 control-label no-padding-right"> Email </label>
                    <div class="col-sm-4">
			            <input type="email" runat="server" id="fEmail" placeholder="Ej. usuario@dominio.com" class="form-control col-xs-10 col-sm-5" />
			        </div>
                </div>
                <!-- Rol de usuario -->
                <div class="form-group">
		            <label class="col-sm-2 control-label no-padding-right"> Rol de usuario </label>
			        <div class="col-sm-4">
			            <asp:DropDownList ID="ddlRolUsuario" CssClass="form-control col-xs-10 col-sm-5" OnSelectedIndexChanged="ddlRolUsuario_SelectedIndexChanged" runat="server" ></asp:DropDownList>
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- Nickname y Password-->
                <div class="form-group">
		            <label class="col-sm-2 control-label no-padding-right"> Usuario </label>
			        <div class="col-sm-4">
			            <input type="text" runat="server" id="username" required class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
		            <label class="col-sm-2 control-label no-padding-right"> Contraseña </label>
                    <div class="col-sm-4">
			            <input type="password" runat="server" id="password" required class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <!-- Chequeo de fortaleza de password-->
                    <div id="messages" class="col-sm-3">
                        <!-- string password checker -->
                    </div>
		        </div>
                <!-- Recomendación -->
                <div class="form-group">
		            <label class="col-sm-2 control-label no-padding-right"> Importante! </label>
			        <div class="col-sm-6">
			            <small>Por su seguridad, utilice contraseñas de al menos 8 caracteres de largo.<br/> 
                            Combine el uso de mayúsculas, minúsculas, números y caracteres especiales.</small>
			        </div>
		        </div>
                <!-- Botones -->
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-9">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Guardar" OnClick="btn_GuardarEvento" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>

    <!-- CAMBIAR IMAGEN MODAL -->
    <div id="cambiarImagen" class="modal fade">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-image"></i> Cambiar imagen</h4>
                </div>
                <div class="modal-body">
                    <label class="ace-file-input ace-file-multiple">
                        <asp:FileUpload ID="fupFoto" runat="server" />
                        <span class="ace-file-container" data-title="Click para elegir una nueva imagen">
                            <span class="ace-file-name" data-title="No File ..."><i class=" ace-icon ace-icon fa fa-picture-o"></i></span>
                        </span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a>
                    </label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCambiarImg" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_CambiarImagen" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->

</asp:Content>
