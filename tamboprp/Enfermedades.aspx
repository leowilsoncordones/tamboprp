<%@ Page Title="tamboprp | enfermedades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Enfermedades.aspx.cs" Inherits="tamboprp.Enfermedades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <link href="css/ace-fonts.css" rel="stylesheet" />
    <link href="css/chosen.css" rel="stylesheet" />
    <link href="css/ui.jqgrid.css" rel="stylesheet" />
    <link href="css/ace.css" rel="stylesheet" />
    <link href="css/ace-part2.css" rel="stylesheet" />
    <link href="css/ace-skins.css" rel="stylesheet" />
    <link href="css/ace-rtl.css" rel="stylesheet" />
    <link href="css/ace-ie.css" rel="stylesheet" />
    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/bootstrap.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-stethoscope"></i> Lista de enfermedades</h1>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div>
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
                </p>
                <asp:Label ID="titCantEnf" runat="server" Text="Cantidad de enfermedades: " Visible="False"></asp:Label><asp:Label ID="lblCantEnf" runat="server" ></asp:Label><br/>
            </div>
        </div>
        <div class="col-md-8"></div>
    </div>

</asp:Content>
