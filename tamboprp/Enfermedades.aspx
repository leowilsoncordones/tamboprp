<%@ Page Title="tamboprp | enfermedades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Enfermedades.aspx.cs" Inherits="tamboprp.Enfermedades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
    <script src="Scripts/dataTables.bootstrap.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <br/>
    
    <div class="row">
        <div class="col-md-4 wrapper">
            
            <div>
                <h3><asp:Label ID="lblTitulo" runat="server" Text="Lista de enfermedades"></asp:Label></h3>
                <p>        
                <asp:GridView ID="gvEnfermedades" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" 
                    AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvEnfermedades_PageIndexChanging" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Numero" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Nombre_enfermedad" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                </Columns>
                <FooterStyle />
                <PagerStyle CssClass="pagination pull-right" />
                <PagerSettings mode="Numeric" position="Bottom" pagebuttoncount="5" />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
                </asp:GridView>
                <h4><asp:Label ID="titCantEnf" runat="server" Text="Cantidad de enfermedades: " Visible="False"></asp:Label><asp:Label ID="lblCantEnf" runat="server" ></asp:Label><h4><br/>
                </p>
            </div>

        </div>
        <div class="col-md-8"></div>
    </div>

    <br />

</asp:Content>
