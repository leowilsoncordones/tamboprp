<%@ Page Title="tamboprp | login" Language="C#" MasterPageFile="~/SitePublic.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tamboprp.Login" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-md-offset-4">
                <h1 class="text-center">
                    <span class="ace-icon fa fa-check-square-o" aria-hidden="true"></span>
                    <span> tambo</span><strong class="text-primary">prp</strong>
                </h1>
                <div class="form-group">
                    <div class="input-group">
						<span class="input-group-addon"><i class="ace-icon fa fa-user fa-lg"></i></span>
                        <input type="text" class="form-control input-lg" placeholder="Usuario" id="fUsuario" runat="server" />
					</div>
                </div>
                <div class="form-group">
                    <div class="input-group">
						<span class="input-group-addon"><i class="ace-icon fa fa-lock fa-lg"></i></span>
                        <input type="password" class="form-control input-lg" placeholder="Contraseña" id="fContrasena" runat="server" />
					</div>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-lg btn-primary btn-block btn-round" Text="Ingresar" OnClick="btnLogin_Click" />
                </div>
                <a href="#meOlvide" id="id-btn-MeOlvide" role="button" data-toggle="modal" class="pull-right">Me olvide la contraseña! </a><span class="clearfix"></span>
                <div class="form-group">
                    <asp:Label ID="lblResLogin" CssClass="bigger-110" runat="server" ></asp:Label>
                </div>
            </div>
        </div>
    </div>
    
    <!-- MODIFICAR DATOS MODAL -->
    <div id="meOlvide" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><i class="ace-icon fa fa-pencil"></i> Notificar olvido de contraseña <asp:Label ID="lblRegistroModalModificar" CssClass="text-info" runat="server"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <span id="bodyModifDataModal" class="text-warning center">
                        <div class="center-block">
                            Ingrese sus datos y el administrador del sitio se comunicará con usted a la brevedad.
                        </div>
                        <div class="hr hr-16 hr-dotted"></div>
                        <!-- FORMULARIO -->
                        <div id="formulario" class="form-horizontal">
                        <!-- Usuario -->
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Usuario </label>
			                <div class="col-sm-5">
			                    <input type="text" runat="server" id="fUsuario2" class="form-control col-xs-10 col-sm-5" />
			                </div>
		                </div>
                        <!-- Nombre -->
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Nombre </label>
			                <div class="col-sm-5">
			                    <input type="text" runat="server" id="fNombre" class="form-control col-xs-10 col-sm-5" />
			                </div>
		                </div>
                        <!-- Identificacion -->
                        <div class="form-group">
		                    <label class="col-sm-4 control-label no-padding-right"> Apellido </label>
			                <div class="col-sm-5">
			                    <input type="text" runat="server" id="fApellido" class="form-control col-xs-10 col-sm-5" />
			                </div>
		                </div>
                        <!-- Email -->
                        <div class="form-group">
                            <label class="col-sm-4 control-label no-padding-right"> Email </label>
                            <div class="col-sm-5">
			                    <input type="email" runat="server" id="fEmail" placeholder="Ej. usuario@dominio.com" class="form-control col-xs-10 col-sm-5" />
			                </div>
                        </div>
                        </div>

                    </span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-sm btn-info" Text="Enviar" OnClick="btn_MeOlvidePassword" />
                </div>
            </div>
        </div>
    </div>
    <!-- FINAL MODAL -->
    
</asp:Content>
