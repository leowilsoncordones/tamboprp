<%@ Page Title="tamboprp | auditoria" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Auditoria.aspx.cs" Inherits="tamboprp.Auditoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/ace.css" rel="stylesheet" />
    <link href="css/ace-part2.css" rel="stylesheet" />

    <link href="css/font-awesome.css" rel="stylesheet" />
    <link href="css/ace-fonts.css" rel="stylesheet" />
    <link href="css/chosen.css" rel="stylesheet" />
    <link href="css/ui.jqgrid.css" rel="stylesheet" />
    
    <link href="css/ace-skins.css" rel="stylesheet" />
    <link href="css/ace-rtl.css" rel="stylesheet" />
    <link href="css/ace-ie.css" rel="stylesheet" />
    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/excanvas.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-keyboard-o"></i> Auditoría <small><i class="ace-icon fa fa-angle-double-right"></i> registros de actividad de usuarios en el sistema</small></h1>
    </div>
    <div id="timeline-2" class="">
		<div class="row">
		    <div class="col-md-8">
			    <div class="col-xs-12 col-sm-10 col-sm-offset-1">
                    <!-- #section:pages/timeline.style2 -->
				    <div class="timeline-container timeline-style2" id="contenedor_dia" runat="server">
                    </div><!-- /.timeline-container -->
			    </div>
            </div>
            <!-- RESUMEN EN COLUMNA DERECHA -->
            <div class="col-md-4">
                <div class="well">
						<h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Resumen</h4>
                        <ul class="list-unstyled spaced2">
                            <li class="bigger-110">
                                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-sm btn-white btn-default" Text="Exportar Log" OnClick="btn_ExportarLog" />
                            </li>
                        </ul>
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
				</div>
             </div>
		</div>
	</div>
</asp:Content>
