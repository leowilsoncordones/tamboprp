<%@ Page Title="tamboprp | categorías" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="tamboprp.Categorias" %>
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
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="page-header">
        <h1><i class="menu-icon fa fa-tags"></i> Lista de categorías de animal </h1>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div>
                <p>        
                <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Id_categ" HeaderText="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                </Columns>
                <FooterStyle />
                <PagerSettings />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
                </asp:GridView>
                </p>
            </div>
        </div>
        <div class="col-md-8"></div>
    </div>

</asp:Content>
