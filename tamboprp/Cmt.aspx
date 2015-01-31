<%@ Page Title="tamboprp | c.m.t." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cmt.aspx.cs" Inherits="tamboprp.Cmt" %>
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
        <h1><i class="menu-icon fa fa-history"></i> Diagnósticos C.M.T. <small><i class="ace-icon fa fa-angle-double-right"></i> información histórica</small></h1>
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
            <asp:GridView ID="gvCmt" runat="server" AutoGenerateColumns="False" GridLines="Both" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center"  
                    AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvCmt_PageIndexChanging" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha de calif." dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Comentarios" HeaderText="Comentario" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
                </Columns>
                <FooterStyle />
                <PagerSettings mode="NumericFirstLast" pagebuttoncount="5" />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
            </asp:GridView>    
        </asp:PlaceHolder><!-- Fin de tabla CMT -->
            <asp:Label ID="titCant" runat="server" Text="Cantidad de diagnósticos CMT: " Visible="False"></asp:Label><asp:Label ID="lblCant" runat="server" ></asp:Label><br/>
            </div>
        
        <div class="col-md-2">
        </div>

        <!-- RESUMEN EN COLUMNA DERECHA -->
        <div class="col-md-4">
        </div>
    </div>
</asp:Content>
