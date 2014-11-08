<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Personal.aspx.cs" Inherits="tamboprp.Personal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <div>
        <h3>Empleados</h3>
        <asp:GridView ID="gvEmpleados" runat="server" AutoGenerateColumns="False" GridLines="None" 
            HorizontalAlign="Left" Width="50%" CssClass="table table-hover table-striped table-bordered table-condensed dataTable" Caption="Lista de empleados" CaptionAlign="Left" >
            <RowStyle HorizontalAlign="Left"  />
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
                <asp:BoundField DataField="Iniciales" HeaderText="Iniciales" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />               
                <asp:BoundField DataField="Activo" HeaderText="Activo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <asp:ButtonField CommandName="editRecord" ControlStyle-CssClass="btn btn-default btn-sm" ButtonType="Button" Text="Editar" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <FooterStyle />
            <PagerStyle HorizontalAlign="Left" />
            <SelectedRowStyle />
            <HeaderStyle />
            <EditRowStyle />
            <AlternatingRowStyle />
        </asp:GridView>    
    </div><br/>

</asp:Content>
