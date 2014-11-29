<%@ Page Title="tamboprp | personal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Personal.aspx.cs" Inherits="tamboprp.Personal" %>
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
    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/excanvas.js"></script>
</asp:Content>
<asp:Content ID="ContentPersonal" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header"><i class="menu-icon fa fa-users"></i> Personal</h1>
    <asp:PlaceHolder ID="phPersonal" runat="server">
        <asp:GridView ID="gvEmpleados" runat="server" AutoGenerateColumns="False" GridLines="None" 
            HorizontalAlign="Left" Width="50%" CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
            <RowStyle HorizontalAlign="Left"  />
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
                <asp:BoundField DataField="Iniciales" HeaderText="Iniciales" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />               
                <asp:BoundField DataField="Activo" HeaderText="Activo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <FooterStyle />
            <PagerStyle HorizontalAlign="Left" />
            <SelectedRowStyle />
            <HeaderStyle />
            <EditRowStyle />
            <AlternatingRowStyle />
        </asp:GridView>    
    </asp:PlaceHolder><!-- Fin de tabla personal -->
</asp:Content>
