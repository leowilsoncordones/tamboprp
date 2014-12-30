<%@ Page Title="tamboprp | análisis de bajas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisisBajas.aspx.cs" Inherits="tamboprp.AnalisisBajas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-money"></i> Análisis de bajas</h1>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="well">
				<h4 class="blue smaller">Muertes <asp:Label ID="lblMuertes" runat="server" ></asp:Label></h4>
                <ul class="list-unstyled spaced2">
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantPartos" runat="server" Text="Cantidad de muertes: " ></asp:Label>
                    <strong><asp:Label ID="lblCantPartos" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                </ul>
			</div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="well">
				<h4 class="blue smaller">Ventas <asp:Label ID="lblVentas" runat="server" ></asp:Label></h4>
                <ul class="list-unstyled spaced2">
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantLactComp" runat="server" Text="Cantidad total de ventas: " ></asp:Label>
                    <strong><asp:Label ID="lblCantLactComp" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                </ul>
			</div>
        </div>
    </div>

</asp:Content>
