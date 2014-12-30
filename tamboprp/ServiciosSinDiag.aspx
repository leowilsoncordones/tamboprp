<%@ Page Title="tamboprp | servicios sin diagnóstico" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ServiciosSinDiag.aspx.cs" Inherits="tamboprp.ServiciosSinDiag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="page-header">
            <h1><i class="menu-icon fa fa-lightbulb-o"></i> Servicios sin diagnóstico de preñez <small><i class="ace-icon fa fa-angle-double-right"></i> Vacas con mas de 70 días de servicio y aún sin diagnóstico</small></h1>
        </div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="Print" />
        <asp:GridView ID="gvServicios" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" OnRowCreated="gvServicios_created">
            <RowStyle HorizontalAlign="Left"  />
            <Columns>
                <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Edad" HeaderText="Edad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
                <asp:BoundField DataField="FechaServicio" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Fecha de Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="RegistroPadre" HeaderText="Caravana de Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="CantServicios" HeaderText="Cantidad de Servicios" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="DiasServicio" HeaderText="Dias de Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Inseminador" HeaderText="Inseminador" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <FooterStyle />
            <PagerStyle HorizontalAlign="Left" />
            <SelectedRowStyle />
            <HeaderStyle />
            <EditRowStyle />
            <AlternatingRowStyle />
        </asp:GridView>
        <div>
            <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad de animales en la lista: " Visible="False"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" Visible="False"></asp:Label><br/>
        </div>
</asp:Content>
