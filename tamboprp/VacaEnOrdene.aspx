<%@ Page Title="Vacas en Ordeñe" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VacaEnOrdene.aspx.cs" Inherits="tamboprp.VacaEnOrdene" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <br/>
    <br/>
    <h2>Tabla analítica de vacas en ordeñe</h2>
    <div class="jumbotron">
        <p>
            <asp:Label ID="titCant" runat="server" Text="Cantidad en la categoría: " ></asp:Label><asp:Label ID="lblCant" runat="server" Text="" CssClass="label label-success"></asp:Label><br/>
            <asp:Label ID="titPromProdLecheLts" runat="server" Text="Promedio de producción de leche (litros): " ></asp:Label><asp:Label ID="lblPromProdLecheLts" runat="server" ></asp:Label><br/>
            <asp:Label ID="titCantL1" runat="server" Text="Cantidad en lactancia 1: "></asp:Label><asp:Label ID="lblCantL1" runat="server" ></asp:Label><br/>
            <asp:Label ID="titCantL2" runat="server" Text="Cantidad en lactancia 2: "></asp:Label><asp:Label ID="lblCantL2" runat="server" ></asp:Label><br/>
            <asp:Label ID="titCantOtrasL" runat="server" Text="Cantidad en otras lactancias: "></asp:Label><asp:Label ID="lblCantOtrasL" runat="server" ></asp:Label><br/>
            <asp:Label ID="titConSSP" runat="server" Text="Con servicios y sin preñez: "></asp:Label><asp:Label ID="lblConSSP" runat="server" ></asp:Label><br/>
            <asp:Label ID="titPrenezConf" runat="server" Text="Preñez confirmada: "></asp:Label><asp:Label ID="lblPrenezConf" runat="server" ></asp:Label><br/>
            <asp:Label ID="titPromL" runat="server" Text="Promedio de lactancias (días): "></asp:Label><asp:Label ID="lblPromL" runat="server" ></asp:Label><br/>
        </p>
     </div>
     <br/>

</asp:Content>
