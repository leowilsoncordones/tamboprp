<%@ Page Title="tamboprp | partos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListPartos.aspx.cs" Inherits="tamboprp.ListPartos" %>
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
        <h1><i class="menu-icon fa fa-list"></i> Listado de partos </h1>
    </div>
    <div class="row">
        <div class="col-md-8">
        <asp:PlaceHolder ID="phPersonal" runat="server">
            <asp:GridView ID="gvListPartos" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center"  
                    AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvListPartos_PageIndexChanging" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha de parto" dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
                    <asp:BoundField DataField="Sexo_parto" HeaderText="Sexo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Reg_hijo" HeaderText="Reg. cría" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Comentarios" HeaderText="Comentarios" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                </Columns>
                <FooterStyle />
                <PagerSettings mode="NumericFirstLast" pagebuttoncount="5" />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
            </asp:GridView>    
        </asp:PlaceHolder><!-- Fin de tabla partos -->
            <asp:Label ID="titCant" runat="server" Text="Cantidad de partos: " Visible="False"></asp:Label><asp:Label ID="lblCant" runat="server" ></asp:Label><br/>
            </div>

        <!-- RESUMEN EN COLUMNA DERECHA -->
            <div class="col-md-4">
                <div class="well">
						<h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Resumen de partos</h4>
                        <ul class="list-unstyled spaced2">
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Hembras &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblH" runat="server" ></asp:Label>&nbsp;&nbsp;-&nbsp;&nbsp;<small class="blue"><asp:Label ID="lblPromH" runat="server" ></asp:Label></small></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Machos &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblM" runat="server" ></asp:Label>&nbsp;&nbsp;-&nbsp;&nbsp;<small class="blue"><asp:Label ID="lblPromM" runat="server" ></asp:Label></small></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Mellizos &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblMellizos" runat="server" ></asp:Label></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Trillizos &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblTrillizos" runat="server" ></asp:Label></strong></li>
                        
                        </ul>
                        <hr/>
                        <ul class="list-unstyled spaced2">
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total partos &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblTotalPartos" runat="server" ></asp:Label></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total muertos &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblTotalMuertos" runat="server" ></asp:Label>&nbsp;&nbsp;-&nbsp;&nbsp;<small class="blue"><asp:Label ID="lblPromMuertos" runat="server" ></asp:Label></small></strong></li>
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total nacimientos &nbsp;&nbsp;</span>
                            <strong><asp:Label ID="lblTotalNac" runat="server" ></asp:Label>&nbsp;&nbsp;-&nbsp;&nbsp;<small class="blue"><asp:Label ID="lblPromNac" runat="server" ></asp:Label></small></strong></li>
                        </ul>
					</div>
            </div>
        </div>

</asp:Content>
