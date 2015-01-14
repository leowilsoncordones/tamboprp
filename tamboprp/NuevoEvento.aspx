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
                        <asp:TextBox runat="server" id="fRegistro" class="form-control col-xs-10 col-sm-5"  OnTextChanged="EventosRegistro" AutoPostBack="True"/>                                            
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
			            <asp:DropDownList ID="ddlCalificacion" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="True" OnSelectedIndexChanged="ddlCalif_SelectedIndexChanged" runat="server" ></asp:DropDownList>
			        </div>
                </div>
                <div class="form-group" id="dCalifPuntos">
                    <label class="col-sm-3 control-label no-padding-right"> Puntos </label>
                    <div class="col-sm-2">
			            <asp:DropDownList ID="ddlCalificacionPts" CssClass="form-control col-xs-10 col-sm-5" runat="server" ></asp:DropDownList>
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                </asp:Panel>
                <!-- CONTROLES -->
                <asp:Panel ID="pnlControles" runat="server">
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Leche </label>
                    <div class="col-sm-2">
			            <input type="text" runat="server" id="fControl" placeholder="Leche en kilos" class="form-control col-xs-10 col-sm-5" />
			        </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Porcentaje de Grasa </label>
                    <div class="col-sm-2">
			            <input type="text" runat="server" id="fGrasa" placeholder="% Grasa" class="form-control col-xs-10 col-sm-5" />
			        </div>
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
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" Text="Guardar" OnClick="btn_GuardarEvento" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script src="js/bloodhound.js"></script>
    <script src="js/date-time/bootstrap-datepicker.js"></script>
    <script src="js/typeahead.jquery.js"></script>
    <%--<script src="js/js_tamboprp/NuevoEvento.js"></script>--%>
    
    <script text="javascript">
        
        //   ---------ABORTO chequear registro----------   //


        //function CheckDatosRegAborto() {
        //    PageMethods.CheckDatosRegAborto();
        //}

        //function OcultarLabelRegistro() {
        //    PageMethods.OcultarLabelRegistro();
        //}


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
                pasarDatos(dato);
            };
            $("#inputEnf.typeahead").on('typeahead:selected', enfermedadSeleccionada);

            function pasarDatos(dato) {
                PageMethods.RecibirDato(dato, function (response) { console.write(response); }, function (response) { console.write(response); });
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

    </script>
    

    <asp:Label ID="lblVer" runat="server" Text="Label" Visible="False"></asp:Label>
<asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
</asp:Content>
