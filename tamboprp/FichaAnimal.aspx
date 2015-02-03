<%@ Page Title="tamboprp | ficha de animal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FichaAnimal.aspx.cs" Inherits="tamboprp.FichaAnimal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <link href="css/colorbox.css" rel="stylesheet" />

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
    <script src="js/excanvas.js"></script>
    
</asp:Content>
<asp:Content ID="ContentAnimal" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <script src="js/jquery.colorbox.js"></script>

    <!------------- Script image gallery --------------->
	<script type="text/javascript">

		$(function () {
		    var colorbox_params = {
		        reposition: true,
		        scalePhotos: true,
		        scrolling: false,
		        previous: '<i class="icon-arrow-left"></i>',
		        next: '<i class="icon-arrow-right"></i>',
		        close: '&times;',
		        current: '{current} of {total}',
		        maxWidth: '100%',
		        maxHeight: '100%',
		        onOpen: function () {
		            document.body.style.overflow = 'hidden';
		        },
		        onClosed: function () {
		            document.body.style.overflow = 'auto';
		        },
		        onComplete: function () {
		            $.colorbox.resize();
		        }
		    };
		    $('.ace-thumbnails [data-rel="colorbox"]').colorbox(colorbox_params);
		    $("#cboxLoadingGraphic").append("<i class='icon-spinner orange'></i>");//let's add a custom loading icon
		    $(window).on('resize.colorbox', function () {
		        try {
		            $.fn.colorbox.load();//to redraw the current frame
		        } catch (e) { }
		    });
		})

	</script>
    

    <div class="page-header">
        <h1><i class="menu-icon fa fa-github-alt"></i> Ficha de animal <small><i class="ace-icon fa fa-angle-double-right"></i> y sus eventos históricos</small></h1>
    </div>
        <div class="row">
            <div class="col-md-4">        
                <div class="input-group input-group-lg">
                    <span class="input-group-btn">
                        <asp:Button ID="btnBuscarAnimal" runat="server" onclick="btnBuscarAnimal_Click" Text="Buscar" CssClass="btn btn-white btn-default" />
                    </span>
                    <input type="text" class="form-control" runat="server" id="regBuscar" placeholder="Registro"/>
                </div>
            </div>
            <div class="col-md-4 btn-group btn-group-lg">
                <asp:DropDownList ID="ddlSimil" runat="server" style="height:46px;" CssClass="btn btn-white btn-default btn-lg col-sm-9 dropdown-toggle" OnSelectedIndexChanged="ddlSimilares_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
            </div>
            <div class="col-md-4">
                
            </div>
        </div>
        <br/>
        <br/>
    <!-- Panel de ficha de animal -->
    <asp:Panel ID="panelFicha" runat="server" CssClass="panel panel-default">
          <div class="panel-heading">
              <!-- Panel heading -->
              <div class="row">
                <div class="col-xs-12 col-md-8">
                    <h3 class="panel-title"><asp:Label ID="lblAnimal" CssClass="btn-lg" runat="server" Text="Registro" ></asp:Label>
                    <asp:Label ID="lblNombre" CssClass="btn-lg" runat="server" Visible="False" ></asp:Label>
                    <asp:Label ID="titSexo" runat="server" CssClass="label label-default label-lg arrowed-right" Text="Sexo" ></asp:Label><asp:Label ID="lblSexo" CssClass="btn-lg" runat="server" ></asp:Label>
                    <asp:Label ID="titIdentif" runat="server" CssClass="label label-default label-lg arrowed-right" Text="Identificación"></asp:Label><asp:Label ID="lblIdentif" CssClass="btn-lg" runat="server" ></asp:Label>
                    <asp:Label ID="titTraz" runat="server" CssClass="label label-default label-lg arrowed-right" Text="MGAP"></asp:Label><asp:Label ID="lblTraz" CssClass="btn-lg" runat="server" ></asp:Label>
                    <asp:Label ID="titCalif" runat="server" CssClass="label label-default label-lg arrowed-right" Text="Calificación" Visible="False"></asp:Label><asp:Label ID="lblCalif" CssClass="btn-lg" runat="server" ></asp:Label>
                  </h3>
                </div>
                <div class="col-xs-6 col-md-4 text-right">
                    <div class="btn-group" role="group" >
                      <a href="#modifData" role="button" class="btn btn-white btn-default btn-sm" data-toggle="modal"><span class="fa fa-pencil" aria-hidden="true"></span> Editar</a>
                      <a href="#fotosModal" role="button" class="btn btn-white btn-default btn-sm" data-toggle="modal"><span class="fa fa-camera-retro" aria-hidden="true"></span> Fotos</a>
                      <a href="#grafModal" role="button" class="btn btn-white btn-default btn-sm" onclick="GetValoreLeche()" data-toggle="modal"><span class="fa fa-bar-chart-o" aria-hidden="true"></span> Producción</a>
                    </div>
                </div>
                </div>
          </div> <!-- Fin panel heading -->

          <!-- Panel boody -->
          <div class="panel-body">
              <!-- Ficha para todos los animales -->
                <ul class="list-group">
                <li class="list-group-item">
                    <div class="row">
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titCategoria" CssClass="text-info" runat="server" Text="Categ: "></asp:Label><strong><asp:Label ID="lblCategoria" CssClass="label label-default btn-lg arrowed" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titEstado" CssClass="text-info" runat="server" Text="Estado: "></asp:Label><strong><asp:Label ID="lblEstado" runat="server" Visible="False" CssClass="label label-default btn-lg arrowed"></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titGen" CssClass="text-info" runat="server" Text="Generación: "></asp:Label><strong><asp:Label ID="lblGen" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titOrigen" CssClass="text-info" runat="server" Text="Origen: "></asp:Label><strong><asp:Label ID="lblOrigen" runat="server" ></asp:Label></strong></div>
                    </div>
                </li>
                <li class="list-group-item">
                    <div class="row">
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titFechaNac" CssClass="text-info" runat="server" Text="Fecha de nacimiento: " ></asp:Label><strong><asp:Label ID="lblFechaNac" runat="server" dataformatstring="{0:dd/MM/yyyy}" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titEdad" CssClass="text-info" runat="server" Text="Edad: " ></asp:Label><strong><asp:Label ID="lblEdad" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titRegPadre" CssClass="text-info" runat="server" Text="Padre: "></asp:Label><strong><asp:Label ID="lblRegPadre" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titRegMadre" CssClass="text-info" runat="server" Text="Madre: "></asp:Label><strong><asp:Label ID="lblRegMadre" runat="server" ></asp:Label></strong></div>
                    </div>
                </li>
                </ul>
              <!-- Ficha solo para hembras -->
              <asp:PlaceHolder ID="phFichaHembra" Visible="false" runat="server">
                <ul class="list-group" >
                <!-- Linea de info de produccion ultimo control y ultima lactancia -->
                <li class="list-group-item">
                    <div class="row">
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titLecheUltControl" CssClass="text-info" runat="server" Text="Leche último control: "></asp:Label><strong><asp:Label ID="lblLecheUltControl" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titProdLecheUlt" CssClass="text-info" runat="server" Text="Producción leche: "></asp:Label><strong><asp:Label ID="lblProdLecheUlt" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titProdGrasaUlt" CssClass="text-info" runat="server" Text="Producción grasa: "></asp:Label><strong><asp:Label ID="lblProdGrasaUlt" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titAvgGrasaUlt" CssClass="text-info" runat="server" Text="Porcentaje grasa: "></asp:Label><strong><asp:Label ID="lblAvgGrasaUlt" runat="server" ></asp:Label></strong></div>
                    </div>
                </li>
                <!-- Linea de info lactancias -->
                <li class="list-group-item">
                    <div class="row">
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titNumLact" CssClass="text-info" runat="server" Text="Lactancia: "></asp:Label><strong><asp:Label ID="lblNumLact" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titDiasLact" CssClass="text-info" runat="server" Text="Días de lactancia: "></asp:Label><strong><asp:Label ID="lblDiasLact" runat="server" ></asp:Label></strong></div>
                        <div class="col-xs-6 col-sm-3"><asp:Label ID="titParidos" CssClass="text-info" runat="server" Text="Partos: "></asp:Label><asp:Label ID="lblH" runat="server" CssClass="badge badge-pink" ></asp:Label>&nbsp;<asp:Label ID="lblM" runat="server" CssClass="badge badge-primary" ></asp:Label></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titFechaUltParto" CssClass="text-info" runat="server" Text="Último parto: "></asp:Label><strong><asp:Label ID="lblFechaUltParto" runat="server" ></asp:Label></strong></div>
                    </div>
                </li>
                <!-- Linea de info total produccion -->
                <li class="list-group-item">
                    <div class="row">
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titControles" CssClass="text-info" runat="server" Text="Total controles: "></asp:Label><strong><asp:Label ID="lblControles" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titProdLeche" CssClass="text-info" runat="server" Text="Producción total leche: "></asp:Label><strong><asp:Label ID="lblProdLeche" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titProdGrasa" CssClass="text-info" runat="server" Text="Producción total grasa: "></asp:Label><strong><asp:Label ID="lblProdGrasa" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titAvgGrasa" CssClass="text-info" runat="server" Text="Porcentaje total grasa: "></asp:Label><strong><asp:Label ID="lblAvgGrasa" runat="server" ></asp:Label></strong></div>
                    </div>
                </li>
                <!-- Linea de info de servicios -->
                <li class="list-group-item">
                    <div class="row">
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titServicios" CssClass="text-info" runat="server" Text="Servicios: "></asp:Label><strong><asp:Label ID="lblServicios" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titFechaUltServ" CssClass="text-info" runat="server" Text="Último Servicio: "></asp:Label><strong><asp:Label ID="lblFechaUltServ" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titRegServicio" CssClass="text-info" runat="server" Text="Registro servicio: "></asp:Label><strong><asp:Label ID="lblRegServicio" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titDiag" CssClass="text-info" runat="server" Text="Diagnóstico: "></asp:Label><strong><asp:Label ID="lblDiag" runat="server" ></asp:Label></strong></div>
                    </div>
                </li>
                <!-- Linea info de de secados y partos -->
                <li class="list-group-item">
                    <div class="row">
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titFechaSecado" CssClass="text-info" runat="server" Text="Último secado: "></asp:Label><strong><asp:Label ID="lblFechaSecado" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titMotivoSecado" CssClass="text-info" runat="server" Text="Motivo: "></asp:Label><strong><asp:Label ID="lblMotivoSecado" runat="server" ></asp:Label></strong></div>
                    </div>
                </li>
                
                </ul>
               </asp:PlaceHolder>

              <!-- Acordeon para ver eventos historicos -->
              <div class="accordion-style1 panel-group" id="accordionHist">
                 <div class="panel panel-default">
                  <div class="panel-heading">
                   <h4 class="panel-title">
                     <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordionHist" href="#accordionHistorico">
                       <i data-icon-show="ace-icon fa fa-angle-right" data-icon-hide="ace-icon fa fa-angle-down" class="bigger-110 ace-icon fa fa-angle-right"></i>
                        <asp:Label ID="titCantEventos" runat="server" Text="Historial de eventos "> </asp:Label><asp:Label ID="lblCantEventos" runat="server" CssClass="badge badge-grey" ></asp:Label>
                     </a>
                   </h4>
                  </div>
                  <div id="accordionHistorico" class="panel-collapse collapse">
                    <div class="panel-body">
                      
                        
                        <!-- Tabla de eventos historicos -->  
                        <asp:PlaceHolder ID="phHistorial" runat="server">
                            <div class="row">
                                  <div class="col-md-4"></div>
                                  <div class="col-md-8 text-right text-info">
                                      <asp:CheckBox ID="cboxControles" Text="  Filtrar Controles" AutoPostBack="True" OnCheckedChanging="cBoxControles_CheckedChanging" runat="server" />
                                  </div>
                            </div>
                            <asp:GridView ID="gvHistoria" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                            <RowStyle HorizontalAlign="Left"  />
                            <Columns>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="NombreEvento" HeaderText="Evento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />   
                                <asp:BoundField DataField="Observaciones" HeaderText="Observaciones del evento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
                                <asp:BoundField DataField="Comentarios" HeaderText="Comentario" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                            <FooterStyle />
                            <PagerStyle HorizontalAlign="Left" />
                            <SelectedRowStyle />
                            <HeaderStyle />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            </asp:GridView>
                        </asp:PlaceHolder><!-- Fin tabla de eventos historicos -->
                        
                    </div>
                  </div>
                 </div> 
                </div> <!-- fin acordeon -->
          
          </div><!-- Fin panel boby -->
            <!-- Panel footer -->
          <div class="panel-footer">
            <asp:Label ID="titFooterPanel" Text="Ficha de animal" runat="server"></asp:Label>&nbsp;&nbsp;
            <asp:Label ID="lblFooterPanel" Visible="False" runat="server"></asp:Label>
            <span class='pull-right'>
                <asp:Label ID="lblStatus" Text="" runat="server"></asp:Label>
            </span>
          </div><!-- Fin panel footer -->
        </asp:Panel>
    
    
    <!-- MODAL CON LA GRAFICA DE CONTROLES DEL ANIMALES HEMBRA -->
          <div id="grafModal" class="modal fade">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><span class="menu-icon fa fa-bar-chart-o"></span> Controles de <asp:Label ID="lblRegistroModal" CssClass="text-info" runat="server"></asp:Label> 
                            <small><i class="ace-icon fa fa-angle-double-right"></i> último año en producción</small></h4>
                    </div>
                    <div class="modal-body">
                        <div id="sales-charts" class="center-block"></div>
                        <span class="text-warning"><small></small></span>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div> 
        <!-- FIN MODAL CON LA GRAFICA DE CONTROLES DEL ANIMALES HEMBRA -->
              
        
      <!-- MODAL FOTOS -->
          <div id="fotosModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><span class="menu-icon fa fa-bar-chart-o"></span> Fotos de <asp:Label ID="lblRegistroModalFotos" CssClass="text-info" runat="server"></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <!-- GALLERY thumbnails -->
                        <ul class="ace-thumbnails clearfix" runat="server" id="ULFotos" >
                            
                        </ul>
                    </div>
                    <div class="modal-footer">
                        <!-- boton cerrar -->
                        <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cerrar</button>
                        <a href="#nuevaFoto" role="button" class="btn btn-default btn-info btn-sm" data-toggle="modal"><span class="fa fa-upload" aria-hidden="true"></span> Subir foto</a>
                    </div>
                </div>
            </div>
        </div>    
        <!-- FIN MODAL FOTOS -->      

    
    <!-- SUBIR FOTO NUEVA MODAL -->
    <div id="nuevaFoto" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-image"></i> Subir nueva foto para <asp:Label ID="lblRegistroModalSubirFoto" CssClass="text-info" runat="server"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <span id="bodyModifNuevFotoModal" class="text-warning center">
                        <!-- FORMULARIO -->
                        <div id="formulario1" class="form-horizontal">
                        <!-- Subir foto -->
                        <div class="form-group">
		                    <label class="col-sm-3 control-label no-padding-right"> Buscar </label>
			                <div class="col-sm-7">
			                    <!-- File upload -->
                                <div class="input-group input-group-lg">
                                    <asp:FileUpload ID="fupFoto" runat="server" />
                                </div>
			                </div>
		                </div>
                        <!-- Pie de foto -->
                        <div class="form-group">
		                    <label class="col-sm-3 control-label no-padding-right"> Pie de Foto </label>
			                <div class="col-sm-7">
			                    <input type="text" runat="server" id="fPie" placeholder="Ej. Campeón Expo Prado 2012" class="form-control col-xs-10 col-sm-5" />
			                </div>
		                </div>
                        <!-- Comentario -->
                        <div class="form-group">
			                <label class="col-sm-3 control-label no-padding-right"> Comentario </label>
			                <div class="col-sm-7">
			                    <textarea class="form-control" id="fComentario" rows="4" runat="server"></textarea>
			                </div>
                            <div class="col-sm-12"></div>
		                </div>
                        </div>
                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnFotoUpload" runat="server" Text="Guardar" CssClass="btn btn-default btn-info btn-sm" OnClick="UploadButton_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL SUBIR FOTO NUEVA MODAL -->    
    
    

    <!-- MODIFICAR DATOS MODAL -->
    <div id="modifData" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-pencil"></i> Modificar animal <asp:Label ID="lblRegistroModalModificar" CssClass="text-info" runat="server"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <span id="bodyModifDataModal" class="text-warning center">
                        
                        <!-- FORMULARIO -->
                        <div id="formulario" class="form-horizontal">
                        <!-- Nombre -->
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Nombre </label>
			                <div class="col-sm-6">
			                    <input type="text" runat="server" id="fNombre" class="form-control col-xs-10 col-sm-5" />
			                </div>
		                </div>
                        <!-- Identificacion -->
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Identificación </label>
			                <div class="col-sm-3">
			                    <input type="text" runat="server" id="fIdentif" class="form-control col-xs-10 col-sm-5" />
			                </div>
		                </div>
                        <!-- Reg. Trazabilidad -->
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Reg. Trazabilidad </label>
			                <div class="col-sm-3">
			                    <input type="text" runat="server" id="fTraz" class="form-control col-xs-10 col-sm-5" />
			                </div>
                            <div class="col-sm-12"></div>
		                </div>
                        <!-- Origen -->
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Origen </label>
                            <div class="col-sm-5">
			                    <input type="text" runat="server" id="fOrigen" placeholder="Ej. PROPIETARIO" class="form-control col-xs-10 col-sm-5" />
			                </div>
                        </div>
                        </div>

                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-sm btn-info" Text="Modificar" OnClick="btn_ModificarAnimal" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->
    

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" /> 
    
    <!-- DATOS GRAFICA -->
    
    <script type="text/javascript">
        
        //$(document).ready(function() {
        //    GetValoreLeche();
           
        //});

        function gd1(date) {
            return new Date(date).getTime();
        }

        function GetValoreLeche() {
            //var reg = $('#regBuscar').val();
            PageMethods.GetControlesByRegistro(OnSuccess);
        }

        function OnSuccess(response){
            var leche3 = [];
        var list = response;
            for (var i = 0; i < list.length; i++) {
                leche3.push([gd1(list[i].Fecha), list[i].Leche]);
            }
            imprimir(leche3);
            
        }


        var sales_charts = $('#sales-charts').css({ 'height': '240px', 'width': '780px' });
        function imprimir(totalLeche) {
        
        $.plot("#sales-charts", [
            { label: "Leche", data: totalLeche }

        ], {
            hoverable: true,
            shadowSize: 0,
            series: {
                lines: { show: true },
                points: { show: true }
            },
            xaxis: {
                tickLength: 0,
                mode: "time",
                timeformat: "%m/%Y",
                tickSize: [2, "month"]
            },
            yaxis: {
                ticks: 10,
                min: 0,
                max: 50,
                tickDecimals: 0
            },
            grid: {
                backgroundColor: { colors: ["#fff", "#fff"] },
                borderWidth: 1,
                borderColor: '#555',
                hoverable: true
            }
        });

        }

    </script>
    
    <script src="js/flot/jquery.flot.js"></script>
    <script src="/js/flot/jquery.flot.time.js"></script>
    <script src="/js/flot/jquery.flot.symbol.js"></script>
    <script src="/js/flot/jquery.flot.axislabels.js"></script>

    <!-- FIN GRAFICA -->
    
    
    

</asp:Content>
