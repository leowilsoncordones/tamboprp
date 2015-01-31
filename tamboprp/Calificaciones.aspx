<%@ Page Title="tamboprp | calificaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calificaciones.aspx.cs" Inherits="tamboprp.Calificaciones" %>
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
        <h1><i class="menu-icon fa fa-sort-numeric-desc"></i> Calificaciones</h1>
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
    <div class="space-6"></div>
    <div class="row">
        <div class="col-md-6">
        <asp:PlaceHolder ID="phPersonal" runat="server">
            <asp:GridView ID="gvCalificaciones" runat="server" AutoGenerateColumns="False" GridLines="Both" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center"  
                    AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvCalificaciones_PageIndexChanging" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Letras" HeaderText="Letras" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
                    <asp:BoundField DataField="Puntos" HeaderText="Puntos" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />               
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha de calif." dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                </Columns>
                <FooterStyle />
                <PagerSettings mode="NumericFirstLast" pagebuttoncount="5" />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
            </asp:GridView>    
        </asp:PlaceHolder><!-- Fin de tabla calificaciones -->
            <asp:Label ID="titCantCalif" runat="server" Text="Cantidad de calificaciones: " Visible="False"></asp:Label><asp:Label ID="lblCantCalif" runat="server" ></asp:Label><br/>
            </div>
        
        <div class="col-md-2">
            
        </div>

        <!-- RESUMEN EN COLUMNA DERECHA -->
            <div class="col-md-4">
                <div class="well">
						<h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Resumen de calificaciones</h4>
                        <ul class="list-unstyled spaced2">
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> EX <small>(100 - 90)</small> &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblEX" runat="server" ></asp:Label></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> MB <small>(89 - 85)</small> &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblMB" runat="server" ></asp:Label></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> BM <small>(84 - 80)</small> &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblBM" runat="server" ></asp:Label></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> B <small>(79 - 75)</small> &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblB" runat="server" ></asp:Label></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> R <small>( < 75)</small> &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblR" runat="server" ></asp:Label></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total calificadas &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblTotal" runat="server" ></asp:Label></strong></li>
                        </ul>
                        <hr/>
                        <ul class="list-unstyled spaced2">
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Puntos promedio &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblProm" runat="server" ></asp:Label></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Calificación máxima &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblMax" runat="server" ></asp:Label></strong></li>                        
                        </ul>
					</div>
            </div>
        </div>

</asp:Content>
