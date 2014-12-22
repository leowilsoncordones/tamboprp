<%@ Page Title="tamboprp | último control" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlProduccionUltimo.aspx.cs" Inherits="tamboprp.ControlProduccionUltimo" %>
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
            <h1><i class="menu-icon fa fa-cogs"></i> Último control de producción <small><i class="ace-icon fa fa-angle-double-right"></i> <asp:Label ID="lblFecha" runat="server" Visible="False"></asp:Label></small></h1>
        </div>
        <div class="row">
        <div class="col-md-9">
            <asp:GridView ID="gvControlProdUltimo" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center" 
                AllowPaging="true" AllowSorting="true" PageSize="40" OnPageIndexChanging="GvControlProdUltimo_PageIndexChanging" >
            <RowStyle HorizontalAlign="Left"  />
            <Columns>
                <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="NumLact" HeaderText="Núm. Lact." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Dias" HeaderText="Dias Lact." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Leche" HeaderText="Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
                <asp:BoundField DataField="ProdLeche" HeaderText="Prod. Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
                <asp:BoundField DataField="FechaServicio" HeaderText="Fecha Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="NumServicio" HeaderText="Núm. Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Diag" HeaderText="Diag." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="FechaProbParto" HeaderText="Fecha Prob. Parto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <FooterStyle />
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" />
            <SelectedRowStyle />
            <HeaderStyle />
            <EditRowStyle />
            <AlternatingRowStyle />
            </asp:GridView>
            <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad de vacas en ordeñe: " Visible="False"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" ></asp:Label><br/>
        </div>
        <div class="col-md-3">
        </div>

    </div>
</asp:Content>
