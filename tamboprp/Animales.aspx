<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Animales.aspx.cs" Inherits="tamboprp.Animales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <br/>
    <h2>Ficha de animal</h2>
        <div class="row">
        <div class="col-md-4 wrapper">        
            <div class="input-group">
                <span class="input-group-btn">
                    <asp:Button ID="btnBuscarAnimal" runat="server" onclick="btnBuscarAnimal_Click" Text="Buscar" CssClass="btn btn-primary" />
                </span>
                <input type="text" class="form-control" runat="server" id="regBuscar" />
            </div>
        </div>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlSimilares" cssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlSimilares_SelectedIndexChanged" runat="server" Visible="false" AppendDataBoundItems="true" />
        </div>
        <div class="col-md-4">
        </div>
        </div>
        <br/>
        <div class="jumbotron">
            <h1><asp:Label ID="lblAnimal" runat="server" Text="Registro" cssClass="label label-default"></asp:Label></h1>
            <p>
                <asp:Label ID="lblVivo" runat="server" Text="" CssClass="label label-success"></asp:Label><br/>
                <asp:Label ID="titSexo" runat="server" Text="Sexo: " ></asp:Label><asp:Label ID="lblSexo" runat="server" ></asp:Label><br/>
                <asp:Label ID="titIdentif" runat="server" Text="Identificación: "></asp:Label><asp:Label ID="lblIdentif" runat="server" ></asp:Label><br/>
                <asp:Label ID="titTraz" runat="server" Text="Trazabilidad: "></asp:Label><asp:Label ID="lblTraz" runat="server" ></asp:Label><br/>
                <asp:Label ID="titNombre" runat="server" Text="Nombre: "></asp:Label><asp:Label ID="lblNombre" runat="server" ></asp:Label><br/>
                <asp:Label ID="titFechaNac" runat="server" Text="Fecha Nacimiento: "></asp:Label><asp:Label ID="lblFechaNac" runat="server" dataformatstring="{0:dd/MM/yyyy}" ></asp:Label><br/>
                <asp:Label ID="titCategoria" runat="server" Text="Categoría: "></asp:Label><asp:Label ID="lblCategoria" runat="server" ></asp:Label><br/>
                <asp:Label ID="titGen" runat="server" Text="Generación: "></asp:Label><asp:Label ID="lblGen" runat="server" ></asp:Label><br/>
                <asp:Label ID="titRegPadre" runat="server" Text="Reg. Padre: "></asp:Label><asp:Label ID="lblRegPadre" runat="server" ></asp:Label><br/>
                <asp:Label ID="titRegMadre" runat="server" Text="Reg. Madre: "></asp:Label><asp:Label ID="lblRegMadre" runat="server" ></asp:Label><br/>
                <asp:Label ID="titOrigen" runat="server" Text="Origen: "></asp:Label><asp:Label ID="lblOrigen" runat="server" ></asp:Label><br/><br/>
                <a class="btn btn-default" href="#" role="button">Ver fotos</a>
                <asp:Button ID="btnHistorial" runat="server" onclick="btnVerHistorial_Click" Text="Ver Historial" CssClass="btn btn-primary" />
            </p>
            <p>
            <asp:GridView ID="gvHistoria" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
            <RowStyle HorizontalAlign="Left"  />
            <Columns>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="NombreEvento" HeaderText="Evento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />   
                <asp:BoundField DataField="Observaciones" HeaderText="Observaciones del evento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
                <asp:BoundField DataField="Comentarios" HeaderText="Comentario" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <FooterStyle />
            <PagerStyle HorizontalAlign="Left" />
            <SelectedRowStyle />
            <HeaderStyle />
            <EditRowStyle />
            <AlternatingRowStyle />
            </asp:GridView>
                <h4><asp:Label ID="titHistorico" runat="server" Text="Cantidad de eventos: " Visible="False"></asp:Label><asp:Label ID="lblHistorico" runat="server" ></asp:Label></h4><br/>
            </p>
        </div>
        <br/>

</asp:Content>
