<%@ Page Title="tamboprp | análisis de toros" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisisToros.aspx.cs" Inherits="tamboprp.AnalisisToros" %>
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
    <script src="js/bootstrap.js"></script>
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
        <h1><i class="menu-icon fa fa-flask"></i> Análisis de toros utilizados y su efectividad </h1>
    </div>
    
    <!-- comienza tabbable -->
    <div class="tabbable">
		<ul class="nav nav-tabs" id="myTab">
			<li class="active"><a data-toggle="tab" href="#porEfectividad"><i class="blue ace-icon fa fa-list-ol bigger-120"></i> Ranking de toros </a></li>
            <li><a data-toggle="tab" href="#porGenero"><i class="blue ace-icon fa fa-tag bigger-120"></i> Nacimientos por género </a></li>
		</ul>
        <!-- comienza contenido de tabbable -->
		<div class="tab-content">
		    
		    <!-- PESTANA 1: Efectividad de toros -->
			<div id="porEfectividad" class="tab-pane fade in active">
			    <div class="page-header">
                    <h1><i class="menu-icon fa fa-list-ol"></i> Ranking de toros <small><i class="ace-icon fa fa-angle-double-right"></i> según efectividad en sus servicios</small></h1>
                </div>
                <div class="row">
                    <div class="col-md-8">
                    <asp:PlaceHolder ID="phPersonal" runat="server">
                        <asp:GridView ID="gvTorosUtilizados" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center"  
                                AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvTorosUtilizados_PageIndexChanging" >
                            <RowStyle HorizontalAlign="Left"  />
                            <Columns>
                                <asp:BoundField DataField="Registro" HeaderText="Reg. Toro" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Origen" HeaderText="Origen" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="CantServicios" HeaderText="# Servicios" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />    
                                <asp:BoundField DataField="CantDiagP" HeaderText="# Preñadas" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PorcEfectividad" HeaderText="% Efectividad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <FooterStyle />
                            <PagerSettings mode="NumericFirstLast" pagebuttoncount="5" />
                            <SelectedRowStyle />
                            <HeaderStyle />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                        </asp:GridView>    
                    </asp:PlaceHolder><!-- Fin de Listado de Animales con indicación de rechazo -->
                        <asp:Label ID="titCant" runat="server" Text="Cantidad de toros utilizados: " Visible="False"></asp:Label><asp:Label ID="lblCant" runat="server" ></asp:Label><br/>
                   </div>

                    <!-- RESUMEN EN COLUMNA DERECHA -->
                        <div class="col-md-4">
                            <div class="well">
						            <h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Resumen</h4>
                                    <ul class="list-unstyled spaced2">
                                    <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Toros más utilizados: &nbsp;&nbsp;</span>
                                        <strong><asp:Label ID="Label1" runat="server" ></asp:Label></strong></li>
                                    <li class="bigger-110">&nbsp;&nbsp;<i class="ace-icon fa fa-caret-right grey"></i><small>#1 &nbsp;&nbsp;</small><asp:Label ID="lblRegMasUtiliz1" runat="server" ></asp:Label>
                                        &nbsp;&nbsp;<strong><asp:Label ID="lblMasUtiliz1" runat="server" ></asp:Label> - <small class="blue"><asp:Label ID="lblEfect1" runat="server" ></asp:Label></small></strong></li>
                                    <li class="bigger-110">&nbsp;&nbsp;<i class="ace-icon fa fa-caret-right grey"></i><small>#2 &nbsp;&nbsp;</small><asp:Label ID="lblRegMasUtiliz2" runat="server" ></asp:Label>
                                        &nbsp;&nbsp;<strong><asp:Label ID="lblMasUtiliz2" runat="server" ></asp:Label> - <small class="blue"><asp:Label ID="lblEfect2" runat="server" ></asp:Label></small></strong></li>
                                    <li class="bigger-110">&nbsp;&nbsp;<i class="ace-icon fa fa-caret-right grey"></i><small>#3 &nbsp;&nbsp;</small><asp:Label ID="lblRegMasUtiliz3" runat="server" ></asp:Label>
                                        &nbsp;&nbsp;<strong><asp:Label ID="lblMasUtiliz3" runat="server" ></asp:Label> - <small class="blue"><asp:Label ID="lblEfect3" runat="server" ></asp:Label></small></strong></li>
                                    </ul>
                                    <hr/>
                                    <ul class="list-unstyled spaced2">
                                    <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total toros utilizados &nbsp;&nbsp;</span>
                                        <strong><asp:Label ID="lblCantToros" runat="server" ></asp:Label></strong></li>
                                    <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total servicios &nbsp;&nbsp;</span>
                                        <strong><asp:Label ID="lblTotalServ" runat="server" ></asp:Label></strong></li>
                                    <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total preñadas &nbsp;&nbsp;</span>
                                        <strong><asp:Label ID="lblTotalEfect" runat="server" ></asp:Label> - <small class="blue"><asp:Label ID="lblPorcEfect" runat="server" ></asp:Label></small></strong></li>
                                    </ul>
					            </div>
                        </div>
                    </div>
			</div> <!-- fin Efectividad de toros -->
            
            <!-- PESTANA 2: Nacimientos por género -->
			<div id="porGenero" class="tab-pane fade">
			    <div class="page-header">
                    <h1><i class="menu-icon fa fa-tag"></i> Nacimientos por género</h1>
                </div>
                <div class="row">
                    <div class="col-md-8">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                        <asp:GridView ID="gvTorosNacimPorGenero" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center"  
                                AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvTorosNacimPorGenero_PageIndexChanging" >
                            <RowStyle HorizontalAlign="Left"  />
                            <Columns>
                                <asp:BoundField DataField="Registro" HeaderText="Reg. Toro" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Origen" HeaderText="Origen" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="CantNacim" HeaderText="# Nacimientos" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="CantH" HeaderText="# Hembras" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="CantM" HeaderText="# Machos" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PorcHembras" HeaderText="% Hembras" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <FooterStyle />
                            <PagerSettings mode="NumericFirstLast" pagebuttoncount="5" />
                            <SelectedRowStyle />
                            <HeaderStyle />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                        </asp:GridView>    
                    </asp:PlaceHolder><!-- Fin de Listado de Animales con indicación de rechazo -->
                        <asp:Label ID="titCant2" runat="server" Text="Cantidad de toros utilizados: " Visible="False"></asp:Label><asp:Label ID="lblCant2" runat="server" ></asp:Label><br/>
                   </div>
                    <div class="col-md-4"></div>
                </div>
                <br/>
                <i class="menu-icon fa fa-list blue"></i><asp:HyperLink ID="hypGraficasProd" NavigateUrl="ListPartos.aspx" runat="server"> Ver listado de partos y nacimientos</asp:HyperLink>
			</div> <!-- fin Nacimientos por género -->
        </div>
        
	</div><!-- fin tabbable -->

</asp:Content>
