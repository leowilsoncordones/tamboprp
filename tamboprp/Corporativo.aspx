<%@ Page Title="tamboprp | corporativo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Corporativo.aspx.cs" Inherits="tamboprp.Corporativo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-book"></i> Datos Corporativos</h1>
    </div>
    <br/>
    <div class="row">
        <!-- COLUMNA - LOGO -->
        <div class="col-sm-3">
            <p class="align-center">
                <a href="#cambiarImagen" id="id-btn-ModifData" role="button" data-toggle="modal" >
                    <img src="img_tamboprp/corporativo/logo.png" alt="Tambo y Cabaña 'El Grillo'" title="Tambo y Cabaña 'El Grillo'" />
                </a>
            </p>
        </div>
        <div class="col-sm-9">
            <!-- FORMULARIO EMPRESA -->
            <div id="formulario" class="form-horizontal">
                <!-- Nombre, Razon Social y Rut -->
                <div class="form-group">
		            <label class="col-sm-2 control-label no-padding-right"> Nombre </label>
			        <div class="col-sm-4">
			            <input type="text" runat="server" id="fNombre" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label no-padding-right"> Razón Social </label>
			        <div class="col-sm-4">
			            <input type="text" runat="server" id="fRazonSocial" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label no-padding-right"> R.U.T. </label>
			        <div class="col-sm-4">
			            <input type="text" runat="server" id="fRut" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- Letra en el sistema -->
                <div class="form-group">
                    <label class="col-sm-2 control-label no-padding-right"> Código </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="FLetraSistema" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- Contacto -->
                <div class="form-group">
		            <label class="col-sm-2 control-label no-padding-right"> Dirección </label>
			        <div class="col-sm-5">
			            <input type="text" runat="server" id="fDireccion" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
		            <label class="col-sm-2 control-label no-padding-right"> Ciudad </label>
			        <div class="col-sm-5">
			            <input type="text" runat="server" id="fCiudad" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label no-padding-right"> C.P. </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fCP" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label no-padding-right"> Teléfono </label>
			        <div class="col-sm-3">
			            <input type="text" runat="server" id="fTelefono" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label no-padding-right"> Celular </label>
			        <div class="col-sm-3">
			            <input type="text" runat="server" id="fCelular" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label no-padding-right"> Web </label>
			        <div class="col-sm-5">
			            <input type="text" runat="server" id="fWeb" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- Botones -->
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-9">
                        <a href="#saveModal" role="button" id="btnSubmit" class="btn btn-info" data-toggle="modal" OnClick="setBodyModal()">Guardar</a>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    </div>
    
    <!-- CONFIRMATION MODAL -->
    <div id="saveModal" class="modal fade">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header widget-header-small">
                    <h4 class="smaller"><i class="ace-icon fa fa-save"></i> Confirmar</h4>
                </div>
                <div class="modal-body">
                    <span id="bodySaveModal" class="text-warning">
                        
                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_GuardarEvento" />
                </div>
            </div>
        </div>
    </div>    
    
    <script>
        function setBodyModal() {
            var bodySaveModal = document.getElementById("bodySaveModal");
            bodySaveModal.innerHTML = "Seguro que desea guardar o modificar los datos corporativos?";
        }
    </script>
    <!-- FINAL MODAL -->
    
    <!-- CAMBIAR IMAGEN MODAL -->
    <div id="cambiarImagen" class="modal fade">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-image"></i> Cambiar imagen</h4>
                </div>
                <div class="modal-body">
                    <label class="ace-file-input ace-file-multiple">
                        <input type="file" name="file-input">
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
