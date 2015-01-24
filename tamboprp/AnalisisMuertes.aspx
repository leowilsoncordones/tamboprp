<%@ Page Title="tamboprp | muertes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisisMuertes.aspx.cs" Inherits="tamboprp.AnalisisMuertes" %>
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
    
    <script src="js/otros/bs.pagination.js"></script>

    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/excanvas.js"></script>
    
    <script type="text/javascript">
        function pageLoad() {
            $('.bs-pagination td table').each(function (index, obj) {
                convertToPagination(obj);
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-thumbs-o-down"></i> Información sobre muertes <small><i class="ace-icon fa fa-angle-double-right"></i> análisis sanitario y lista de muertes</small></h1>
    </div>

    <!-- comienza tabbable -->
    <div class="tabbable">
		<ul class="nav nav-tabs" id="myTab">
			<li class="active"><a data-toggle="tab" href="#analisisSanitario"><i class="blue ace-icon fa fa-stethoscope bigger-120"></i> Análisis sanitario de muertes </a></li>
            <li><a data-toggle="tab" href="#listaMuertes"><i class="blue ace-icon fa fa-list bigger-120"></i> Lista de muertes <asp:Label ID="badgeCantOrdene" runat="server" cssClass="badge badge-success"></asp:Label></a></li>
		</ul>
        <!-- comienza contenido de tabbable -->
		<div class="tab-content">
		    
		    <!-- PESTANA 1: Análisis sanitario de muertes -->
			<div id="analisisSanitario" class="tab-pane fade in active">
			    <div class="page-header">
                    <h1><i class="menu-icon fa fa-stethoscope"></i> Análisis sanitario de muertes <small><i class="ace-icon fa fa-angle-double-right"></i> según incidencia en el rodeo</small></h1>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="pull-right">
                            <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm" Text=" Excel" onclick="excelExport_Click"><span><i class="fa fa-file-excel-o bigger-110 green"></i></span> Excel</asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" PDF" onclick="pdfExport_Click"><span><i class="fa fa-file-pdf-o bigger-110 red"></i></span> PDF</asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" Print" onclick="print_Click"><span><i class="fa fa-print bigger-110 grey"></i></span> Print</asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-md-6"></div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                    
                        <asp:GridView ID="gvMuertesResumen" runat="server" AutoGenerateColumns="False" GridLines="Both" HorizontalAlign="Left" 
                            CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center" 
                            AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvMuertesResumen_PageIndexChanging" >
                        <RowStyle HorizontalAlign="Left"  />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <FooterStyle />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" />
                        <SelectedRowStyle />
                        <HeaderStyle />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        </asp:GridView>
                    </div>
                    <div class="col-md-2"></div>
                    <!-- RESUMEN EN COLUMNA DERECHA -->
                    <div class="col-md-4">
                        
                        <div class="row">
                          <div class="well">
						    <h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Resumen de muertes</h4>
                            <ul class="list-unstyled spaced2">
                            <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i>  
                                <asp:Label ID="titTotalMuertes" runat="server" Text="Cantidad de muertes: " Visible="False"></asp:Label>
                                <strong><asp:Label ID="lblTotalMuertes" runat="server" ></asp:Label></strong></li>
                            <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i>
                            <asp:Label ID="titEnfDif" runat="server" Text="Enfermedades diferentes: " Visible="False"></asp:Label>
                                <strong><asp:Label ID="lblEnfDif" runat="server" ></asp:Label></strong></li>
                            </ul>
					    </div>  
                        </div>
                        <div class="row">
                           <div class="input-group">
                            <span class="input-group-btn">
                                <asp:Button ID="btnListar" runat="server" Text="Mostrar" onclick="btnListar_Click" CssClass="btn btn-white btn-default" />
                            </span>
                            <asp:DropDownList ID="ddlFechas" cssClass="form-control" runat="server"  AutoPostBack="False"></asp:DropDownList>
                            </div> 
                        </div>
                        <div class="row"></div>
                        <div class="row">                   
                            <asp:Panel ID="pnlFechasGraf" runat="server" class="input-group">
                                <h5 class="header smaller lighter blue">Entre dos fechas</h5> 
                                <div class="input-group">                 
                                <span class="input-group-btn">                   
                                <asp:Button ID="Button1" runat="server" Text="Mostrar" onclick="btnListar_gvMuertesResumen" CssClass="btn btn-white btn-default" />
                                </span>
                                <input type="text" id="fechasVentas" name="fechasVentas" placeholder="Fechas" class="form-control"/>  
                                    </div>                			                         
                            </asp:Panel>                   
                        </div>
                        
                    </div>
                </div>
			</div> <!-- fin Análisis sanitario de muertes -->

            <!-- PESTANA 2: Lista de muertes -->
			<div id="listaMuertes" class="tab-pane fade">
			    <div class="page-header">
			        <h1><i class="menu-icon fa fa-list"></i> Lista de muertes</h1>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <div class="pull-right">
                            <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm" Text=" Excel" onclick="excelExport_Click1"><span><i class="fa fa-file-excel-o bigger-110 green"></i></span> Excel</asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" PDF" onclick="pdfExport_Click1"><span><i class="fa fa-file-pdf-o bigger-110 red"></i></span> PDF</asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" Print" onclick="print_Click1"><span><i class="fa fa-print bigger-110 grey"></i></span> Print</asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-md-8"></div>
                </div>
			    <div class="row">
                    <div class="col-md-8">
                        <asp:GridView ID="gvMuertes" runat="server" AutoGenerateColumns="False" GridLines="Both" HorizontalAlign="Left" 
                            CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center" 
                            AllowPaging="true" AllowSorting="true" PageSize="40" OnPageIndexChanging="GvMuertes_PageIndexChanging" >
                        <RowStyle HorizontalAlign="Left"  />
                        <Columns>
                            <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" dataformatstring="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Enfermedad" HeaderText="Enfermedad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Comentarios" HeaderText="Comentarios" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <FooterStyle />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" />
                        <SelectedRowStyle />
                        <HeaderStyle />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        </asp:GridView>
                        <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad de muertes: " Visible="False"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" ></asp:Label><br/>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                           <div class="input-group">
                            <span class="input-group-btn">
                                <asp:Button ID="btnListar1" runat="server" Text="Mostrar" onclick="btnListar_Click1" CssClass="btn btn-white btn-default" />
                            </span>
                            <asp:DropDownList ID="ddlFechas1" cssClass="form-control" runat="server"  AutoPostBack="False"></asp:DropDownList>
                            </div> 
                        </div>
                        <div class="row"></div>
                        <div class="row">                   
                            <asp:Panel ID="Panel1" runat="server" class="input-group">
                                <h5 class="header smaller lighter blue">Entre dos fechas</h5> 
                                <div class="input-group">                 
                                <span class="input-group-btn">                   
                                <asp:Button ID="Button3" runat="server" Text="Mostrar" onclick="btnListar_gvMuertesResumen1" CssClass="btn btn-white btn-default" />
                                </span>
                                <input type="text" id="fechasVentas1" name="fechasVentas1" placeholder="Fechas" class="form-control"/>  
                                    </div>                			                         
                            </asp:Panel>                   
                        </div>
                    </div>
                </div>
            </div> <!-- fin Lista de muertes -->
		</div>
	</div> <!-- fin tabbable -->
    
    
    <script src="js/ace-extra.js" ></script>

    <script src="js/date-time/moment.js"></script>
    <script src="js/date-time/daterangepicker.js"></script>
    
    
    <link href="css/daterangepicker.css" rel="stylesheet" />
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
    
    <script type="text/javascript">

        $("#fechasVentas").daterangepicker({
            locale: {
                applyLabel: 'Confirma',
                cancelLabel: 'Cancela',
                fromLabel: 'Desde',
                toLabel: 'Hasta',
            },
            format: 'DD/MM/YYYY'
        });

        $("#fechasVentas1").daterangepicker({
            locale: {
                applyLabel: 'Confirma',
                cancelLabel: 'Cancela',
                fromLabel: 'Desde',
                toLabel: 'Hasta',
            },
            format: 'DD/MM/YYYY'
        });


    </script>
    
    
    
    
    
    
    
    


</asp:Content>
