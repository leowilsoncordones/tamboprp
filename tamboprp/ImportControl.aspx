<%@ Page Title="tamboprp | importar control" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImportControl.aspx.cs" Inherits="tamboprp.ImportControl" %>
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
    <script src="js/jquery-ui.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/excanvas.js"></script>
    <script src="js/bootstrap.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />  
    <div class="page-header">
        <h1><i class="menu-icon fa fa-upload"></i> Importar archivo de controles de producción </h1>
    </div>
    
    <div class="row">
        <h3></h3>
    </div>
    <div class="row">
       
            <!-- FORMULARIO -->
            <div id="formulario" class="form-horizontal">
                <!-- Cargar foto -->
                <div class="row"></div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"></label>
                    <div class="col-sm-4">
                            <asp:FileUpload runat="server" ID="fupTxt"/>
                    </div>

		         </div>
                <!-- Botones -->
                <div class="form-group">
                    <div class="col-md-offset-3 col-md-9">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info col-sm-4" Text="Importar datos" OnClick="btn_GuardarEvento" />
				    </div>
                </div>
            </div>
    </div>
    <asp:Panel runat="server" ID="panelExitosas">
        <div class="row">
        <div class="alert alert-info col-md-4">
            <i class="ace-icon glyphicon glyphicon-list"></i>
            <asp:Label runat="server" ID="lblTotales"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="alert alert-success col-md-4">
            <i class="ace-icon glyphicon glyphicon-ok"></i>
           <asp:Label runat="server" ID="lblExitosas"></asp:Label> 
        </div>       
    </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="panelFallidas">
    <div class="row">
        <div class="alert alert-danger col-md-4">
            <i class="ace-icon glyphicon glyphicon-remove"></i>
            <asp:Label runat="server" ID="lblFallidas"></asp:Label>
        </div>       
    </div>
    <div class="row"></div>
    <div class="row"></div>
    
            <div class="row">
            <h3>Controles que no pudieron ser procesados</h3>
            </div>
            <div class="row">
                <div class="col-md-9">					
                    <!-- GRIDVIEW -->
                    <asp:GridView ID="gvControles" runat="server" AutoGenerateColumns="False" GridLines="Both" HorizontalAlign="Left" 
                            CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                        <RowStyle HorizontalAlign="Left"  />
                        <Columns>
                            <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />  
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" dataformatstring="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Comentarios" HeaderText="Comentarios" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Grasa" HeaderText="Grasa" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Leche" HeaderText="Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <FooterStyle />
                        <PagerStyle HorizontalAlign="Left" />
                        <SelectedRowStyle />
                        <HeaderStyle />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                </div>
                <div class="col-md-3"></div>
            </div>
    </asp:Panel>

    <div class="row">
        <asp:Label runat="server" ID="lblStatus" CssClass="alert alert-danger col-md-6"></asp:Label>
    </div>
</asp:Content>
