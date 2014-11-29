<%@ Page Title="tamboprp | animales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Animales.aspx.cs" Inherits="tamboprp.Animales" %>
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
    
    <!-- script para dropdown resultados similares (aun no funciona) -->
    <script type='text/javascript'>
        $('.dropdown-hover').on('mouseenter', function () {
            var offset = $(this).offset();
            var $w = $(window);
            if (offset.top > $w.scrollTop() + $w.innerHeight() - 100)
                $(this).addClass('dropup');
            else $(this).removeClass('dropup');
        });
    </script>

</asp:Content>
<asp:Content ID="ContentAnimal" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header"><i class="menu-icon fa fa-folder-open"></i> Ficha de animal</h1>
        <div class="row">
            <div class="col-md-4">        
                <div class="input-group input-group-lg">
                    <span class="input-group-btn">
                        <asp:Button ID="btnBuscarAnimal" runat="server" onclick="btnBuscarAnimal_Click" Text="Buscar" CssClass="btn btn-white btn-default" />
                    </span>
                    <input type="text" class="form-control" runat="server" id="regBuscar" placeholder="Registro"/>
                </div>
            </div>
            <div class="col-md-4" id="divContenedorDdl" runat="server" >
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
                        <button type="button" class="btn btn-white btn-default btn-sm" id="btnAgregar"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span> Evento</button>
                      <button type="button" class="btn btn-white btn-default btn-sm" id="btnEditar"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> Editar</button>
                      <button type="button" class="btn btn-white btn-default btn-sm" id="btnEliminar"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span> Eliminar</button>
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
                <!-- Linea de info total produccion -->
                <li class="list-group-item">
                    <div class="row">
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titControles" CssClass="text-info" runat="server" Text="Controles: "></asp:Label><strong><asp:Label ID="lblControles" runat="server" ></asp:Label></strong></div>
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
                <!-- Linea info de lactancias -->
                <li class="list-group-item">
                    <div class="row">
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titNumLact" CssClass="text-info" runat="server" Text="Lactancia: "></asp:Label><strong><asp:Label ID="lblNumLact" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titDiasLact" CssClass="text-info" runat="server" Text="Días de lactancia: "></asp:Label><strong><asp:Label ID="lblDiasLact" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titParidos" CssClass="text-info" runat="server" Text="Partos: "></asp:Label><asp:Label ID="lblH" runat="server" CssClass="badge badge-pink" ></asp:Label>&nbsp;<asp:Label ID="lblM" runat="server" CssClass="badge badge-primary" ></asp:Label></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titFechaUltParto" CssClass="text-info" runat="server" Text="Último parto: "></asp:Label><strong><asp:Label ID="lblFechaUltParto" runat="server" ></asp:Label></strong></div>
                    </div>
                </li>
                <!-- Linea de info de secados -->
                <li class="list-group-item">
                    <div class="row">
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titFechaSecado" CssClass="text-info" runat="server" Text="Último secado: "></asp:Label><strong><asp:Label ID="lblFechaSecado" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titMotivoSecado" CssClass="text-info" runat="server" Text="Motivo: "></asp:Label><strong><asp:Label ID="lblMotivoSecado" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><asp:Label ID="titLecheUltControl" CssClass="text-info" runat="server" Text="Leche último control: "></asp:Label><strong><asp:Label ID="lblLecheUltControl" runat="server" ></asp:Label></strong></div>
                      <div class="col-xs-6 col-sm-3"><span class="glyphicon glyphicon-camera" aria-hidden="true"></span><asp:Label ID="lblFotos" CssClass="text-info" runat="server" Text=" Ver fotos"></asp:Label></div>
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
                        <asp:Label ID="titCantEventos" runat="server" Text="Historial de eventos "> </asp:Label><asp:Label ID="lblCantEventos" runat="server" CssClass="badge" ></asp:Label>
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
                                    <label class="checkbox-inline"><input type="checkbox" checked id="checkboxControles" value="opcControles"> Controles</label>
                                    <label class="checkbox-inline"><input type="checkbox" checked id="checkboxPartos" value="opcPartos"> Partos</label>
                                    <label class="checkbox-inline"><input type="checkbox" checked id="checkboxDiag" value="opcDiag"> Diagnósticos</label>
                                    <label class="checkbox-inline"><input type="checkbox" checked id="checkboxSecados" value="opcSecados"> Secados</label>
                                    <label class="checkbox-inline"><input type="checkbox" checked id="checkboxConcurso" value="opcConcurso"> Concursos</label>
                                    <label class="checkbox-inline"><input type="checkbox" checked id="checkboxBajas" value="opcBajas"> Bajas</label>
                                    <asp:CheckBox ID="checkboxAll" CssClass="checkbox-inline" runat="server" Text="All" />
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
            Aca va el footer del panel
          </div><!-- Fin panel footer -->
        </asp:Panel>


</asp:Content>
