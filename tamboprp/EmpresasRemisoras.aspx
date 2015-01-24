<%@ Page Title="tamboprp | empresas remisoras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpresasRemisoras.aspx.cs" Inherits="tamboprp.EmpresasRemisoras" %>
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
        <h1><i class="menu-icon fa fa-building-o"></i> Empresas remisoras <small><i class="ace-icon fa fa-angle-double-right"></i> actual e históricas</small></h1>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div>
                <asp:GridView ID="gvRemisoras" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center"  
                    AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvRemisoras_PageIndexChanging" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Rut" HeaderText="RUT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="EsActual" HeaderText="Actual" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                </Columns>
                <FooterStyle />
                <PagerSettings mode="Numeric" pagebuttoncount="5" />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
                </asp:GridView>
            </div>
        </div>
        <!-- RESUMEN EN COLUMNA DERECHA -->
        <div class="col-md-4">
            <div class="well">
                <h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Resumen</h4>
                <ul class="list-unstyled spaced2">
                    <li class="bigger-110">
                        <a href="#newModal" role="button" id="id-btn-save" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-pencil"></i> Agregar empresa</a>
                    </li>
                    <li class="bigger-110">
                        <a href="#cambiarActual" role="button" id="id-btn-change" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-refresh"></i> Cambiar actual</a>
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
                    <h4><i class="ace-icon fa fa-save"></i> Ingreso de nueva empresa remisora</h4>
                </div>
                <div class="modal-body">
                    <span id="bodySaveModal" class="text-warning center">
                        <!-- FORMULARIO -->
                        <div id="formulario" class="form-horizontal">
                            <!-- Nueva empresa -->
                            <div class="form-group">
		                        <label class="col-sm-3 control-label no-padding-right"> Nombre </label>
			                    <div class="col-sm-6">
                                    <asp:TextBox ID="fNombre" CssClass="form-control col-xs-10 col-sm-5" runat="server"></asp:TextBox>
			                    </div>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-3 control-label no-padding-right"> Razón Social </label>
			                    <div class="col-sm-6">
                                    <asp:TextBox ID="fRazonSocial" CssClass="form-control col-xs-10 col-sm-5" runat="server"></asp:TextBox>
			                    </div>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-3 control-label no-padding-right"> RUT </label>
			                    <div class="col-sm-6">
                                    <asp:TextBox ID="fRut" CssClass="form-control col-xs-10 col-sm-5" runat="server"></asp:TextBox>
			                    </div>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-3 control-label no-padding-right"> Dirección </label>
			                    <div class="col-sm-6">
                                    <asp:TextBox ID="fDireccion" CssClass="form-control col-xs-10 col-sm-5" runat="server"></asp:TextBox>
			                    </div>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-3 control-label no-padding-right"> Teléfono </label>
			                    <div class="col-sm-6">
                                    <asp:TextBox ID="fTelefono" CssClass="form-control col-xs-10 col-sm-5" runat="server"></asp:TextBox>
			                    </div>
		                    </div>
                        </div>
                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_GuardarRemisora" />
                </div>
            </div>
        </div>
    </div>

    <!-- FINAL MODAL -->
    
    
    <!-- MODIFICAR DATOS MODAL -->
    <div id="cambiarActual" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-pencil"></i> Cambiar empresa remisora actual</h4>
                </div>
                <div class="modal-body">
                    <span id="bodyModifDataModal" class="text-warning center">
                        <!-- FORMULARIO -->
                        <div id="formulario2" class="form-horizontal">
                        <!-- Empleado -->
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Empresa </label>
			                <div class="col-sm-5">
			                    <asp:DropDownList ID="ddlEmpresas" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="False" runat="server" ></asp:DropDownList>
			                </div>
		                </div>
                        </div>
                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnModificarDatos" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_ModificarActiva" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->

</asp:Content>
