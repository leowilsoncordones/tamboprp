<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Datos_Generales.aspx.cs" Inherits="tamboprp.Datos_Generales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <br/>
    <h2>Datos Generales</h2>
    <div class="jumbotron">
        <p>
        <h3>Datos del rodeo</h3>
        <asp:Label ID="titCAntOrdene" runat="server" Text="Vacas en ordeñe: " ></asp:Label><asp:Label ID="lblCAntOrdene" runat="server" ></asp:Label><br/>
        <asp:Label ID="titCantEntoradas" runat="server" Text="Vacas entoradas: " ></asp:Label><asp:Label ID="lblCantEntoradas" runat="server" ></asp:Label><br/>
        <asp:Label ID="titCantSecas" runat="server" Text="Vacas secas: " ></asp:Label><asp:Label ID="lblCantSecas" runat="server" ></asp:Label><br/>
        <asp:Label ID="titCantAbortos" runat="server" Text="Abortos este año: " ></asp:Label><asp:Label ID="lblAbortosEsteAno" runat="server" ></asp:Label><br/>
        <h3>Último control lechero <asp:Label ID="lblFechaUltControl" runat="server" ></asp:Label></h3>
        <asp:Label ID="titCantAnControl" runat="server" Text="Cantidad de animales en último control lechero: " ></asp:Label><asp:Label ID="lblCantAnUltControl" runat="server" ></asp:Label><br/>
        <asp:Label ID="titSumLecheUltControl" runat="server" Text="Producción total de leche en último control: " ></asp:Label><asp:Label ID="lblSumLecheUltControl" runat="server" ></asp:Label><br/>
        <asp:Label ID="titPromLecheUltControl" runat="server" Text="Producción promedio de leche en último control: " ></asp:Label><asp:Label ID="lblPromLecheUltControl" runat="server" ></asp:Label><br/>
        <asp:Label ID="titSumGrasaUltControl" runat="server" Text="Producción total de grasa en último control: " ></asp:Label><asp:Label ID="lblSumGrasaUltControl" runat="server" ></asp:Label><br/>
        <asp:Label ID="titPromGrasaUltControl" runat="server" Text="Producción promedio de grasa en último control: " ></asp:Label><asp:Label ID="lblPromGrasaUltControl" runat="server" ></asp:Label><br/>
        </p>
    </div><br/>
</asp:Content>
