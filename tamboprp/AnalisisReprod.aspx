<%@ Page Title="tamboprp | análisis reproductivo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisisReprod.aspx.cs" Inherits="tamboprp.AnalisisReprod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-eye"></i> Análisis reproductivo</h1>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="well">
				<h4 class="blue smaller">Servicios <asp:Label ID="Label1" runat="server" ></asp:Label></h4>
                <ul class="list-unstyled spaced2">
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantCSS" runat="server" Text="Cantidad de celos sin servicios: " ></asp:Label>
                    <strong><asp:Label ID="lblCantCSS" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantServPorPrenez" runat="server" Text="Cantidad de servicios por preñez: " ></asp:Label>
                    <strong><asp:Label ID="lblCantServPorPrenez" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titEdadProm" runat="server" Text="Edad promedio del primer servicio: " ></asp:Label>
                    <strong><asp:Label ID="lblEdadProm" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantServNatural" runat="server" Text="Cantidad de servicios por monta natural: " ></asp:Label>
                    <strong><asp:Label ID="lblCantSecas" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantServSemen" runat="server" Text="Cantidad de servicios por semen: " ></asp:Label>
                    <strong><asp:Label ID="lblCantServSemen" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantToros" runat="server" Text="Cantidad de toros usados: " ></asp:Label>
                    <strong><asp:Label ID="lblCantToros" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                </ul>
			</div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="well">
				<h4 class="blue smaller">Partos, e indicadores reproductivos <asp:Label ID="lblPartos" runat="server" ></asp:Label></h4>
                <ul class="list-unstyled spaced2">
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantPartos" runat="server" Text="Cantidad de partos por género: " ></asp:Label>
                    <strong><asp:Label ID="lblCantPartos" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titPartosNumLact" runat="server" Text="Cantidad de partos por número de lactancia: " ></asp:Label>
                    <strong><asp:Label ID="lblPartosNumLact" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantAbortos" runat="server" Text="Abortos: " ></asp:Label>
                    <strong><asp:Label ID="lblAbortos" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titPorcAnimPrenados" runat="server" Text="Porcentajes de animales preñados y sus categorías: " ></asp:Label>
                    <strong><asp:Label ID="lblPorcAnimPrenados" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titIntervPPS" runat="server" Text="Intervalo PPS: " ></asp:Label>
                    <strong><asp:Label ID="lblIntervPPS" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titIntervPC" runat="server" Text="Intervalo PC: " ></asp:Label>
                    <strong><asp:Label ID="lblIntervPC" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                </ul>
			</div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="well">
				<h4 class="blue smaller">Secados <asp:Label ID="lblSecados" runat="server" ></asp:Label></h4>
                <ul class="list-unstyled spaced2">
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantLactComp" runat="server" Text="Por lactancia completa: " ></asp:Label>
                    <strong><asp:Label ID="lblCantLactComp" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantRazSanit" runat="server" Text="Por razones sanitarias: " ></asp:Label>
                    <strong><asp:Label ID="lblCantRazSanit" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantBajaProd" runat="server" Text="Por baja producción: " ></asp:Label>
                    <strong><asp:Label ID="lblCantBajaProd" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantPrenezAv" runat="server" Text="Por preñez avanzada: " ></asp:Label>
                    <strong><asp:Label ID="lblCantPrenezAv" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                </ul>
			</div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="well">
				<h4 class="blue smaller">Diagnósticos <asp:Label ID="lblDiag" runat="server" ></asp:Label></h4>
                <ul class="list-unstyled spaced2">
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titPrenezConf" runat="server" Text="Cantidad preñez confirmada: " ></asp:Label>
                    <strong><asp:Label ID="lblPrenezConf" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantVacias" runat="server" Text="Cantidad vacías: " ></asp:Label>
                    <strong><asp:Label ID="lblCantVacias" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantDudosas" runat="server" Text="Cantidad dudosas: " ></asp:Label>
                    <strong><asp:Label ID="lblCantDudosas" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                </ul>
			</div>
        </div>
    </div>

</asp:Content>
