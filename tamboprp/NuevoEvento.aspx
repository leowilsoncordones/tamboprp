<%@ Page Title="tamboprp | evento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoEvento.aspx.cs" Inherits="tamboprp.NuevoEvento" %>
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
    
    <link href="css/datepicker.css" rel="stylesheet" />
    
    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/excanvas.js"></script>
    <script src="js/bootstrap.js"></script>

    <script src="js/date-time/bootstrap-datepicker.js"></script>

    <link href="css/jquery-ui.custom.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {
            $('#datepicker').datepicker({
                autoclose: true,
                todayHighlight: true
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-edit"></i> Ingreso de un nuevo evento </h1>
    </div>

    <div class="row">
        <div class="col-sm-12">
            
            <!-- Panel para ingreso de celo sin servicio -->
                <!-- Registro -->
                <div id="formulario" class="form-group form-group-lg">
		            <label class="col-sm-3 control-label no-padding-right"> Registro </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fRegistro" placeholder="Registro" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="space-4"></div>

                <!-- Fecha -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Fecha </label>
					<div class="col-sm-2">
						<div class="input-group date">
						    <input type="text" data-date-format="dd/mm/YYYY" id="datepicker" class="form-control col-xs-10 col-sm-5" runat="server"/>
							<span class="input-group-addon"><i class="ace-icon fa fa-calendar"></i></span>
						</div>
					</div>
                </div>

                <!-- Comentario -->
                <div class="form-group">
			        <label class="col-sm-3 control-label no-padding-right"> Comentario </label>
			        <div class="col-sm-5">
			            <textarea class="form-control" id="fComentario" rows="3" runat="server"></textarea>
			        </div>
		        </div>
                <div class="space-4"></div>

                <!-- Submit Boton -->
                <div class="form-group">
                <div class="col-md-offset-3 col-md-9">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" Text="Guardar" OnClick="btn_GuardarEvento" />
				</div>
                </div><br/>
            
            <asp:Label ID="lblVer" runat="server" Text="Label"></asp:Label>
            

        </div>
    </div>
    

</asp:Content>
