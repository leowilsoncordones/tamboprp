<%@ Page Title="tamboprp | notificaciones y alertas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="tamboprp.Notificaciones" %>
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
        <h1><i class="menu-icon fa fa-newspaper-o"></i> Notificaciones y alertas</h1>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div>
                <asp:GridView ID="gvReportes" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Titulo" HeaderText="Titulo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Envio" HeaderText="Envío" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                </Columns>
                <FooterStyle />
                <PagerSettings />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
                </asp:GridView>
            </div>
        </div>
        <!-- RESUMEN EN COLUMNA DERECHA -->
        <div class="col-md-4">
            <div class="well">
                <h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Resumen</h4>
                <ul class="list-unstyled spaced2">
                    <li class="bigger-110">
                        <a href="#selDest" role="button" id="id-btn-SelDest" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-send"></i> Seleccionar destinatarios</a>
                    </li>
                    <li class="bigger-110">
                        <a href="#defProg" role="button" id="id-btn-defProg" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-clock-o"></i> Definir programación</a>
                    </li>
                    <li class="bigger-110">
                        <a href="#reporteSemanal" role="button" id="id-btn-VerSemanal" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-newspaper-o"></i> Resumen operativo</a>
                    </li>
                    <li class="bigger-110">
                        <a href="#" role="button" id="id-btn-VerMensual" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-newspaper-o"></i> Informe cierre de mes</a>
                    </li>
                </ul>
                <hr/>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
			</div>
        </div>
    </div>
    
    <!-- EJEMPLO REPORTE MODAL -->
    <div id="reporteSemanal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body" id="bodyModal" runat="server">
                    
                    <!-- EJEMPLO DE MAIL -->

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnPrueba" runat="server" CssClass="btn btn-sm btn-info" Text="Enviar prueba" OnClick="btn_EnviarPrueba" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->

    <!-- SELECCIONAR MODAL -->
    <div id="selDest" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-send"></i> Seleccione destinatarios a enviarle el reporte por mail</h4>
                </div>
                <div class="modal-body">
                    <span id="bodyDeleteModal" class="text-warning center">
                        <div id="formularioDelete" class="form-horizontal">
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right h4 smaller lighter blue"> Resumen operativo </label>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right"> </label>
			                    <div class="col-sm-6 align-left">
                                    <asp:CheckBoxList ID="cboxUsuarios1" runat="server"></asp:CheckBoxList>
			                    </div>
		                    </div>
                            <hr/>
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right h4 smaller lighter blue"> Informe cierre de mes </label>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right"> </label>
			                    <div class="col-sm-6 align-left">
                                    <asp:CheckBoxList ID="cboxUsuarios2" runat="server"></asp:CheckBoxList>
			                    </div>
		                    </div>
                        </div>
                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_SeleccionarDestinatarios" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL SELECCIONAR -->
    
    <!-- SELECCIONAR MODAL -->
    <div id="defProg" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-clock-o"></i> Seleccione programación de envío</h4>
                </div>
                <div class="modal-body">
                    <span id="bodyDeleteModalProg" class="text-warning center">
                        <div id="formularioDeleteProg" class="form-horizontal">
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right"> Reporte </label>
			                    <div class="col-sm-6">
			                        <asp:DropDownList ID="ddlReportesProg" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="False" runat="server" ></asp:DropDownList>
			                    </div>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right"> Seleccione día </label>
			                    <div class="col-sm-6 align-left">
                                    <asp:DropDownList ID="ddlDias" CssClass="form-control col-xs-10 col-sm-5" runat="server" ></asp:DropDownList>
			                    </div>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right"> Seleccione frecuencia </label>
			                    <div class="col-sm-6 align-left">
                                    <asp:DropDownList ID="ddlFrecuencia" CssClass="form-control col-xs-10 col-sm-5" runat="server" ></asp:DropDownList>
			                    </div>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right"> Nota </label>
			                    <div class="col-sm-6 align-left">
                                    <small class="text-info">En la frecuencia mensual, el envío se hará el día seleccionado en su última ocurrencia mensual.</small>
			                    </div>
		                    </div>
                        </div>
                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnProg" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_ProgReporte" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL SELECCIONAR -->


</asp:Content>
