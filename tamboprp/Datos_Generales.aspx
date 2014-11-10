<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Datos_Generales.aspx.cs" Inherits="tamboprp.Datos_Generales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="jumbotron">
    <br/>
    <h3>Datos Generales</h3>
    <br/>
    <p>
        <asp:Label ID="titCantAbortos" runat="server" Text="Abortos este año: " ></asp:Label><asp:Label ID="lblAbortosEsteAno" runat="server" ></asp:Label><br/>
        <asp:Label ID="titCantAnControl" runat="server" Text="Cantidad de animales en último control lechero: " ></asp:Label><asp:Label ID="lblCantAnUltControl" runat="server" ></asp:Label><br/>
        <asp:Label ID="titSumLecheUltControl" runat="server" Text="Producción total de leche en último control: " ></asp:Label><asp:Label ID="lblSumLecheUltControl" runat="server" ></asp:Label><br/>
        <asp:Label ID="titPromLecheUltControl" runat="server" Text="Producción promedio de leche en último control: " ></asp:Label><asp:Label ID="lblPromLecheUltControl" runat="server" ></asp:Label><br/>
        <asp:Label ID="titSumGrasaUltControl" runat="server" Text="Producción total de grasa en último control: " ></asp:Label><asp:Label ID="lblSumGrasaUltControl" runat="server" ></asp:Label><br/>
        <asp:Label ID="titPromGrasaUltControl" runat="server" Text="Producción promedio de grasa en último control: " ></asp:Label><asp:Label ID="lblPromGrasaUltControl" runat="server" ></asp:Label><br/>
        <a class="btn btn-default" href="#" role="button">Ver fotos</a>
    </p>
</div>

</asp:Content>
