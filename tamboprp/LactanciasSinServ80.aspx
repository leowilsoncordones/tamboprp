<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LactanciasSinServ80.aspx.cs" Inherits="tamboprp.LactanciasSinServ80" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <br/>
    <div>
        <h3>Vacas en lactancia con mas de 80 dias sin servicio</h3>
               
        <asp:GridView ID="gvServicios" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
            <RowStyle HorizontalAlign="Left"  />
            <Columns>
                <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />  
                <asp:BoundField DataField="FechaServicio" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Fecha de Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="RegistroPadre" HeaderText="Caravana de Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Inseminador" HeaderText="Inseminador" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <FooterStyle />
            <PagerStyle HorizontalAlign="Left" />
            <SelectedRowStyle />
            <HeaderStyle />
            <EditRowStyle />
            <AlternatingRowStyle />
        </asp:GridView>
        <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad total de animales: " Visible="True"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" Visible="False"></asp:Label><br/>
    </div>

</asp:Content>
