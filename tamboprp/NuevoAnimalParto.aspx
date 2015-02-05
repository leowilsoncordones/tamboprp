<%@ Page Title="tamboprp | nuevo animal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoAnimalParto.aspx.cs" Inherits="tamboprp.NuevoAnimalParto" %>
<%@ Import Namespace="tamboprp" %>
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
        <h1><i class="menu-icon fa fa-edit"></i> Ingreso de parto y sus crías</h1>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div id="formulario" class="form-horizontal">
                <div class="form-group">
                    <div class="col-sm-3 control-label no-padding-right">
                        <h4 class="widget-title lighter blue">NUEVO PARTO</h4>
                    </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- GENERAL - Registro -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Registro madre </label>
			        <div class="col-sm-2">
                        <asp:TextBox runat="server" id="fRegistro" CssClass="form-control col-xs-10 col-sm-5" style="border-color: #72aec2;" OnTextChanged="EventosRegistro" AutoPostBack="True"/>
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- GENERAL - Fecha -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Fecha </label>
					<div class="col-sm-2">
						<div class="input-group date">
						    <input type="text" id="mydate" name="mydate" class="form-control col-xs-10 col-sm-5" style="border-color: #72aec2;" />
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
                <!-- GENERAL - Comentarios -->
                <div class="form-group">
			        <label class="col-sm-3 control-label no-padding-right"> Comentarios </label>
			        <div class="col-sm-5">
			            <textarea class="form-control" id="fComentario" rows="3" runat="server"></textarea>
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- SERVICIO y REG PADRE -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Registro padre </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fRegPadre" placeholder="Registro padre" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- GENERAL - Fecha -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Fecha servicio </label>
					<div class="col-sm-2">
						<div class="input-group date">
						    <input type="text" id="mydateServ" class="form-control col-xs-10 col-sm-5" runat="server" />
							<span class="input-group-addon"><i class="ace-icon fa fa-calendar"></i></span>
						</div>
					</div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- Botones -->
                <div class="space-6"></div>
                <div class="form-group">
                    <div class="col-md-offset-3 col-md-9">
                        <a href="#nuevaCria" role="button" id="id-btn-nuevaCria" class="btn btn-info" data-toggle="modal"><i class="ace-icon fa fa-save"></i> Ingresar cría</a>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				    </div>
                </div>
                
            </div>
         </div>
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    </div>
   
    <div class="space-6"></div>
    
    <div class="row">
    <!-- panel de crías ya ingresadas -->
    <asp:Panel ID="panelCriasIngresadas" runat="server" Visible="False">
        <hr/>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-8">
            <h4 class="widget-title lighter blue">Crías vivas ingresadas</h4>
            <p>        
            <asp:GridView ID="gvAnimales" runat="server" AutoGenerateColumns="False" GridLines="Both" HorizontalAlign="Left" 
                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
            <RowStyle HorizontalAlign="Left"  />
            <Columns>
                <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Identificacion" HeaderText="Identificacion" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Reg_trazab" HeaderText="Trazabilidad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Sexo" HeaderText="Sexo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Gen" HeaderText="Gen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Fecha_Nacim" HeaderText="Fecha Nac." dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
                <asp:BoundField DataField="Origen" HeaderText="Origen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Reg_padre" HeaderText="Reg. Padre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Reg_madre" HeaderText="Reg. Madre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <FooterStyle />
            <PagerSettings />
            <SelectedRowStyle />
            <HeaderStyle />
            <EditRowStyle />
            <AlternatingRowStyle />
            </asp:GridView>
            </p>
            </div>
            <div class="col-md-2"></div>
        </div>
    </asp:Panel>
    
    </div>
    <!-- NUEVA CRIA MODAL -->
    <div id="nuevaCria" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-pencil"></i> Ingresar cría </h4>
                </div>
                <div class="modal-body">
                    <span id="bodyModifDataModal" class="text-warning center">
                        
                        <!-- FORMULARIO -->
                        <div id="formularioCria" class="form-horizontal">
                         
                        <!-- HIJO - SEXO?, VIVO? -->
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Vivo </label>
                            <div class="col-sm-2">
					            <label>
					                <input id="checkVivo" name="switchVivo" class="ace ace-switch ace-switch-6" type="checkbox" checked runat="server"/>
						            <span class="lbl"></span>
					            </label>
				            </div>
                            <div class="col-sm-12"></div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Sexo </label>
                            <div class="col-sm-2">
					            <label>
					                <input id="checkSexo" name="switchSexo" class="ace ace-switch ace-switch-5" type="checkbox" checked runat="server"/>
						            <span class="lbl"></span>
					            </label>
				            </div>
                            <div class="col-sm-12"></div>
                        </div>
                        <hr/>
                        <asp:Panel ID="pnlServicio" runat="server">
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Registro cría </label>
			                <div class="col-sm-4">
			                    <input type="text" runat="server" id="fRegCria" placeholder="Registro cría" class="form-control col-xs-10 col-sm-5" style="border-color: #72aec2;" />
			                </div>
                            <div class="col-sm-12"></div>
                        </div>
                        <!-- HIJO - Nombre, Origen, TRazab -->
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Nombre </label>
			                <div class="col-sm-6">
			                    <input type="text" runat="server" id="fNombre" placeholder="" class="form-control col-xs-10 col-sm-5" />
			                </div>
                            <div class="col-sm-12"></div>
		                </div>
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Origen </label>
			                <div class="col-sm-4">
			                    <input type="text" runat="server" id="fOrigen" placeholder="PROPIETARIO" class="form-control col-xs-10 col-sm-5" />
			                </div>
                            <div class="col-sm-12"></div>
		                </div>
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Trazabilidad </label>
			                <div class="col-sm-4">
			                    <input type="text" runat="server" id="fTraz" placeholder="MGAP" class="form-control col-xs-10 col-sm-5" />
			                </div>
                            <div class="col-sm-12"></div>
		                </div>
                        <!-- HIJO - GEN, IDENTIFICACION -->
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Generación </label>
			                <div class="col-sm-2">
			                    <input type="text" runat="server" id="fGen" class="form-control col-xs-10 col-sm-5" />
			                </div>
                            <div class="col-sm-12"></div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Identificación </label>
			                <div class="col-sm-4">
			                    <input type="text" runat="server" id="fIdentif" class="form-control col-xs-10 col-sm-5" />
			                </div>
                            <div class="col-sm-12"></div>
                        </div>
                        </asp:Panel>
                            
                        </div>

                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_NuevaCria" />
                </div>
            </div>
        </div>
        <asp:Label ID="lblLetraSistema" runat="server" Visible="False"></asp:Label>
    </div>
    <!-- NUEVA CRIA MODAL -->
    
    
    <script src="js/date-time/bootstrap-datepicker.js"></script>
        
    <script text="javascript">
        
        //
<%--        $(document).on("click", "#nuevaCria", function () {

            var gen = document.getElementById('<%=fGen.ClientID%>').value;
            var letraSistema = document.getElementById('<%=lblLetraSistema.ClientID%>').value;
            var registroCria = document.getElementById('fRegCria').value;
            document.getElementById('fIdentif').innerHTML = gen + letraSistema + registroCria;

        });--%>


        //   ---------DATEPICKER----------   //

        $("#mydate").datepicker({
            autoclose: true,
            todayHighlight: true,
            format: 'dd/mm/yyyy'
        });
        $("#mydate").datepicker('setDate', new Date());

    </script>

</asp:Content>
