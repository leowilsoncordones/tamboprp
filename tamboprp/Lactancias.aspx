<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Lactancias.aspx.cs" Inherits="tamboprp.Lactancias" %>
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
        <h1><i class="menu-icon fa fa-recycle"></i> Lactancias</h1>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="input-group">
                <span class="input-group-btn">
                    <asp:Button ID="btnListar" runat="server" Text="Listar" onclick="btnListar_Click" CssClass="btn btn-white" />
                </span>
                <asp:DropDownList ID="ddlTipoListado" cssClass="form-control" runat="server" OnSelectedIndexChanged="ddl_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-5">
            <span class="ui-spinner ui-widget ui-widget-content ui-corner-all pull-right">
                <asp:Label ID="titDdlCant" runat="server" Text="Mostrar: " Visible="False"></asp:Label>
                <asp:DropDownList ID="ddlCantidad" runat="server" CssClass="ui-spinner-input" Visible="False"></asp:DropDownList>
            </span>
        </div>
        <div class="col-md-3">
            
        </div>
    </div>

  <div class="row">
        <div class="col-md-9">  
        <h3><asp:Label ID="lblTitulo" runat="server" Visible="False"></asp:Label></h3>
        <p>        
        <asp:GridView ID="gvLactancias" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" PagerStyle-CssClass="bs-pagination text-center"
            CssClass="table table-hover table-striped table-bordered table-condensed dataTable"
            AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvLactancias_PageIndexChanging" >
        <RowStyle HorizontalAlign="Left"  />
        <Columns>
            <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Numero" HeaderText="Numero" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Dias" HeaderText="Dias" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Leche305" HeaderText="Leche 305" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Grasa305" HeaderText="Grasa 305" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Leche365" HeaderText="Leche 365" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
            <asp:BoundField DataField="Grasa365" HeaderText="Grasa 365" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ProdLeche" HeaderText="Prod. Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PorcentajeGrasa" HeaderText="% Grasa" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
        </Columns>
        <FooterStyle />
        <PagerSettings mode="Numeric" pagebuttoncount="5" />
        <SelectedRowStyle />
        <HeaderStyle />
        <EditRowStyle />
        <AlternatingRowStyle />
        </asp:GridView>
        </p>
        <br/><br/>
        <div>
            <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad de elementos en la lista: " Visible="False"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" ></asp:Label>
        </div>
        </div>
      <div class="col-md-3"></div>
  </div>

</asp:Content>
