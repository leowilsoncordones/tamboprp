<%@ Page Title="tamboprp | remito a planta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoRemito.aspx.cs" Inherits="tamboprp.NuevoRemito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <link href="css/datepicker.css" rel="stylesheet" />

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
        <h1><i class="menu-icon fa fa-edit"></i> Ingreso de un nuevo remito a planta </h1>
    </div>

    <div class="row">
        <div class="col-sm-12">
            
            <!-- FORMULARIO -->
            <div id="formulario" class="form-horizontal">
                <!-- GENERAL - Fecha -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Fecha </label>
					<div class="col-sm-2">
						<div class="input-group date">
						    <input type="text" id="mydate" name="mydate" class="form-control col-xs-10 col-sm-5" style="border-color: #72aec2;"/>
							<span class="input-group-addon"><i class="ace-icon fa fa-calendar"></i></span>
						</div>
					</div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- Empresa, x defecto la actual -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Empresa </label>
			        <div class="col-sm-3">
			            <asp:DropDownList ID="ddlEmpresa" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged" runat="server" style="border-color: #72aec2;" ></asp:DropDownList>
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- FACTURA - Serie -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Factura </label>
			        <div class="col-sm-1">
			            <input type="text" runat="server" id="fFactSerie" placeholder="A" class="form-control col-xs-10 col-sm-5" style="border-color: #72aec2;" />
			        </div>
                    <div class="col-sm-2">
                        <input type="text" runat="server" id="fFactNum" placeholder="123456" class="form-control col-xs-10 col-sm-5" style="border-color: #72aec2;" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- MATRICULA -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Matrícula </label>
                    <div class="col-sm-2">
			            <input type="text" runat="server" id="fMatricula" placeholder="101" value="101" class="form-control col-xs-10 col-sm-5" />
			        </div>
                </div>
                <!-- LITROS -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Litros </label>
                    <div class="col-sm-2">
			            <input type="text" runat="server" id="fLitros" placeholder="" class="form-control col-xs-10 col-sm-5" style="border-color: #72aec2;" />
			        </div>
                </div>
                <!-- TEMPERATURAS --> <!-- chequear que en los decimales ingresaen con coma -->
                 <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Temperatura 1 </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fTemp1" placeholder="" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Temperatura 2 </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fTemp2" placeholder="" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- SERVICIO -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Encargado </label>
			        <div class="col-sm-3">
			            <input type="text" runat="server" id="fEncargado" placeholder="Ej. Jesús" class="form-control ui-autocomplete-input col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- OBSERVACIONES -->
                <div class="form-group">
			        <label class="col-sm-3 control-label no-padding-right"> Observaciones </label>
			        <div class="col-sm-5">
			            <textarea class="form-control" id="fObservaciones" rows="4" runat="server"></textarea>
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- Botones -->
                <div class="form-group">
                    <div class="col-md-offset-3 col-md-9">
                        <a href="#saveModal" role="button" id="id-btn-save" class="btn btn-info" data-toggle="modal" OnClick="setBodyModal()">Guardar</a>
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
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_GuardarEvento" />
                </div>
            </div>
        </div>
    </div>    
    
    <script>
        function setBodyModal() {
            var fecha = document.getElementById("mydate").value;
            var bodySaveModal = document.getElementById("bodySaveModal");
            bodySaveModal.innerHTML = "Seguro que desea guardar un remito a planta correspondiente al día " + fecha + "?";
        }
    </script>

    <!-- FINAL MODAL -->
    
    <script src="js/date-time/bootstrap-datepicker.js"></script>
    <script>

        //   ---------DATEPICKER----------   //

        $("#mydate").datepicker({
            autoclose: true,
            todayHighlight: true
        });
        $("#mydate").datepicker('setDate', new Date());
    </script>
    

</asp:Content>
