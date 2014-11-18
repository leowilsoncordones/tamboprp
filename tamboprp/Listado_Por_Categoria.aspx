<%@ Page Title="Listados de animales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Listado_Por_Categoria.aspx.cs" Inherits="tamboprp.ListadoPorCategoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <br/>
    <h2>Listados de animales</h2>

    <div class="row">
        <div class="col-md-4 wrapper">
            <div class="input-group">
                <span class="input-group-btn">
                    <asp:Button ID="btnListar" runat="server" Text="Listar" onclick="btnListar_Click" CssClass="btn btn-primary" />
                </span>
                <asp:DropDownList ID="ddlCategorias" cssClass="form-control" runat="server" ></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-8"></div>
    </div>
    
    <div>
        <h3><asp:Label ID="lblCateg" runat="server" ></asp:Label></h3>
        <p>        
        <asp:GridView ID="gvAnimales" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
            CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
        <RowStyle HorizontalAlign="Left"  />
        <Columns>
            <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Identificacion" HeaderText="Identificacion" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Reg_trazab" HeaderText="Trazabilidad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Sexo" HeaderText="Sexo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Gen" HeaderText="Gen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Fecha_Nacim" HeaderText="Fecha Nac." dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
            <asp:BoundField DataField="Origen" HeaderText="Origen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Reg_padre" HeaderText="Reg. Padre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Reg_madre" HeaderText="Reg. Madre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
        </Columns>
        <FooterStyle />
        <PagerStyle HorizontalAlign="Left" />
        <SelectedRowStyle />
        <HeaderStyle />
        <EditRowStyle />
        <AlternatingRowStyle />
        </asp:GridView>
        <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad de animales en la categoría: " Visible="False"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" ></asp:Label><br/>
        </p>
    </div><br />
</asp:Content>
