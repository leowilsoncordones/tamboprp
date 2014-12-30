<%@ Page Title="tamboprp | vitalicias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListVitalicias.aspx.cs" Inherits="tamboprp.ListVitalicias" %>
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
        <h1><i class="menu-icon fa fa-star"></i> Listado de vacas vitalicias <small><i class="ace-icon fa fa-angle-double-right"></i> producción mayor a 50.000kg de Leche</small></h1>
    </div>
    <div class="row">
        <div class="col-md-12">
        <h3><asp:Label ID="lblCateg" runat="server" ></asp:Label></h3>
        <p>        
        <asp:GridView ID="gvVitalicias" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
            CssClass="table table-hover table-striped table-bordered table-condensed dataTable"  PagerStyle-CssClass="bs-pagination text-center" 
                AllowPaging="true" AllowSorting="true" PageSize="40" OnPageIndexChanging="GvVitalicias_PageIndexChanging" >
        <RowStyle HorizontalAlign="Left"  />
        <Columns>
            <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Identificacion" HeaderText="Identificacion" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Reg_trazab" HeaderText="Trazabilidad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="NumGen" HeaderText="Gen." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Fecha_Nacim" HeaderText="Fecha Nac." dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
            <asp:BoundField DataField="Reg_madre" HeaderText="Reg. Madre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Reg_padre" HeaderText="Reg. Padre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Categoria" HeaderText="Categoría" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="NumLact" HeaderText="Núm. Lact." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ProdVitalicia" HeaderText="Producción vitalicia" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
        </Columns>
        <FooterStyle />
        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
        <SelectedRowStyle />
        <HeaderStyle />
        <EditRowStyle />
        <AlternatingRowStyle />
        </asp:GridView>
        <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad de animales en la categoría: " Visible="False"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" ></asp:Label><br/>
        </p>
        </div>
    </div>
</asp:Content>
