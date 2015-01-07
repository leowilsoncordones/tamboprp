<%@ Page Title="tamboprp | inseminaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inseminaciones.aspx.cs" Inherits="tamboprp.Inseminaciones" %>
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
        <h1><i class="menu-icon fa fa-hand-o-right"></i> Inseminaciones con diágnostico de preñez confirmado </h1>
    </div>
    <div class="row">
        <div class="col-md-8">
        <asp:GridView ID="gvInseminaciones" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                CssClass="table table-hover table-striped table-bordered table-condensed dataTable"  PagerStyle-CssClass="bs-pagination text-center"  
                    AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvInseminaciones_PageIndexChanging" >
            <RowStyle HorizontalAlign="Left"  />
            <Columns>
                <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />  
                <asp:BoundField DataField="FechaServicio" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Fecha serv." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="RegistroPadre" HeaderText="Reg. Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="FechaDiagnostico" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Fecha diag." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Diagnostico" HeaderText="Diag." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Inseminador" HeaderText="Inseminador" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <FooterStyle />
            <PagerSettings mode="NumericFirstLast" pagebuttoncount="5" />
            <SelectedRowStyle />
            <HeaderStyle />
            <EditRowStyle />
            <AlternatingRowStyle />
        </asp:GridView>
        <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad total de animales: " Visible="True"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" Visible="False"></asp:Label><br/>
        </div>
        <!-- RESUMEN EN COLUMNA DERECHA -->
            <div class="col-md-4">
                <div class="well">
						<h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Resumen por inseminador</h4>
                        <ul class="list-unstyled spaced2" runat="server" id="listaInseminadores">
                        </ul>
                        <hr/>
                        <ul class="list-unstyled spaced2">
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total preñadas &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblTotal" runat="server" ></asp:Label></strong></li>                        
                        </ul>
					</div>
            </div>
    </div>

</asp:Content>
