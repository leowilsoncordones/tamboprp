<%@ Page Title="tamboprp | nuevo animal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoAnimal.aspx.cs" Inherits="tamboprp.NuevoAnimal" %>
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
    <div class="page-header">
        <h1><i class="menu-icon fa fa-edit"></i> Ingreso de nuevo animal</h1>
    </div>
    
    <div class="row">
        <div class="col-sm-12">
            <div id="formulario" class="form-horizontal">
                <div class="form-group">
                    <div class="col-sm-3 control-label no-padding-right">
                        <h4 class="widget-title lighter blue">NUEVO ANIMAL</h4>
                    </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- GENERAL - Registro, Identif -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Registro </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fRegistro" placeholder="Registro" class="form-control col-xs-10 col-sm-5" style="border-color: #72aec2;" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Identificación </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fIdentif" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- Nombre, Origen, Trazab -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Nombre </label>
			        <div class="col-sm-3">
			            <input type="text" runat="server" id="fNombre" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Origen </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fOrigen" placeholder="Ej.: SEMEX" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Trazabilidad </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fTraz" placeholder="MGAP" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- HIJO - GEN, IDENTIFICACION -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Generación </label>
			        <div class="col-sm-1">
			            <input type="text" runat="server" id="fGen" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- SEXO -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Sexo </label>
                    <div class="col-sm-2">
					    <label>
					        <input id="checkSexo" name="switchSexo" class="ace ace-switch ace-switch-5" type="checkbox" checked runat="server"/>
						    <span class="lbl"></span>
					    </label>
				    </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- CATEGORIA -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Categoría </label>
                    <div class="col-sm-3">
					    <label>
					        <asp:DropDownList ID="ddlCategorias" cssClass="form-control col-xs-10 col-sm-5" runat="server" style="border-color: #72aec2;" ></asp:DropDownList>
					    </label>
				    </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- GENERAL - Fecha -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Fecha de nacimiento</label>
					<div class="col-sm-2">
						<div class="input-group date">
						    <input type="text" id="mydate" name="mydate" class="form-control col-xs-10 col-sm-5" />
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
                
                <!-- SERVICIO y REG PADRE -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Registro madre </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fRegMadre" placeholder="Dam" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- SERVICIO y REG PADRE -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Registro padre </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fRegPadre" placeholder="Sire" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
               
                <!-- Botones -->
                <div class="space-6"></div>
                <div class="form-group">
                    <div class="col-md-offset-3 col-md-9">
                        <a href="#modalGuardar" role="button" id="btnGuardar" class="btn btn-info" data-toggle="modal"><i class="ace-icon fa fa-save"></i> Guardar</a>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				    </div>
                </div>
            </div>
         </div>
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    </div>
    
    
    <!-- GUARDAR MODAL -->
    <div id="modalGuardar" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-save"></i> Ingresar nuevo animal</h4>
                    <h6>Confirme si desea guardar los siguientes datos</h6>
                </div>
                <div class="modal-body" id="bodyModal" runat="server">
                    <div class="form-horizontal">

                    <div class="form-group">
                        <label class="col-sm-5 control-label no-padding-right">Registro </label>
                        <div class="col-sm-5">
                            <label id="txtModalRegistro" class="control-label no-padding-left"></label>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label class="col-sm-5 control-label no-padding-right"> Identificación </label>
			            <div class="col-sm-5">
			                <label id="txtModalIdentif" class="control-label no-padding-left"></label>
			            </div>
                    </div>
                        
                    <div class="form-group">
                        <label class="col-sm-5 control-label no-padding-right"> Nombre </label>
			            <div class="col-sm-5">
			                <label id="txtModalNom" class="control-label no-padding-left"></label>
			            </div>
                    </div>    
                    
                    <div class="form-group">
                        <label class="col-sm-5 control-label no-padding-right"> Origen </label>
			            <div class="col-sm-5">
			                <label id="txtModalOrigen" class="control-label no-padding-left"></label>
			            </div>
                    </div>  
                        
                    <div class="form-group">
                        <label class="col-sm-5 control-label no-padding-right"> Trazabilidad </label>
			            <div class="col-sm-5">
			                <label id="txtModalTraz" class="control-label no-padding-left"></label>
			            </div>
                    </div> 
                    
                    <div class="form-group">
                        <label class="col-sm-5 control-label no-padding-right"> Gen </label>
			            <div class="col-sm-5">
			                <label id="txtModalGen" class="control-label no-padding-left"></label>
			            </div>
                    </div> 

                    <div class="form-group">
                        <label class="col-sm-5 control-label no-padding-right"> Sexo </label>
			            <div class="col-sm-5">
			                <label id="txtModalSexo" class="control-label no-padding-left"></label>
			            </div>
                    </div> 
                    
                    <div class="form-group" id="lblModalConcCateg">
                        <label class="col-sm-5 control-label no-padding-right">Categoría </label>
                        <div class="col-sm-5">
                            <label id="txtModalCateg" class="control-label no-padding-left"></label>
                        </div>
                    </div>    

                    <div class="form-group">
                        <label id="lblModalFecha" class="col-sm-5 control-label no-padding-right">Fecha de nacimiento </label>
                        <div class="col-sm-5">
                            <label id="txtModalFecha" class="control-label no-padding-left"></label>
                        </div>
                    </div>

                    <div class="form-group" id="lblModalServRegMadre">
                        <label class="col-sm-5 control-label no-padding-right">Registro madre </label>
                        <div class="col-sm-5">
                            <label id="txtModalServRegMadre" class="control-label no-padding-left"></label>
                        </div>
                    </div>
                    
                   <div class="form-group" id="lblModalServRegPadre">
                        <label class="col-sm-5 control-label no-padding-right">Registro padre </label>
                        <div class="col-sm-5">
                            <label id="txtModalServRegPadre" class="control-label no-padding-left"></label>
                        </div>
                    </div>
                    </div>
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnPrueba" runat="server" CssClass="btn btn-sm btn-info" Text="Confirmar" OnClick="btn_GuardarAnimal" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->    
    
    <script src="js/date-time/bootstrap-datepicker.js"></script>
        
    <script text="javascript">
        
        //
        $(document).on("click", "#btnGuardar", function () {
            var ddlCat = document.getElementById("<%=ddlCategorias.ClientID%>");
            var valorCat = ddlCat.options[ddlCat.selectedIndex].text;

            document.getElementById('txtModalRegistro').innerHTML = document.getElementById('<%=fRegistro.ClientID%>').value;
            document.getElementById('txtModalIdentif').innerHTML = document.getElementById('<%=fIdentif.ClientID%>').value;
            document.getElementById('txtModalNom').innerHTML = document.getElementById('<%=fNombre.ClientID%>').value;
            document.getElementById('txtModalOrigen').innerHTML = document.getElementById('<%=fRegistro.ClientID%>').value;
            document.getElementById('txtModalTraz').innerHTML = document.getElementById('<%=fOrigen.ClientID%>').value;
            document.getElementById('txtModalGen').innerHTML = document.getElementById('<%=fGen.ClientID%>').value;
            document.getElementById('txtModalSexo').innerHTML = document.getElementById('<%=checkSexo.ClientID%>').checked ? 'MACHO' : 'HEMBRA';
            document.getElementById('txtModalFecha').innerHTML = document.getElementById('mydate').value;
            document.getElementById('txtModalCateg').innerHTML = valorCat;
            document.getElementById('txtModalServRegMadre').innerHTML = document.getElementById('<%=fRegMadre.ClientID%>').value;
            document.getElementById('txtModalServRegPadre').innerHTML = document.getElementById('<%=fRegPadre.ClientID%>').value;
            
        });


        //   ---------DATEPICKER----------   //

        $("#mydate").datepicker({
            autoclose: true,
            todayHighlight: true,
            format: 'dd/mm/yyyy'
        });
        $("#mydate").datepicker('setDate', '');

    </script>

</asp:Content>
