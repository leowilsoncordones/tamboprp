<%@ Page Title="tamboprp | ayuda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="tamboprp.Help" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-wrench"></i> Ayuda y soporte</h1>
    </div>
    
    <!-- comienza tabbable -->
    <div class="tabbable">
		<ul class="nav nav-tabs" id="myTab">
			<li class="active"><a data-toggle="tab" href="#faqs"><i class="blue ace-icon fa fa-question bigger-120"></i> FAQs </a></li>
            <li><a data-toggle="tab" href="#casoSoporte"><i class="blue ace-icon fa fa-wrench bigger-120"></i> Caso de soporte <asp:Label ID="badgeCantOrdene" runat="server" cssClass="badge badge-success"></asp:Label></a></li>
		</ul>
        <!-- comienza contenido de tabbable -->
		<div class="tab-content">
		    
		    <!-- PESTANA 1: FAQs -->
			<div id="faqs" class="tab-pane fade in active">
			    <div class="page-header">
                    <h1><i class="menu-icon fa fa-question"></i> FAQs <small><i class="ace-icon fa fa-angle-double-right"></i> preguntas frecuentes</small></h1>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        
                        <!-- ACCORDION faq-tab-1 -->
                        <div id="faq-tab-1" class="tab-pane fade in active">
							<h4 class="blue"><i class="ace-icon fa fa-check bigger-110"></i> Preguntas generales</h4>
                            <div class="space-8"></div>
                            <div id="faqList" class="panel-group accordion-style1 accordion-style2" runat="server">
                                <!-- LISTA DE FAQS -->
							</div>
						</div>
                        <!-- fin de ACCORDION faq-tab-1  -->
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
                <hr />
                <div class="row">
                <div class="col-md-10">
                    <p>
                        Puede visitar nuestro <span><a href="http://blog.tamboprp.uy">blog</a></span> donde encontrará 
                        más información sobre ayuda y ver consultas hechas por otros usuarios de la comunidad.<br/>
                        Si no encuentra respuestas a sus dudas por estos canales, puede crear un caso de soporte y 
                        le ayudaremos a la mayor brevedad posible.
                    </p>
                </div>
                <div class="col-md-2">
                </div>
            </div>
			</div> <!-- fin PESTANA 1 -->
            <!-- PESTANA 2: Caso de soporte -->
			<div id="casoSoporte" class="tab-pane fade">
			    <div class="page-header">
			        <h1><i class="menu-icon fa fa-wrench"></i> Caso de soporte</h1>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div id="formulario" class="form-horizontal">
                            <!-- NOMBRE y APELLIDO, EMAIL-->
                            <div class="form-group">
		                        <label class="col-sm-3 control-label no-padding-right"> Nombre y Apellido </label>
			                    <div class="col-sm-3">
			                        <input type="text" runat="server" id="fNomApe" required class="form-control col-xs-10 col-sm-5" />
			                    </div>
                                <label class="col-sm-1 control-label no-padding-right"> Email </label>
                                <div class="col-sm-3">
			                        <input type="email" runat="server" id="fEmail" placeholder="Ej. usuario@dominio.com" class="form-control col-xs-10 col-sm-5" />
			                    </div>
		                    </div>
                            <!-- ESTABLECIMIENTO Y TELEFONO DE CONTACTO -->
                            <div class="form-group">
		                        <label class="col-sm-3 control-label no-padding-right"> Establecimiento </label>
			                    <div class="col-sm-3">
			                        <input type="text" runat="server" id="fEstablecimiento" class="form-control col-xs-10 col-sm-5" />
			                    </div>
                                <label class="col-sm-1 control-label no-padding-right"> Teléfono </label>
			                    <div class="col-sm-3">
			                        <input type="text" runat="server" id="fTelef" class="form-control col-xs-10 col-sm-5" />
			                    </div>
		                    </div>
                            <hr/>
                            <!-- TITULO -->
                            <div class="form-group">
		                        <label class="col-sm-3 control-label no-padding-right"> Título de la consulta </label>
			                    <div class="col-sm-4">
			                        <input type="text" runat="server" id="fTitulo" required class="form-control col-xs-10 col-sm-5" />
			                    </div>
                                <div class="col-sm-12"></div>
		                    </div>
                            <!-- GENERAL - Comentarios -->
                            <div class="form-group">
			                    <label class="col-sm-3 control-label no-padding-right "> Tipo de problema </label>
			                    <div class="col-sm-4">
			                        <asp:DropDownList ID="ddlTipo" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="False" runat="server" ></asp:DropDownList>
			                    </div>
                                <div class="col-sm-12"></div>
		                    </div>
                            <!-- DESCRIPCION -->
                            <div class="form-group">
			                    <label class="col-sm-3 control-label no-padding-right"> Descripción del problema </label>
			                    <div class="col-sm-6">
			                        <textarea class="form-control" runat="server" id="fComentario" required rows="5" ></textarea>
			                    </div>
                                <div class="col-sm-12"></div>
		                    </div>
                            <div class="form-group">
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-6">
                                    <small>
                                        Proporciona una descripción detallada del problema que has tenido. <br/>
                                        Cualquier detalle ayuda (como las últimas acciones que has realizado antes de que se produjera el problema).
                                    </small>
                                </div>
                            </div>
                            <!-- Botones -->
                            <div class="form-group">
                                <div class="col-md-offset-3 col-md-9">
                                    <a href="#sendModal" role="button" id="btnSubmit" class="btn btn-info" data-toggle="modal" OnClick="setBodyModal()">Enviar</a>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				                </div>
                            </div>
                        </div>
                     </div>
                </div>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div> <!-- fin PESTANA 2 -->
        </div>
	</div> <!-- fin tabbable -->
    
    <!-- CONFIRMATION MODAL -->
    
    <div id="sendModal" class="modal fade">
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
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_EnviarFormulario" />
                </div>
            </div>
        </div>
    </div>    
    
    <script>
        function setBodyModal() {
            var bodySaveModal = document.getElementById("bodySaveModal");
            bodySaveModal.innerHTML = "Seguro que desea enviar un caso de soporte?";
        }
    </script>

    <!-- FINAL MODAL -->
    
    
</asp:Content>
