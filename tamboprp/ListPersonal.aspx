<%@ Page Title="tamboprp | personal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListPersonal.aspx.cs" Inherits="tamboprp.ListPersonal" %>
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
    <script src="js/excanvas.js"></script>
</asp:Content>
<asp:Content ID="ContentPersonal" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-male"></i> Personal</h1>
    </div>
    <div class="row">
        <div class="col-md-5">
            <!-- botones -->
            <div class="clearfix">
                <div class="pull-right tableTools-container">
                    <div class="btn-group btn-overlap">               
                    <!-- Botones para exportar en diversos formatos -->
                        <div class="pull-right">
                            <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm" Text=" Excel" onclick="excelExport_Click"><span><i class="fa fa-file-excel-o bigger-110 green"></i></span> Excel</asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" PDF" onclick="pdfExport_Click"><span><i class="fa fa-file-pdf-o bigger-110 red"></i></span> PDF</asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" Print" onclick="print_Click"><span><i class="fa fa-print bigger-110 grey"></i></span> Print</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <!-- grilla -->
        <asp:PlaceHolder ID="phPersonal" runat="server">
            <asp:GridView ID="gvEmpleados" runat="server" AutoGenerateColumns="False" GridLines="Both" 
                HorizontalAlign="Left" CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre y Apellido" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Iniciales" HeaderText="Iniciales" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />               
                    <asp:BoundField DataField="EstaActivo" HeaderText="Activo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <FooterStyle />
                <PagerStyle HorizontalAlign="Left" />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
            </asp:GridView>    
        </asp:PlaceHolder><!-- Fin de tabla personal -->

            
        </div>

        <div class="col-md-3"></div>

        <!-- RESUMEN EN COLUMNA DERECHA -->
        <div class="col-md-4">
            <div class="well">
                <h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Gestión de empleados</h4>
                <ul class="list-unstyled spaced2">
                    <li class="bigger-110">
                        <a href="NuevoEmpleado.aspx" role="button" class="btn btn-white btn-default btn-sm"><i class="ace-icon fa fa-user"></i> Crear nuevo</a>
                    </li>
                    <li class="bigger-110">
                        <a href="#modifData" role="button" id="id-btn-ModifData" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-pencil"></i> Modificar</a>
                    </li>
                    <li class="bigger-110">
                        <asp:Button ID="btnVerActivos" runat="server" CssClass="btn btn-sm btn-white btn-default" Text="Ver Activos" OnClick="btn_VerActivos" />
                    </li>
                    <li class="bigger-110">
                        <asp:Button ID="btnVerTodos" runat="server" CssClass="btn btn-sm btn-white btn-default" Text="Ver Todos" OnClick="btn_VerTodos" />
                    </li>
                </ul>
                <hr/>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
			</div>
        </div>
        <!-- FIN RESUMEN EN COLUMNA DERECHA -->
        
    <!-- MODIFICAR DATOS MODAL -->
    <div id="modifData" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-pencil"></i> Modificar datos</h4>
                </div>
                <div class="modal-body">
                    <span id="bodyModifDataModal" class="text-warning center">
                        <!-- FORMULARIO -->
                        <div id="formulario" class="form-horizontal">
                        <!-- Empleado -->
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Empleado </label>
			                <div class="col-sm-5">
			                    <asp:DropDownList ID="ddlEmpleados" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="False" runat="server" ></asp:DropDownList>
			                </div>
		                </div>
                        <!-- Nombre y Apellido -->
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Nombre </label>
			                <div class="col-sm-5">
			                    <input type="text" runat="server" id="fNombre" class="form-control col-xs-10 col-sm-5" />
			                </div>
		                </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Apellido </label>
			                <div class="col-sm-5">
			                    <input type="text" runat="server" id="fApellido" class="form-control col-xs-10 col-sm-5" />
			                </div>
                            <div class="col-sm-12"></div>
		                </div>
                        <!-- Activo -->
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Activo </label>
					        <div class="col-sm-1">
                            <label>
					            <input id="checkActivo" name="switchEnable" class="ace ace-switch ace-switch-6" type="checkbox" checked runat="server"/>
						        <span class="lbl"></span>
					        </label>
                            </div>
                        </div>
                        </div>

                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnModificarDatos" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_ModificarDatos" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->
        


    </div>
</asp:Content>
