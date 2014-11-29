<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Genealogia.aspx.cs" Inherits="tamboprp.Genealogia" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header"><i class="menu-icon fa fa-sitemap"></i> Genealogía</h1>
        <!-- Busqueda -->
        <div class="row">
            <div class="col-md-4">        
                <div class="input-group input-group-lg">
                    <span class="input-group-btn">
                        <asp:Button ID="btnBuscarAnimal" runat="server" onclick="btnBuscarAnimal_Click" Text="Buscar" CssClass="btn btn-white btn-default" />
                    </span>
                    <input type="text" class="form-control" runat="server" id="regBuscar" placeholder="Registro"/>
                </div>
            </div>
            <div class="col-md-4" id="divContenedorDdl" runat="server" >
            </div>
            <div class="col-md-4">
            </div>
        </div>
        <br/>
    <!-- arbol genealogico -->
    <div class="row clearfix">
		<div class="col-md-4">
            <asp:Panel ID="PanelAnimal" runat="server" Height="500px">
                <h5><asp:Label ID="Animal" runat="server"></asp:Label></h5>
                <asp:Label ID="lblEstado" Visible="False" runat="server" Text=""></asp:Label>
            </asp:Panel>
        </div>
        <div class="col-md-4">
            <asp:Panel ID="PanelMadre" runat="server" Height="250px">
                <h5><asp:Label ID="Madre" runat="server">Madre: </asp:Label></h5>
                <asp:Label ID="lblEstadoMadre" Visible="False" runat="server" Text=""></asp:Label>
            </asp:Panel>
            <asp:Panel ID="PanelPadre" runat="server" BackColor="#eeeeee" Height="250px">
                <h5><asp:Label ID="Padre" runat="server">Padre: </asp:Label></h5>
                <asp:Label ID="lblEstadoPadre" Visible="False" runat="server" Text=""></asp:Label>
            </asp:Panel>
        </div>
        <div class="col-md-4">
            <asp:Panel ID="PanelAbuelaM" runat="server" Height="125px">
                <h5><asp:Label ID="AbuelaM" runat="server">Abuela Materna: </asp:Label></h5>
                <asp:Label ID="lblEstadoAbuelaM" Visible="False" runat="server" Text=""></asp:Label>
            </asp:Panel>
            <asp:Panel ID="PanelAbueloM" runat="server" BackColor="#eeeeee" Height="125px">
                <h5><asp:Label ID="AbueloM" runat="server">Abuelo Materno: </asp:Label></h5>
                <asp:Label ID="lblEstadoAbueloM" Visible="False" runat="server" Text=""></asp:Label>
            </asp:Panel>
            <asp:Panel ID="PanelAbuelaP" runat="server" Height="125px">
                <h5><asp:Label ID="AbuelaP" runat="server">Abuela Paterna: </asp:Label></h5>
                <asp:Label ID="lblEstadoAbuelaP" Visible="False" runat="server" Text=""></asp:Label>
            </asp:Panel>
            <asp:Panel ID="PanelAbueloP" runat="server" BackColor="#eeeeee" Height="125px">
                <h5><asp:Label ID="AbueloP" runat="server">Abuelo Paterno: </asp:Label></h5>
                <asp:Label ID="lblEstadoAbueloP" Visible="False" runat="server" Text=""></asp:Label>
            </asp:Panel>
        </div>
    </div>


</asp:Content>
