<%@ Page Title="tamboprp | genealogía" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Genealogia.aspx.cs" Inherits="tamboprp.Genealogia" %>
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
    
    <link href="css/colorbox.css" rel="stylesheet" />

    <script src="js/jquery.colorbox.js"></script>

    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/excanvas.js"></script>

    <script type="text/javascript">
        var colorbox_params = {
            rel: 'colorbox',
            reposition: true,
            scalePhotos: true,
            scrolling: false,
            previous: '<i class="ace-icon fa fa-arrow-left"></i>',
            next: '<i class="ace-icon fa fa-arrow-right"></i>',
            close: '&times;',
            current: '{current} of {total}',
            maxWidth: '100%',
            maxHeight: '100%',
            onComplete: function () {
                $.colorbox.resize();
            }
        }

        $('[data-rel="colorbox"]').colorbox(colorbox_params);
        $('#cboxLoadingGraphic').append("<i class='ace-icon fa fa-spinner orange'></i>");
    </script>    


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="page-header">
        <h1><i class="menu-icon fa fa-puzzle-piece"></i> Genealogía</h1>
    </div>
        <!-- Busqueda -->
        <div class="row">
            <div class="col-md-4">        
                <div class="input-group input-group-lg">
                    <span class="input-group-btn">
                        <asp:Button ID="btnBuscarAnimal" runat="server" onclick="btnBuscarAnimal_Click" Text="Buscar" CssClass="btn btn-white btn-default" />
                    </span>
                    <input type="text" class="form-control" runat="server" id="regBuscar" placeholder="Registro"/>
                </div>
            </div>
            <div class="col-md-4 btn-group btn-group-lg">
                <asp:DropDownList ID="ddlSimil" runat="server" style="height:46px;" CssClass="btn btn-white btn-default btn-lg col-sm-9 dropdown-toggle" OnSelectedIndexChanged="ddlSimilares_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
            </div>
            <div class="col-md-4">
                
            </div>
        </div>
        <br/>

