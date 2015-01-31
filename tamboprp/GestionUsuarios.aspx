<%@ Page Title="tamboprp | usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs" Inherits="tamboprp.GestionUsuarios" %>
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
    
    <script src="js/js_tamboprp/PasswordStrongChecker.js"></script>
    
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
        <h1><i class="menu-icon fa fa-users"></i> Gestión de usuarios <small><i class="ace-icon fa fa-angle-double-right"></i> del sistema</small></h1>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div>
                <asp:GridView ID="gvUsuario" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center"  
                    AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvUsuarios_PageIndexChanging" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre y apellido" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Nickname" HeaderText="Usuario" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Email" HeaderText="E-mail" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Rol" HeaderText="Rol" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="HabilitadoText" HeaderText="Habilitado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
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
                <h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Gestión de usuarios</h4>
                <ul class="list-unstyled spaced2">
                    <li class="bigger-110">
                        <a href="NuevoUsuario.aspx" role="button" class="btn btn-white btn-default btn-sm"><i class="ace-icon fa fa-user"></i> Crear nuevo</a>
                    </li>
                    <li class="bigger-110">
                        <a href="#habilitar" role="button" id="id-btn-Habilitar" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-lock"></i> Habilitar / Deshabilitar</a>
                    </li>
                    <li class="bigger-110">
                        <a href="#modifData" role="button" id="id-btn-ModifData" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-pencil"></i> Modificar</a>
                    </li>
                    <li class="bigger-110">
                        <a href="#changePwd" role="button" id="id-btn-changePwd" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-refresh"></i> Resetear contraseña</a>
                    </li>
                    <li class="bigger-110">
                        <a href="#delete" role="button" id="id-btn-delete" class="btn btn-white btn-default btn-sm" data-toggle="modal"><i class="ace-icon fa fa-trash"></i> Eliminar</a>
                    </li>
                </ul>
                <hr/>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
			</div>
        </div>
    </div>

    <!-- NUEVA CAMBIO CONTRASEÑA MODAL -->
    <div id="changePwd" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-refresh"></i> Resetear contraseña</h4>
                </div>
                <div class="modal-body">
                    <span id="bodyChangePwdModal" class="text-warning center">
                        <!-- FORMULARIO -->
                        <div id="formularioPwd" class="form-horizontal">
                            <!-- Usuario -->
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right"> Usuario </label>
			                    <div class="col-sm-5">
			                        <asp:DropDownList ID="dllUsuariosChangePwd" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="False" runat="server" ></asp:DropDownList>
			                    </div>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right"> Contraseña </label>
                                <div class="col-sm-5">
			                        <input type="password" runat="server" id="password" class="form-control col-xs-10 col-sm-5" />
			                    </div>
                                <!-- Chequeo de fortaleza de password-->
                                <div id="messages" class="col-sm-4">
                                    <!-- string password checker -->
                                </div>
		                    </div>
		                </div>
                        <!-- Recomendación -->
		                <label class="col-sm-2 text-info"> Importante! </label>
                            <small>Por seguridad, utilice contraseñas de al menos 8 caracteres de largo.<br/> 
                            Combine el uso de mayúsculas, minúsculas, números y caracteres especiales.</small>
                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_ResetearPassword" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->
    
    <!-- MODIFICAR DATOS MODAL -->
    <div id="modifData" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-pencil"></i> Modificar usuario</h4>
                </div>
                <div class="modal-body">
                    <span id="bodyModifDataModal" class="text-warning center">
                        
                        <!-- FORMULARIO -->
                        <div id="formulario" class="form-horizontal">
                         <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Usuario </label>
			                <div class="col-sm-5">
			                    <asp:DropDownList ID="ddlUsuarioSelecc" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="False" runat="server" ></asp:DropDownList>
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
                            <!-- Email -->
                            <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Email </label>
                            <div class="col-sm-5">
			                    <input type="email" runat="server" id="fEmail" placeholder="Ej. usuario@dominio.com" class="form-control col-xs-10 col-sm-5" />
			                </div>
                        </div>
                        <!-- Rol de usuario -->
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Rol de usuario </label>
			                <div class="col-sm-5">
			                    <asp:DropDownList ID="ddlRolUsuario" CssClass="form-control col-xs-10 col-sm-5" runat="server" ></asp:DropDownList>
			                </div>
                            <div class="col-sm-12"></div>
		                </div>
                        </div>

                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_ModificarUsuario" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->
    
    <!-- ELIMINAR MODAL -->
    <div id="delete" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-trash"></i> Eliminar usuario</h4>
                </div>
                <div class="modal-body">
                    <span id="bodyDeleteModal" class="text-warning center">
                        <div id="formularioDelete" class="form-horizontal">
                            <div class="form-group">
		                        <label class="control-label no-padding-right"> Seguro que desea eliminar el usuario? </label>
		                    </div>
                            <div class="form-group">
		                        <label class="col-sm-4 control-label no-padding-right"> Usuario </label>
			                    <div class="col-sm-5">
			                        <asp:DropDownList ID="ddlUsuarioEliminar" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="False" runat="server" ></asp:DropDownList>
			                    </div>
		                    </div>
                        </div>
                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_EliminarUsuario" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->

    <!-- SELECCIONAR MODAL -->
    <div id="habilitar" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-lock"></i> Habilitar /deshabilitar usuario</h4>
                </div>
                <div class="modal-body">
                <span id="bodySelecModal" class="text-warning center">
                    <div id="formularioSelec" class="form-horizontal">
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Usuario </label>
			                <div class="col-sm-5">
			                    <asp:DropDownList ID="ddlUsuariosModificar" CssClass="form-control col-xs-10 col-sm-5" runat="server" OnSelectedIndexChanging="ddlUsuariosModificar_SelectedIndexChanging" ></asp:DropDownList>
			                </div>
		                </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Habilitado </label>
					        <div class="col-sm-1">
                            <label>
					            <input id="checkHabilitabo" name="switchEnable" class="ace ace-switch ace-switch-6" type="checkbox" checked runat="server"/>
						        <span class="lbl"></span>
					        </label>
                            </div>
                        </div>
                    </div>
                </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnHabilitar" runat="server" CssClass="btn btn-sm btn-info" Text="Ok" OnClick="btn_HabilitarUsuario" />
                </div>
            </div>
        </div>
    </div>
    <!-- SELECCIONAR MODAL -->

</asp:Content>
