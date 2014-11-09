<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Animales.aspx.cs" Inherits="tamboprp.Animales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1>Ficha de animal</h1>
        <div class="input-group">
            <span class="input-group-btn">
                <asp:Button ID="btnBuscarAnimal" runat="server" onclick="btnBuscarAnimal_Click" Text="Buscar" CssClass="btn btn-default" />
            </span>
            <input type="text" class="form-control" runat="server" id="regBuscar" />
        </div>
        <br/>
        <div class="jumbotron">
            <h1><asp:Label ID="lblAnimal" runat="server" Text="Registro" cssClass="label label-default"></asp:Label></h1>
            <p>
                <asp:Label ID="lblVivo" runat="server" Text="Vivo" CssClass="label label-success"></asp:Label><br/>
                <asp:Label ID="titSexo" runat="server" Text="Sexo: " ></asp:Label><asp:Label ID="lblSexo" runat="server" ></asp:Label><br/>
                <asp:Label ID="titIdentif" runat="server" Text="Identificación: "></asp:Label><asp:Label ID="lblIdentif" runat="server" ></asp:Label><br/>
                <asp:Label ID="titTraz" runat="server" Text="Trazabilidad: "></asp:Label><asp:Label ID="lblTraz" runat="server" ></asp:Label><br/>
                <asp:Label ID="titNombre" runat="server" Text="Nombre: "></asp:Label><asp:Label ID="lblNombre" runat="server" ></asp:Label><br/>
                <asp:Label ID="titFechaNac" runat="server" Text="Fecha Nacimiento: "></asp:Label><asp:Label ID="lblFechaNac" runat="server" ></asp:Label><br/>
                <asp:Label ID="titCategoria" runat="server" Text="Categoría: "></asp:Label><asp:Label ID="lblCategoria" runat="server" ></asp:Label><br/>
                <asp:Label ID="titGen" runat="server" Text="Generación: "></asp:Label><asp:Label ID="lblGen" runat="server" ></asp:Label><br/>
                <asp:Label ID="titRegPadre" runat="server" Text="Reg. Padre: "></asp:Label><asp:Label ID="lblRegPadre" runat="server" ></asp:Label><br/>
                <asp:Label ID="titRegMadre" runat="server" Text="Reg. Madre: "></asp:Label><asp:Label ID="lblRegMadre" runat="server" ></asp:Label><br/>
                <asp:Label ID="titOrigen" runat="server" Text="Origen: "></asp:Label><asp:Label ID="lblOrigen" runat="server" ></asp:Label><br/><br/>
                <a class="btn btn-default" href="#" role="button">Ver fotos</a>
            </p>
            <p>
                <asp:Label ID="titHistorico" runat="server" Text="Cantidad de eventos: " ></asp:Label><asp:Label ID="lblHistorico" runat="server" ></asp:Label><br/>
            </p>
        </div>
        
        <!-- Small modal -->
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#bs-example-modal-sm">Small modal</button>
        <div class="modal fade" id="bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-sm">
            <div class="modal-content">
              List
            </div>
          </div>
        </div>

</asp:Content>
