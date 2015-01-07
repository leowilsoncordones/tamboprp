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
                <img src="img_tamboprp/corporativo/logo.png" alt="Tambo y Cabaña 'El Grillo'" title="Tambo y Cabaña 'El Grillo'" />
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
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Guardar" OnClick="btn_GuardarEvento" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				    </div>
                </div>
            </div>
        </div>
        
    </div>
</asp:Content>
