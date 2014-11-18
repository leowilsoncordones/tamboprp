<%@ Page Title="Control Producción Ultimo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlProduccionUltimo.aspx.cs" Inherits="tamboprp.ControlProduccionUltimo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <br/>
    <div>
        <h3>Último control de producción: <asp:Label ID="lblFecha" runat="server" Visible="False"></asp:Label></h3>
        <p>        
        <asp:GridView ID="gvControlProdUltimo" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
            CssClass="table table-hover table-striped table-bordered table-condensed dataTable"
            AllowPaging="true" AllowSorting="true" PageSize="40" OnPageIndexChanging="GvControlProdUltimo_PageIndexChanging" >
        <RowStyle HorizontalAlign="Left"  />
        <Columns>
            <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="NumLact" HeaderText="Núm. Lact" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Dias" HeaderText="Dias" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Leche" HeaderText="Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
            <asp:BoundField DataField="ProdLeche" HeaderText="Prod. Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
            <asp:BoundField DataField="FechaServicio" HeaderText="Fecha Servicio" dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="NumServicio" HeaderText="Núm. Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Diag" HeaderText="Diag." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="FechaProbParto" HeaderText="Prob. Parto" dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
        </Columns>
        <FooterStyle />
        <PagerStyle CssClass="pagination pull-right" />
        <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
        <SelectedRowStyle />
        <HeaderStyle />
        <EditRowStyle />
        <AlternatingRowStyle />
        </asp:GridView>
        <h4><asp:Label ID="titCantAnimales" runat="server" Text="Cantidad de vacas en ordeñe: " Visible="False"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" ></asp:Label><br/></h4>
        </p>
    </div><br />

</asp:Content>
