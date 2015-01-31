<%@ Page Title="tamboprp | en lactancia y sin servicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LactanciasSinServ80.aspx.cs" Inherits="tamboprp.LactanciasSinServ80" %>
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
        <h1><i class="menu-icon fa fa-bullhorn"></i> Animales con más de 80 días en lactancia sin servicio </h1>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="pull-right">
                <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm" Text=" Excel" onclick="excelExport_Click"><span><i class="fa fa-file-excel-o bigger-110 green"></i></span> Excel</asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" PDF" onclick="pdfExport_Click"><span><i class="fa fa-file-pdf-o bigger-110 red"></i></span> PDF</asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" Print" onclick="print_Click"><span><i class="fa fa-print bigger-110 grey"></i></span> Print</asp:LinkButton>
            </div>
        </div>
        <div class="col-md-8"></div>
    </div>
    <div class="space-6"></div>
    <div class="row">
        <div class="col-md-8">
        <asp:GridView ID="gvServicios" runat="server" AutoGenerateColumns="False" GridLines="Both" HorizontalAlign="Left" 
                CssClass="table table-hover table-striped table-bordered table-condensed dataTable"  PagerStyle-CssClass="bs-pagination text-center"  
                    AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvLactanciasSinServ80_PageIndexChanging" >
            <RowStyle HorizontalAlign="Left"  />
            <Columns>
                <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />  
                <asp:BoundField DataField="FechaServicio" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Fecha de Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="RegistroPadre" HeaderText="Caravana de Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
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
        <div class="col-md-4">
        </div>
    </div>

</asp:Content>