<asp:Panel ID="verResultado" runat="server" Visible="False">
    
    <!-- row registro - arbol genealogico -->
    <div class="row clearfix">
		<div class="col-md-12">
		    <asp:Panel ID="PanelAnimal" runat="server" CssClass="panel panel-default" Height="620px">
                <div class="panel-heading">
                    <span class="widget-title h4"><strong><asp:Label ID="Animal" runat="server"></asp:Label></strong>&nbsp;
                    <asp:Label ID="lblNom" CssClass="btn-lg" runat="server" Visible="false"></asp:Label></span>
                    <span class="pull-right small">REGISTRO</span>
                </div>
                <div class="panel-body">
                    <div class="row">
		            <div class="col-md-7">
		                <asp:Label ID="lblSexo" runat="server" CssClass="badge badge-pink" Text="H" Visible="True" ></asp:Label>&nbsp;
                        <asp:Label ID="titFNac" runat="server" CssClass="label label-default arrowed-right" Text="Fecha Nac." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblFNac" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titGen" runat="server" CssClass="label label-default arrowed-right" Text="Gen." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblGen" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titId" runat="server" CssClass="label label-default arrowed-right" Text="Identif." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titTraz" runat="server" CssClass="label label-default arrowed-right" Text="MGAP" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblTraz" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <div class="space-4"></div>
                        <asp:Label ID="lblEst" runat="server" Text="" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblCat" runat="server" Text="" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <div class="hr hr-16 hr-dotted"></div>
                        <p>
                            <asp:Label ID="titPL" runat="server" CssClass="label label-default arrowed-right" Text="Prod. Leche" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblPL" runat="server" Text="" Visible="False"></asp:Label>&nbsp;        
                            <asp:GridView ID="gvLactancias" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                            <RowStyle HorizontalAlign="Left" CssClass="small" />
                            <Columns>
                                <asp:BoundField DataField="Numero" HeaderText="Núm." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Dias" HeaderText="Días" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="ProdLeche" HeaderText="Prod. Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="PorcentajeGrasa" HeaderText="%Grasa" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Leche305" HeaderText="Leche 305" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Leche365" HeaderText="Leche 365" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                            <FooterStyle />
                            <PagerSettings />
                            <SelectedRowStyle />
                            <HeaderStyle CssClass="small" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            </asp:GridView>
                        </p>
                        <div class="hr hr-16 hr-dotted"></div>
                        <p>
                            <asp:Label ID="lblPremios" runat="server" CssClass="label label-default" Text="Premios en concursos" Visible="False"></asp:Label><br/>
                            <asp:GridView ID="gvConcursos" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                            <RowStyle HorizontalAlign="Left" CssClass="small" />
                            <Columns>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" dataformatstring="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Lugar" HeaderText="Lugar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="NombreExpo" HeaderText="Expo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="CategConcurso" HeaderText="Categ." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="ElPremio" HeaderText="Premio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                            <FooterStyle />
                            <PagerSettings />
                            <SelectedRowStyle />
                            <HeaderStyle CssClass="small" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            </asp:GridView>
                        </p>
                    </div>
                    <div class="col-md-5">
                        <!-- GALLERY thumbnails -->
                        <asp:Label ID="lblFotos" runat="server" CssClass="label label-default" Text="Fotos" Visible="True"></asp:Label><br/>
                        <div>
                            <ul class="ace-thumbnails clearfix" id="ULFotos" runat="server">
                            
                            </ul>
                        </div>
                    </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
     
    <h3 class="row header smaller lighter pink2">
		<span class="col-sm-12"><i class="ace-icon fa fa-puzzle-piece"></i> Línea materna</span>
	</h3>

    <!-- row linea materna -->
    <div class="row clearfix">
        <div class="col-md-6">
            <asp:Panel ID="PanelMadre" runat="server" CssClass="panel panel-default" Height="620px">
                <div class="panel-heading">
                    <span class="widget-title h4"><strong><asp:Label ID="Madre" runat="server"></asp:Label></strong>&nbsp;
                    <asp:Label ID="lblNomMadre" runat="server" Visible="false"></asp:Label></span>
                    <span class="pull-right small pink2">MADRE</span>
                </div>
                <div class="panel-body">
                    <asp:Label ID="lblSexoMadre" runat="server" CssClass="badge badge-pink" Text="H" ></asp:Label>&nbsp;
                    <asp:Label ID="titFNacMadre" runat="server" CssClass="label label-default arrowed-right" Text="Fecha Nac." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblFNacMadre" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="titIdMadre" runat="server" CssClass="label label-default arrowed-right" Text="Id." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblIdMadre" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="titTrazMadre" runat="server" CssClass="label label-default arrowed-right" Text="MGAP" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblTrazMadre" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                    <div class="space-4"></div>
                    <asp:Label ID="lblEstMadre" Visible="False" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCatMadre" Visible="False" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                    <div class="hr hr-16 hr-dotted"></div>
                    <p>
                        <asp:Label ID="titPLMadre" runat="server" CssClass="label label-default arrowed-right" Text="Prod. Leche" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblPLMadre" runat="server" Text="" Visible="False"></asp:Label>&nbsp;        
                        <asp:GridView ID="gvLactMadre" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                            CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                        <RowStyle HorizontalAlign="Left" CssClass="small" />
                        <Columns>
                            <asp:BoundField DataField="Numero" HeaderText="Núm." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Dias" HeaderText="Días" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="ProdLeche" HeaderText="Prod. Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="PorcentajeGrasa" HeaderText="%Grasa" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Leche305" HeaderText="Leche 305" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Leche365" HeaderText="Leche 365" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <FooterStyle />
                        <PagerSettings />
                        <SelectedRowStyle />
                        <HeaderStyle CssClass="small" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        </asp:GridView>
                    </p>
                    <div class="hr hr-16 hr-dotted"></div>
                    <!-- GALLERY thumbnails -->
                    <asp:Label ID="lblFotosMadre" runat="server" CssClass="label label-default" Text="Fotos" Visible="True"></asp:Label><br/>
                    <div>
                        <ul class="ace-thumbnails clearfix" id="ULFotosMadre" runat="server">
                            
                        </ul>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="col-md-6">
            <div class="row">
                <asp:Panel ID="PanelAbuelaM" runat="server" CssClass="panel panel-default" Height="400px">
                    <div class="panel-heading">
                        <span class="widget-title h4"><strong><asp:Label ID="AbuelaM" runat="server"></asp:Label></strong>&nbsp;
                        <asp:Label ID="lblNomAbuelaM" CssClass="btn-lg" runat="server" Visible="false"></asp:Label></span>
                        <span class="pull-right small pink2">ABUELA MATERNA</span>
                    </div>
                    <div class="panel-body">
                        <asp:Label ID="lblSexoAbuelaM" runat="server" CssClass="badge badge-pink" Text="H" ></asp:Label>&nbsp;
                        <asp:Label ID="titFNacAbuelaM" runat="server" CssClass="label label-default arrowed-right" Text="Fecha Nac." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblFNacAbuelaM" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titIdAbuelaM" runat="server" CssClass="label label-default arrowed-right" Text="Id." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblIdAbuelaM" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titTrazAbuelaM" runat="server" CssClass="label label-default arrowed-right" Text="MGAP" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblTrazAbuelaM" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <div class="space-4"></div>
                        <asp:Label ID="lblEstAbuelaM" Visible="False" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblCatAbuelaM" Visible="False" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                        <div class="hr hr-16 hr-dotted"></div>
                        <p>
                            <asp:Label ID="titPLAbuelaM" runat="server" CssClass="label label-default arrowed-right" Text="Prod. Leche" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblPLAbuelaM" runat="server" Text="" Visible="False"></asp:Label>&nbsp;        
                            <asp:GridView ID="gvLactAbuelaM" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                            <RowStyle HorizontalAlign="Left" CssClass="small" />
                            <Columns>
                                <asp:BoundField DataField="Numero" HeaderText="Núm." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Dias" HeaderText="Días" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="ProdLeche" HeaderText="Prod. Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="PorcentajeGrasa" HeaderText="%Grasa" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Leche305" HeaderText="Leche 305" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Leche365" HeaderText="Leche 365" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                            <FooterStyle />
                            <PagerSettings />
                            <SelectedRowStyle />
                            <HeaderStyle CssClass="small" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            </asp:GridView>
                        </p>
                    </div>
                </asp:Panel>
            </div>
            <div class="row">
                <asp:Panel ID="PanelAbueloM" runat="server" CssClass="panel panel-default" Height="200px">
                    <div class="panel-heading">
                        <span class="widget-title h4"><strong><asp:Label ID="AbueloM" runat="server"></asp:Label></strong>&nbsp;
                    <asp:Label ID="lblNomAbueloM" runat="server" Visible="false"></asp:Label></span>
                        <span class="pull-right small pink2">ABUELO MATERNO</span>
                    </div>
                    <div class="panel-body">
                        <asp:Label ID="lblSexoAbueloM" runat="server" CssClass="badge badge-primary" Text="M" ></asp:Label>&nbsp;
                        <asp:Label ID="titFNacAbueloM" runat="server" CssClass="label label-default arrowed-right" Text="Fecha Nac." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblFNacAbueloM" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titIdAbueloM" runat="server" CssClass="label label-default arrowed-right" Text="Id." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblIdAbueloM" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titTrazAbueloM" runat="server" CssClass="label label-default arrowed-right" Text="MGAP" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblTrazAbueloM" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <div class="space-4"></div>
                        <asp:Label ID="lblEstAbueloM" Visible="False" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblCatAbueloM" Visible="False" runat="server" Text=""></asp:Label>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    
    
    <h3 class="row header smaller lighter blue">
		<span class="col-sm-12"><i class="ace-icon fa fa-puzzle-piece"></i> Línea paterna</span>
	</h3>
    
    <!-- row linea paterna -->
    <div class="row clearfix">
        <div class="col-md-6">
            <asp:Panel ID="PanelPadre" runat="server" CssClass="panel panel-default" Height="620px">
                <div class="panel-heading">
                    <span class="widget-title h4"><strong><asp:Label ID="Padre" runat="server"></asp:Label></strong>&nbsp;
                    <asp:Label ID="lblNomPadre" runat="server" Visible="false"></asp:Label></span>
                    <span class="pull-right small blue">PADRE</span>
                </div>
                <div class="panel-body">
                    <asp:Label ID="lblSexoPadre" runat="server" CssClass="badge badge-primary" Text="M" ></asp:Label>&nbsp;
                    <asp:Label ID="titFNacPadre" runat="server" CssClass="label label-default arrowed-right" Text="Fecha Nac." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblFNacPadre" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="titIdPadre" runat="server" CssClass="label label-default arrowed-right" Text="Id." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblIdPadre" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="titTrazPadre" runat="server" CssClass="label label-default arrowed-right" Text="MGAP" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblTrazPadre" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                    <div class="space-4"></div>
                    <asp:Label ID="lblEstPadre" Visible="False" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCatPadre" Visible="False" runat="server" Text=""></asp:Label>
                    <div class="hr hr-16 hr-dotted"></div>
                    <!-- GALLERY thumbnails -->
                    <asp:Label ID="lblFotosPadre" runat="server" CssClass="label label-default" Text="Fotos" Visible="True"></asp:Label><br/>
                    <div>
                        <ul class="ace-thumbnails clearfix" id="ULFotosPadre" runat="server">
                            
                        </ul>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="col-md-6">
            <div class="row">
                <asp:Panel ID="PanelAbuelaP" runat="server" CssClass="panel panel-default" Height="400px">
                    <div class="panel-heading">
                        <span class="widget-title h4"><strong><asp:Label ID="AbuelaP" runat="server"></asp:Label></strong>&nbsp;
                    <asp:Label ID="lblNomAbuelaP" runat="server" Visible="false"></asp:Label></span>
                        <span class="pull-right small blue">ABUELA PATERNA</span>
                    </div>
                    <div class="panel-body">
                        <asp:Label ID="lblSexoAbuelaP" runat="server" CssClass="badge badge-pink" Text="H" ></asp:Label>&nbsp;
                        <asp:Label ID="titFNacAbuelaP" runat="server" CssClass="label label-default arrowed-right" Text="Fecha Nac." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblFNacAbuelaP" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titIdAbuelaP" runat="server" CssClass="label label-default arrowed-right" Text="Id." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblIdAbuelaP" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titTrazAbuelaP" runat="server" CssClass="label label-default arrowed-right" Text="MGAP" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblTrazAbuelaP" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <div class="space-4"></div>
                        <asp:Label ID="lblEstAbuelaP" Visible="False" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblCatAbuelaP" Visible="False" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                        <div class="hr hr-16 hr-dotted"></div>
                        <p>
                            <asp:Label ID="titPLAbuelaP" runat="server" CssClass="label label-default arrowed-right" Text="Prod. Leche" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblPLAbuelaP" runat="server" Text="" Visible="False"></asp:Label>
                            <asp:GridView ID="gvLactAbuelaP" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                                CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                            <RowStyle HorizontalAlign="Left" CssClass="small" />
                            <Columns>
                                <asp:BoundField DataField="Numero" HeaderText="Núm." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Dias" HeaderText="Días" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="ProdLeche" HeaderText="Prod. Leche" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="PorcentajeGrasa" HeaderText="%Grasa" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Leche305" HeaderText="Leche 305" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Leche365" HeaderText="Leche 365" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                            <FooterStyle />
                            <PagerSettings />
                            <SelectedRowStyle />
                            <HeaderStyle CssClass="small" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            </asp:GridView>
                        </p>
                    </div>
                </asp:Panel>
            </div>
            <div class="row">
                <asp:Panel ID="PanelAbueloP" runat="server" CssClass="panel panel-default" Height="200px">
                    <div class="panel-heading">
                        <span class="widget-title h4"><strong><asp:Label ID="AbueloP" runat="server"></asp:Label></strong>&nbsp;
                        <asp:Label ID="lblNomAbueloP" runat="server" Visible="false"></asp:Label></span>
                        <span class="pull-right small blue">ABUELO PATERNO</span>
                    </div>
                    <div class="panel-body">
                        <asp:Label ID="lblSexoAbueloP" runat="server" CssClass="badge badge-primary" Text="M" ></asp:Label>&nbsp;
                        <asp:Label ID="titFNacAbueloP" runat="server" CssClass="label label-default arrowed-right" Text="Fecha Nac." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblFNacAbueloP" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titIdAbueloP" runat="server" CssClass="label label-default arrowed-right" Text="Id." Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblIdAbueloP" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="titTrazAbueloP" runat="server" CssClass="label label-default arrowed-right" Text="MGAP" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblTrazAbueloP" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        <div class="space-4"></div>
                        <asp:Label ID="lblEstAbueloP" Visible="False" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblCatAbueloP" Visible="False" runat="server" Text=""></asp:Label>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Panel>

</asp:Content>
