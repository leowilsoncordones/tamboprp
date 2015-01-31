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
    
    <script src="js/otros/bs.pagination.js"></script>
    
    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/bootstrap.js"></script>
    
    
    <script type="text/javascript">
        function pageLoad() {
            $('.bs-pagination td table').each(function (index, obj) {
                convertToPagination(obj);
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-stethoscope"></i> Lista de enfermedades</h1>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="pull-right">
                <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm" Text=" Excel" onclick="excelExport_Click"><span><i class="fa fa-file-excel-o bigger-110 green"></i></span> Excel</asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" PDF" onclick="pdfExport_Click"><span><i class="fa fa-file-pdf-o bigger-110 red"></i></span> PDF</asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" Print" onclick="print_Click"><span><i class="fa fa-print bigger-110 grey"></i></span> Print</asp:LinkButton>
            </div>
        </div>
        <div class="col-md-4"></div>
        <div class="col-md-4"></div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div>
                <p>        
                <asp:GridView ID="gvEnfermedades" runat="server" AutoGenerateColumns="False" GridLines="Both" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center"  
                    AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvEnfermedades_PageIndexChanging" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Nombre_enfermedad" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                </Columns>
                <FooterStyle />
                <PagerSettings mode="Numeric" pagebuttoncount="5" />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
                </asp:GridView>
                </p>
                <asp:Label ID="titCantEnf" runat="server" Text="Cantidad de enfermedades: " Visible="False"></asp:Label><asp:Label ID="lblCantEnf" runat="server" ></asp:Label><br/>
            </div>
        </div>
        <div class="col-md-4"></div>
        
        <!-- RESUMEN EN COLUMNA DERECHA -->
        <div class="col-md-4">
            <div class="well">
                <h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Tabla de enfermedades</h4>
                <ul class="list-unstyled spaced2">
                    <li class="bigger-110">
                        <a href="#newModal" role="button" id="id-btn-save" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-pencil"></i> Agregar enfermedad</a>
                    </li>
                </ul>
                <hr/>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
			</div>
        </div>
   </div>
    
    
    <!-- NUEVA ENFERMEDAD MODAL -->
    
    <div id="newModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-save"></i> Ingreso enfermedad</h4>
                </div>
                <div class="modal-body">
                    <span id="bodySaveModal" class="text-warning center">
                        <!-- FORMULARIO -->
                        <div id="formulario" class="form-horizontal">
                            <!-- Nombre y Apellido -->
                            <div class="form-group">
		                        <label class="col-sm-3 control-label no-padding-right"> Nombre </label>
			                    <div class="col-sm-6">
                                    <asp:TextBox ID="newEnfermedad" CssClass="form-control col-xs-10 col-sm-5" runat="server"></asp:TextBox>
			                    </div>
		                    </div>
                        </div>
                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_GuardarEnfermedad" />
                </div>
            </div>
        </div>
    </div>

    <!-- FINAL MODAL -->

</asp:Content>
