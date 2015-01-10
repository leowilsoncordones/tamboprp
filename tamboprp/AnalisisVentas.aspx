<%@ Page Title="tamboprp | análisis de bajas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisisVentas.aspx.cs" Inherits="tamboprp.AnalisisVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-money"></i> Análisis de ventas</h1>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="well">
				<h4 class="blue smaller">Ventas <asp:Label ID="lblVentas" runat="server" ></asp:Label></h4>
                <ul class="list-unstyled spaced2">
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantVentas" runat="server" Text="Cantidad total de ventas: " ></asp:Label>
                    <strong><asp:Label ID="lblCantVentas" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titAFrigorifico" runat="server" Text="Ventas a Frigorífico: " ></asp:Label>
                    <strong><asp:Label ID="lblAFrigorifico" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titRecienNac" runat="server" Text="Ventas recién nacidos: " ></asp:Label>
                    <strong><asp:Label ID="lblRecienNac" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titPorVieja" runat="server" Text="Ventas por edad avanzada: " ></asp:Label>
                    <strong><asp:Label ID="lblPorVieja" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                </ul>
			</div>
        </div>
        <div class="col-md-6">
        </div>
    </div>

</asp:Content>
