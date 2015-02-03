<%@ Page Title="tamboprp | nuevo evento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoEvento.aspx.cs" Inherits="tamboprp.NuevoEvento" %>
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
    
    <script src="js/ace/ace.searchbox-autocomplete.js"></script>
    
    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/excanvas.js"></script>
    <script src="js/bootstrap.js"></script>
    
    <script src="js/ace/elements.typeahead.js"></script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />  
    <div class="page-header">
        <h1><i class="menu-icon fa fa-edit"></i> Ingreso de un nuevo evento </h1>
    </div>

    <div class="row">
        <div class="col-sm-12">
            
            <!-- Registro -->
            <div id="formulario" class="form-horizontal">
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Seleccione tipo de evento </label>
			        <div class="col-sm-3">
			            <asp:DropDownList ID="ddlEvento" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="True" OnSelectedIndexChanged="ddlEvento_SelectedIndexChanged" runat="server" ></asp:DropDownList>
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- GENERAL - Registro -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Registro </label>
			        <div class="col-sm-2">
			            <%--<input type="text" runat="server" id="fRegistro" placeholder="Registro" class="form-control col-xs-10 col-sm-5" />--%>
                        <asp:TextBox runat="server" id="fRegistro" name="fRegistro" class="form-control col-xs-10 col-sm-5"  OnTextChanged="EventosRegistro" AutoPostBack="True"/>                                            
			        </div>
                    <label runat="server" id="lblRegistro" class="col-sm-3 control-label text-warning" ></label>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- GENERAL - Fecha -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Fecha </label>
					<div class="col-sm-2">
						<div class="input-group date">
						    <input type="text" id="mydate" name="mydate" class="form-control col-xs-10 col-sm-5"/>
							<span class="input-group-addon"><i class="ace-icon fa fa-calendar"></i></span>
						</div>
                        <%--<asp:TextBox ID="DateTextBox" runat="server"  />
                        <asp:Image ID="Image1" runat="server" ImageUrl="Images/CalendarIcon3.png" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                            TargetControlID="DateTextBox" PopupButtonID="Image1">
                        </ajaxToolkit:CalendarExtender>--%>
					</div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- ABORTO -->
                <asp:Panel ID="pnlAborto" runat="server">
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Registro servicio </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fRegistroServ" readonly placeholder="Registro Servicio" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                </asp:Panel>
                <!-- CALIFICACIONES -->
                <asp:Panel ID="pnlCalif" runat="server">
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Letras </label>
                    <div class="col-sm-2">
                        <select id="selectLetras" name="selectLetras" class="form-control col-xs-10 col-sm-5" onchange="cargaSelectNumeros()">
			            </select>
			            <!--<asp:DropDownList ID="ddlCalificacion" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="True" OnSelectedIndexChanged="ddlCalif_SelectedIndexChanged" runat="server" ></asp:DropDownList>-->
			        </div>
                </div>
                <div class="form-group" id="dCalifPuntos">
                    <label class="col-sm-3 control-label no-padding-right"> Puntos </label>
                    <div class="col-sm-2">
                        <select id="selectNumeros" name="selectNumeros" class="form-control col-xs-10 col-sm-5">
			            </select>
			            <!--<asp:DropDownList ID="ddlCalificacionPts" CssClass="form-control col-xs-10 col-sm-5" runat="server" ></asp:DropDownList>-->
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                </asp:Panel>
                <!-- CONTROLES -->
                <asp:Panel ID="pnlControles" runat="server">
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Leche </label>
                    <div class="col-sm-2">
			            <%--<input type="text" runat="server" id="fControl" placeholder="Leche en kilos" class="form-control col-xs-10 col-sm-5" />--%>
                        <asp:TextBox runat="server" id="fLecheControl" class="form-control col-xs-10 col-sm-5"  OnTextChanged="EventosRegistro" AutoPostBack="True"/>
			        </div>
                    <label runat="server" id="lblLecheControl" class="col-sm-3 control-label text-warning" ></label>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Porcentaje de Grasa </label>
                    <div class="col-sm-2">
			            <%--<input type="text" runat="server" id="fGrasa" placeholder="% Grasa" class="form-control col-xs-10 col-sm-5" />--%>
                        <asp:TextBox runat="server" id="fGrasaControl" class="form-control col-xs-10 col-sm-5"  OnTextChanged="EventosRegistro" AutoPostBack="True"/>
			        </div>
                    <label runat="server" id="lblGrasaControl" class="col-sm-3 control-label text-warning" ></label>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Días Lactancia </label>
                    <div class="col-sm-2">
			            <input type="text" readonly runat="server" id="fLeche" placeholder="" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                </asp:Panel>
                <!-- DIAGNOSTICO -->
                <asp:Panel ID="pnlDiagnostico" runat="server">
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Diagnóstico </label>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlDiagnostico" CssClass="form-control col-xs-10 col-sm-5" runat="server" ></asp:DropDownList>
			        </div>
                </div>
                </asp:Panel>
                <!-- SECADO -->
                <asp:Panel ID="pnlSecado" runat="server">
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Motivo de secado </label>
                    <div class="col-sm-3">
			            <asp:DropDownList ID="ddlMotivoSec" AutoPostBack="True" CssClass="form-control col-xs-10 col-sm-5" OnSelectedIndexChanged="ddlMotivoSec_SelectedIndexChanged" runat="server" ></asp:DropDownList>
			        </div>
                </div>
                </asp:Panel>
                <!-- BAJAS -->
                <asp:Panel ID="pnlBajas" runat="server">
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Enfermedad </label>
			        <%--<div class="col-sm-3">
			            <input type="text" runat="server" id="fEnfermedad" placeholder="Enfermedad" class=" form-control col-xs-10 col-sm-5"/>
			        </div>--%>
                    <div class="col-sm-3" id="inputEnfermedad">
			            <input type="text"  id="inputEnf" placeholder="Enfermedad" class="typeahead form-control col-xs-10 col-sm-5" onblur="checkEnfVacia()"/>
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                </asp:Panel>
                <!-- SERVICIO -->
                <asp:Panel ID="pnlServicio" runat="server">
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Monta natural </label>
                    <div class="col-sm-2">
					    <label>
					        <input id="checkMontaNat" name="switchMontaNat" class="ace ace-switch ace-switch-6" type="checkbox" runat="server"/>
						    <span class="lbl"></span>
					    </label>
				    </div>
                    <div class="col-sm-12"></div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Registro padre </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fRegPadre" placeholder="Registro padre" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <label runat="server" id="lblRegPadre" class="col-sm-3 control-label text-warning" ></label>
                    <div class="col-sm-12"></div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Inseminador </label>
			        <div  class="col-sm-3">
			            <select id="selectEmpleados" name="selectEmpleados" class="form-control col-xs-10 col-sm-5">
			            </select>
			            <%--<input type="text" runat="server" id="fInseminador" placeholder="Inseminador" class="form-control ui-autocomplete-input col-xs-10 col-sm-5" />--%>
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                </asp:Panel>
                <!-- CONCURSO -->
                <asp:Panel ID="pnlConcurso" runat="server">
                 <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Nombre de concurso / Lugar </label>
                    <div class="col-sm-3">
			            <asp:DropDownList ID="ddlNomConcurso" CssClass="form-control col-xs-10 col-sm-5" runat="server" ></asp:DropDownList>
			        </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Categoría de concurso </label>
                    <div class="col-sm-3">
			            <asp:DropDownList ID="ddlCategConcurso" CssClass="form-control col-xs-10 col-sm-5" runat="server" ></asp:DropDownList>
			        </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Premio </label>
			        <div class="col-sm-5">
			            <input type="text" runat="server" id="fPremio" placeholder="Ej. Gran Campeona" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                </asp:Panel>
                <!-- GENERAL - Comentarios -->
                <div class="form-group">
			        <label class="col-sm-3 control-label no-padding-right"> Comentarios </label>
			        <div class="col-sm-5">
			            <textarea class="form-control" id="fComentario" rows="3" runat="server"></textarea>
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- Botones -->
                <div class="form-group">
                    <div class="col-md-offset-3 col-md-9">
                        <%--<asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" Text="Guardar" OnClick="btn_GuardarEvento" />--%>
                        <a href="#modalGuardar" class="btn btn-info" id="btnGuardar" data-toggle="modal">Guardar</a>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				    </div>
                    <%--<a href="#modalGuardar" role="button" id="id-btn-VerMensual" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-newspaper-o"></i> TEST</a>--%>
                </div>
            </div>
        </div>
    </div>
    
    
    <!-- EJEMPLO GUARDAR MODAL -->
    <div id="modalGuardar" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-pencil"></i> Ingresar evento</h4>
                    <h6>Confirme si desea guardar los siguientes datos</h6>
                </div>
                <div class="modal-body" id="bodyModal" runat="server">
                    <div class="form-horizontal">
                        
                    <div class="form-group">
                        <label id="lblModalEvento" class="col-sm-4 control-label no-padding-right">Evento: </label>
                        <div class="col-sm-5">
                            <label id="txtModalEvento" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group">
                        <label id="lblModalRegistro" class="col-sm-4 control-label no-padding-right">Registro: </label>
                        <div class="col-sm-5">
                            <label id="txtModalRegistro" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group">
                        <label id="lblModalFecha" class="col-sm-4 control-label no-padding-right">Fecha: </label>
                        <div class="col-sm-5">
                            <label id="txtModalFecha" class="control-label no-padding-left"></label>
                        </div>
                    </div>
                    
                    
                    <div class="form-group" id="lblModalSecadoMotivo">
                        <label class="col-sm-4 control-label no-padding-right">Motivo secado: </label>
                        <div class="col-sm-5">
                            <label id="txtModalSecadoMotivo" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalEnfermedad">
                        <label class="col-sm-4 control-label no-padding-right">Enfermedad: </label>
                        <div class="col-sm-5">
                            <label id="txtModalEnfermedad" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalRegServ">
                        <label class="col-sm-4 control-label no-padding-right">Registro servicio: </label>
                        <div class="col-sm-5">
                            <label id="txtModalRegServ" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalCalifLetras">
                        <label class="col-sm-4 control-label no-padding-right">Letras: </label>
                        <div class="col-sm-5">
                            <label id="txtModalCalifLetras" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalCalifPuntos">
                        <label class="col-sm-4 control-label no-padding-right">Puntos: </label>
                        <div class="col-sm-5">
                            <label id="txtModalCalifPuntos" class="control-label no-padding-left"></label>
                        </div>
                    </div>
                    
                    <div class="form-group" id="lblModalConcNombre">
                        <label class="col-sm-4 control-label no-padding-right">Nombre Concurso: </label>
                        <div class="col-sm-5">
                            <label id="txtModalConcNombre" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalConcCateg">
                        <label class="col-sm-4 control-label no-padding-right">Categoria: </label>
                        <div class="col-sm-5">
                            <label id="txtModalConcCateg" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalConcPremio">
                        <label class="col-sm-4 control-label no-padding-right">Premio: </label>
                        <div class="col-sm-5">
                            <label id="txtModalConcPremio" class="control-label no-padding-left"></label>
                        </div>
                    </div>
                    
                    <div class="form-group" id="lblModalContProdLeche">
                        <label class="col-sm-4 control-label no-padding-right">Leche: </label>
                        <div class="col-sm-5">
                            <label id="txtModalContProdLeche" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalContProdGrasa">
                        <label class="col-sm-4 control-label no-padding-right">Grasa: </label>
                        <div class="col-sm-5">
                            <label id="txtModalContProdGrasa" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalContProdDias">
                        <label class="col-sm-4 control-label no-padding-right">Dias: </label>
                        <div class="col-sm-5">
                            <label id="txtModalContProdDias" class="control-label no-padding-left"></label>
                        </div>
                    </div>
                    
                    <div class="form-group" id="lblModalDiagPrenezDiag">
                        <label class="col-sm-4 control-label no-padding-right">Diagnostico: </label>
                        <div class="col-sm-5">
                            <label id="txtModalDiagPrenezDiag" class="control-label no-padding-left"></label>
                        </div>
                    </div>
                    
                    <div class="form-group" id="lblModalServMonta">
                        <label class="col-sm-4 control-label no-padding-right">Monta natural: </label>
                        <div class="col-sm-5">
                            <label id="txtModalServMonta" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalServRegPadre">
                        <label class="col-sm-4 control-label no-padding-right">Registro padre: </label>
                        <div class="col-sm-5">
                            <label id="txtModalServRegPadre" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalServInsem">
                        <label class="col-sm-4 control-label no-padding-right">Inseminador: </label>
                        <div class="col-sm-5">
                            <label id="txtModalServInsem" class="control-label no-padding-left"></label>
                        </div>
                    </div>
                    
                    

                    <div class="form-group">
                        <label id="lblModalComentarios" class="col-sm-4 control-label no-padding-right">Comentarios: </label>
                        <div class="col-sm-5">
                            <label id="txtModalComentarios" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    </div>
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnPrueba" runat="server" CssClass="btn btn-sm btn-info" Text="Confirmar" OnClick="btn_GuardarEvento" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->
    


    <script src="js/bloodhound.js"></script>
    <script src="js/date-time/bootstrap-datepicker.js"></script>
    <script src="js/typeahead.jquery.js"></script>
    <%--<script src="js/js_tamboprp/NuevoEvento.js"></script>--%>
    
    <script text="javascript">
        
        //
        $(document).on("click", "#btnGuardar", function () {
            var myBookId = $("#fRegistro").val();
            var ddlReport = document.getElementById("<%=ddlEvento.ClientID%>");
            var valorEvento = ddlReport.options[ddlReport.selectedIndex].value;

            document.getElementById('txtModalEvento').innerHTML = ddlReport.options[ddlReport.selectedIndex].text;
            document.getElementById('txtModalRegistro').innerHTML = document.getElementById('<%=fRegistro.ClientID%>').value;
            document.getElementById('txtModalFecha').innerHTML = document.getElementById('mydate').value;
            document.getElementById('txtModalComentarios').innerHTML = document.getElementById('<%=fComentario.ClientID%>').value;

            document.getElementById("lblModalSecadoMotivo").style.display = "none";
            document.getElementById("lblModalEnfermedad").style.display = "none";
            document.getElementById("lblModalRegServ").style.display = "none";
            document.getElementById("lblModalCalifLetras").style.display = "none";
            document.getElementById("lblModalCalifPuntos").style.display = "none";
            document.getElementById("lblModalConcNombre").style.display = "none";
            document.getElementById("lblModalConcCateg").style.display = "none";
            document.getElementById("lblModalConcPremio").style.display = "none";
            document.getElementById("lblModalContProdLeche").style.display = "none";
            document.getElementById("lblModalContProdGrasa").style.display = "none";
            document.getElementById("lblModalContProdDias").style.display = "none";
            document.getElementById("lblModalDiagPrenezDiag").style.display = "none";
            document.getElementById("lblModalServMonta").style.display = "none";
            document.getElementById("lblModalServRegPadre").style.display = "none";
            document.getElementById("lblModalServInsem").style.display = "none";
            

            //ABORTO 0
            if (valorEvento == 0) {
                document.getElementById("lblModalRegServ").style.display = "block";
                document.getElementById('txtModalRegServ').innerHTML = document.getElementById('<%=fRegistroServ.ClientID%>').value;
            }
            // CELO SIN SERV 2
            if (valorEvento == 2) {

            }

            //SERVICIO 3
            if (valorEvento == 3) {
                document.getElementById("lblModalServMonta").style.display = "block";
                document.getElementById("lblModalServRegPadre").style.display = "block";
                document.getElementById("lblModalServInsem").style.display = "block";
                document.getElementById('txtModalServMonta').innerHTML = document.getElementById('<%=checkMontaNat.ClientID%>').checked ? 'S' : 'N';
                document.getElementById('txtModalServRegPadre').innerHTML = document.getElementById('<%=fRegPadre.ClientID%>').value;
                var e = document.getElementById('selectEmpleados');
                var strUser = e.options[e.selectedIndex].text;
                document.getElementById('txtModalServInsem').innerHTML = strUser;
            }

            //SECADO 4
            if (valorEvento == 4) {
                document.getElementById("lblModalSecadoMotivo").style.display = "block";
                document.getElementById("lblModalEnfermedad").style.display = "block";
                document.getElementById('txtModalEnfermedad').innerHTML = enfermedadTest;
                var ddlm = document.getElementById("<%=ddlMotivoSec.ClientID%>");
                var valorSec = ddlm.options[ddlm.selectedIndex].text;
                document.getElementById('txtModalSecadoMotivo').innerHTML = valorSec;
            }

            //DIAG PRENEZ 7
            if (valorEvento == 7) {
                document.getElementById("lblModalDiagPrenezDiag").style.display = "block";
                var ddlDiag = document.getElementById("<%=ddlDiagnostico.ClientID%>");
                var valorDiag = ddlDiag.options[ddlDiag.selectedIndex].value;
                document.getElementById('txtModalDiagPrenezDiag').innerHTML = valorDiag;
            }

            //CONTROL PROD 8
            if (valorEvento == 8) {
                document.getElementById("lblModalContProdLeche").style.display = "block";
                document.getElementById("lblModalContProdGrasa").style.display = "block";
                document.getElementById("lblModalContProdDias").style.display = "block";
                document.getElementById('txtModalContProdLeche').innerHTML = document.getElementById('<%=fLecheControl.ClientID%>').value;
                document.getElementById('txtModalContProdGrasa').innerHTML = document.getElementById('<%=fGrasaControl.ClientID%>').value;
                document.getElementById('txtModalContProdDias').innerHTML = document.getElementById('<%=fLeche.ClientID%>').value;
            }

            // CALIFICACION 9
            if (valorEvento == 9) {
                document.getElementById("lblModalCalifLetras").style.display = "block";
                document.getElementById("lblModalCalifPuntos").style.display = "block";
                var letr = document.getElementById('selectLetras');
                var lert1 = letr.options[letr.selectedIndex].value;
                document.getElementById('txtModalCalifLetras').innerHTML = lert1;

                var punt = document.getElementById('selectNumeros');
                var punt1 = punt.options[punt.selectedIndex].value;
                document.getElementById('txtModalCalifPuntos').innerHTML = punt1;
            }

            //CONCURSO 10
            if (valorEvento == 10) {
                document.getElementById("lblModalConcNombre").style.display = "block";
                document.getElementById("lblModalConcCateg").style.display = "block";
                document.getElementById("lblModalConcPremio").style.display = "block";

                var ddlNom = document.getElementById("<%=ddlNomConcurso.ClientID%>");
                var valorNom = ddlNom.options[ddlNom.selectedIndex].text;
                document.getElementById('txtModalConcNombre').innerHTML = valorNom;

                var ddlCatConc = document.getElementById("<%=ddlCategConcurso.ClientID%>");
                var valorCatConc = ddlCatConc.options[ddlCatConc.selectedIndex].text;
                document.getElementById('txtModalConcCateg').innerHTML = valorCatConc;

                document.getElementById('txtModalConcPremio').innerHTML = document.getElementById('<%=fPremio.ClientID%>').value;
            }

            // BAJA 11 12
            if (valorEvento == 11 || valorEvento == 12) {
                document.getElementById("lblModalEnfermedad").style.display = "block";
                 document.getElementById('txtModalEnfermedad').innerHTML = enfermedadTest != undefined? enfermedadTest.replace(/"/g,'') : '-';
            }

            //document.getElementById("TestLeo").style.display = "none";
            //document.getElementById("idOfElement").style.display = "block";
        });

        var enfermedadTest;



//   ---------DATEPICKER----------   //

        $("#mydate").datepicker({
            autoclose: true,
            todayHighlight: true,
            format: 'dd/mm/yyyy'
        });
        $("#mydate").datepicker('setDate', new Date());


        //  ---------TYPEAHEAD-----------   //


        // ------------ typeahead ENFERMEDADES ------------- //

        function cargarTypeaheadEnfermedades() {
        
            var substringMatcherEnf = function (strs) {
                return function findMatches(q, cb) {
                    var matches, substrRegex;
                    matches = [];
                    substrRegex = new RegExp(q, 'i');
                    $.each(strs, function (i, str) {
                        if (substrRegex.test(str.Nombre_enfermedad)) {
                            matches.push({ Nombre_enfermedad: str.Nombre_enfermedad, Id:str.Id});
                        }
                    });
                    cb(matches);
                };
            };
      
            var listaTypeahead = [];
            GetListaEnf();
            function GetListaEnf() {
                PageMethods.GetEnfermedades(OnSuccess);
            }
            function OnSuccess(response) {
                var list = response;
                for (var i = 0; i < list.length; i++) {
                    var enfermedad = { Id: list[i].Id, Nombre_enfermedad: list[i].Nombre_enfermedad};
                    listaTypeahead.push(enfermedad);
                }
            }

            $("#inputEnf.typeahead").typeahead({
                hint: true,
                highlight: true,
                minLength: 2
            },
            {
                name: 'listaTypeahead',
                displayKey: 'Nombre_enfermedad',
                source: substringMatcherEnf(listaTypeahead)
            });

            var enfermedadSeleccionada = function (eventObject, suggestionObject, suggestionDataset) {
                var dato = JSON.stringify(suggestionObject.Id);
                enfermedadTest = JSON.stringify(suggestionObject.Nombre_enfermedad);
                pasarDatos(dato);
            };
            $("#inputEnf.typeahead").on('typeahead:selected', enfermedadSeleccionada);

            function pasarDatos(dato) {
                PageMethods.RecibirDato(dato, function (response) {  }, function (response) {});
            }

        }

        function checkEnfVacia() {
            var valor = $('#inputEnf').val();
            if ( valor == "") {
                PageMethods.RecibirDato(valor, function (response) { console.write(response); }, function (response) { console.write(response); });
            }
        }


        // ------------ typeahead REGISTROS ABORTOS ------------- //

        function cargarTypeaheadAbortos() {

            var substringMatcherAborto = function (strs) {
                return function findMatches(q, cb) {
                    var matches, substrRegex;
                    matches = [];
                    substrRegex = new RegExp(q, 'i');
                    $.each(strs, function (i, str) {
                        if (substrRegex.test(str.Nombre)) {
                            matches.push({ Nombre: str.Nombre });
                        }
                    });
                    cb(matches);
                };
            };

            var listaAbortos = [];
            GetListaAbortos();
            function GetListaAbortos() {
                PageMethods.GetAbortosAnimalesConServicios(OnSuccessAb);
            }
            function OnSuccessAb(response) {
                var list = response;
                for (var i = 0; i < list.length; i++) {
                    var aborto = { Nombre: list[i].Nombre };
                    listaAbortos.push(aborto);
                }
            }

            $("#inputAborto.typeahead").typeahead({
                hint: true,
                highlight: true,
                minLength: 2
            },
            {
                name: 'listaAbortos',
                displayKey: 'Nombre',
                source: substringMatcherAborto(listaAbortos)
            });

            var abortoSeleccionada = function (eventObject, suggestionObject, suggestionDataset) {
                //alert(JSON.stringify(suggestionObject));
                var dato = JSON.stringify(suggestionObject.Nombre);
                pasarDatos(dato);
            };
            $("#inputAborto.typeahead").on('typeahead:selected', abortoSeleccionada);

            function pasarDatos(dato) {
                PageMethods.RecibirDatoAbortoRegistro(dato, function (response) { document.getElementById("fRegistroServ").value =response; }, function (response) { console.write(response); });
            }

        }

        // ------------ typeahead REGISTROS CALIFICACIONES ------------- //

        function cargarTypeaheadCategorias() {

            var substringMatcherAborto = function (strs) {
                return function findMatches(q, cb) {
                    var matches, substrRegex;
                    matches = [];
                    substrRegex = new RegExp(q, 'i');
                    $.each(strs, function (i, str) {
                        if (substrRegex.test(str.Nombre)) {
                            matches.push({ Nombre: str.Nombre });
                        }
                    });
                    cb(matches);
                };
            };

            var listaAbortos = [];
            GetListaAbortos();
            function GetListaAbortos() {
                PageMethods.GetAbortosAnimalesConServicios(OnSuccessAb);
            }
            function OnSuccessAb(response) {
                var list = response;
                for (var i = 0; i < list.length; i++) {
                    var aborto = { Nombre: list[i].Nombre };
                    listaAbortos.push(aborto);
                }
            }

            $("#divInputCalificaciones.typeahead").typeahead({
                hint: true,
                highlight: true,
                minLength: 2
            },
            {
                name: 'listaAbortos',
                displayKey: 'Nombre',
                source: substringMatcherAborto(listaAbortos)
            });

            var abortoSeleccionada = function (eventObject, suggestionObject, suggestionDataset) {
                var dato = JSON.stringify(suggestionObject.Nombre);
                pasarDatos(dato);
            };
            $("#divInputCalificaciones.typeahead").on('typeahead:selected', abortoSeleccionada);

            function pasarDatos(dato) {
                PageMethods.RecibirDatoAbortoRegistro(dato, function (response) { console.write(response); } , function (response) { console.write(response); });
            }

        }

        // ------------ select EMPLEADOS ------------- //

        function cargarSelectEmpleados() {
            var htmlText = "";
            var listaEmpleados = [];
            GetListaEmpleados();
            function GetListaEmpleados() {
                PageMethods.GetListaEmpleados(OnSuccessEmp, function (response) { console.write(response); });
            }
            function OnSuccessEmp(response) {               
                var list = response;
                for (var i = 0; i < list.length; i++) {
                    var emple = { nombreCompleto: list[i].Nombre +" "+ list[i].Apellido + " - " + list[i].Iniciales, idEmpleado: list[i].Id_empleado };
                    //var nombreCompleto = list[i].apellido + " - " + list[i].iniciales;
                    htmlText += '<option value=' + emple.idEmpleado + '>' + emple.nombreCompleto + '</option>';
                }
                $("#selectEmpleados").append(htmlText);
            }           
        }


        // ------------ select LETRAS CALIFICACIONES ------------- //

        function cargarSelectLetras() {
            var htmlText = "";
            var listaLetras = ['EX','MB','BM','B','R'];
            for (var i = 0; i < listaLetras.length; i++) {
                htmlText += '<option value=' + listaLetras[i] + '>' + listaLetras[i] + '</option>';
            }
            $("#selectLetras").append(htmlText);
        }




        // ------------ select NUMEROS CALIFICACIONES ------------- //
        function cargaSelectNumeros() {

            $('#selectNumeros').find('option').remove().end();
            var htmlText = "";
            var num;
            var listaNumeros = [];
            var x = document.getElementById("selectLetras").value;
            switch (x) {
            case 'EX':
                for (var i = 0; i <= 10; i++)
                {
                    num = 100 - i;
                    htmlText += '<option value=' + num + '>' + num + '</option>';
                }
                break;
            case 'MB':
                for (var i = 0; i <= 5; i++) {
                    num = 90 - i;
                    htmlText += '<option value=' + num + '>' + num + '</option>';
                }
                break;
            case 'BM':
                for (var i = 0; i <= 5; i++) {
                    num = 85 - i;
                    htmlText += '<option value=' + num + '>' + num + '</option>';
                }
                break;
            case 'B':
                for (var i = 0; i <= 5; i++) {
                    num = 80 - i;
                    htmlText += '<option value=' + num + '>' + num + '</option>';
                }
                break;
            case 'R':
                for (var i = 0; i <= 6; i++) {
                    num = 76 - i;
                    htmlText += '<option value=' + num + '>' + num + '</option>';
                }
                break;
            }
            $("#selectNumeros").append(htmlText);

        }


    </script>
    

    <asp:Label ID="lblVer" runat="server" Text="Label" Visible="False"></asp:Label>
    <div class="row"><asp:Label ID="lblStatusOk" runat="server" Text="" CssClass="alert alert-success col-md-4">Evento ingresado correctamente</asp:Label></div>
    <div class="row"><asp:Label ID="lblStatusError" runat="server" Text="" CssClass="alert alert-danger col-md-4">No se pudo ingresar el evento</asp:Label></div>
    <div class="row"><asp:Label ID="lblStatusAviso" runat="server" Text="" CssClass="alert alert-info col-md-4">Debe seleccionar un evento para ingresar</asp:Label></div>
    
    
    
</asp:Content>
