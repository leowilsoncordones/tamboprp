<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Lactancias.aspx.cs" Inherits="tamboprp.Lactancias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <br/>
    <br/>
    <h2>Lactancias</h2>

    <div class="row">
        <div class="col-md-4 wrapper">
            <div class="input-group">
                <span class="input-group-btn">
                    <asp:Button ID="btnListar" runat="server" Text="Listar" onclick="btnListar_Click" CssClass="btn btn-primary" />
                </span>
                <asp:DropDownList ID="ddlTipoListado" cssClass="form-control" runat="server" ></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-8"></div>
    </div>
    
    <div>
        <h3><asp:Label ID="lblTitulo" runat="server" Visible="False"></asp:Label></h3>
        <p>        
        <asp:GridView ID="gvLactancias" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
            CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
        <RowStyle HorizontalAlign="Left"  />
        <Columns>
            <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Numero" HeaderText="Numero" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Dias" HeaderText="Dias" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Leche305" HeaderText="Leche 305" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Grasa305" HeaderText="Grasa 305" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Leche365" HeaderText="Leche 365" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
            <asp:BoundField DataField="Grasa365" HeaderText="Grasa 365" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ProdLeche" HeaderText="Prod. Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PorcentajeGrasa" HeaderText="% Grasa" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
        </Columns>
        <FooterStyle />
        <PagerStyle HorizontalAlign="Left" />
        <SelectedRowStyle />
        <HeaderStyle />
        <EditRowStyle />
        <AlternatingRowStyle />
        </asp:GridView>
        <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad de animales en la lista: " Visible="False"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" ></asp:Label><br/>
        </p>
    </div><br />

</asp:Content>
