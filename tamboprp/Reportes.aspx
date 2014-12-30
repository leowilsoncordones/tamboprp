<%@ Page Title="tamboprp | reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="tamboprp.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/ace.css" rel="stylesheet" />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <link href="css/ace-fonts.css" rel="stylesheet" />
    <link href="css/chosen.css" rel="stylesheet" />
    <link href="css/ui.jqgrid.css" rel="stylesheet" />
    <link href="css/ace-part2.css" rel="stylesheet" />
    <link href="css/ace-skins.css" rel="stylesheet" />
    <link href="css/ace-rtl.css" rel="stylesheet" />
    <link href="css/ace-ie.css" rel="stylesheet" />
    
    <link href="css/datepicker.css" rel="stylesheet" />
    
    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script> 
    <script src="js/jquery1x.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/excanvas.js"></script>   
    <script src="js/jquery-ui.js"></script>
    <script src="js/jquery.js"></script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-bar-chart-o"></i> Reportes</h1>
    </div>
   
   <div class="row">
       <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="NuevoRemito.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-edit bigger-200"></i><br/>
                    Ingreso de nuevo remito
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Remitos.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-truck bigger-200"></i><br/>
                    Remitos a Planta
	            </a>
            </div>
        </div>
       <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="GraficasProd.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-bar-chart-o bigger-200"></i><br/>
                    Gráficas de producción
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="EmpresasRemisoras.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-building-o bigger-200"></i><br/>
                    Empresas Remisoras
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>

     
    <asp:Panel ID="pnlLinks" Visible="false" runat="server">
     <ul>
        <li><i class="menu-icon fa fa-bar-chart-o blue"></i><asp:HyperLink ID="hypGraficaProd" NavigateUrl="GraficasProd.aspx" runat="server"> Gráficas de producción</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-truck blue"></i><asp:HyperLink ID="hypRemitos" NavigateUrl="Remitos.aspx" runat="server"> Remitos a planta</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-edit blue"></i><asp:HyperLink ID="hypNuevoRemito" NavigateUrl="NuevoRemito.aspx" runat="server">  Ingreso de nuevo remito</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-building-o blue"></i><asp:HyperLink ID="hypEmpresasRemisoras" NavigateUrl="EmpresasRemisoras.aspx" runat="server"> Empresas Remisoras</asp:HyperLink></li>
    </ul>
        
        <div class="row">
        <div class="col-xs-6">
            <!-- GENERAL - Fecha -->
            <div class="form-group">
                <label class="col-sm-3 control-label no-padding-right"> Fecha </label>
	            <div class="col-sm-2">
		            <div class="input-group input-group-sm date">
			            <input type="text" name="mydate" id="mydate"/>
			            <span class="input-group-addon"><i class="ace-icon fa fa-calendar"></i></span>
		            </div>
	            </div>
             </div>
         </div>
         <div class="col-xs-6">   
            <div class="input-daterange input-group input-group-sm">
              <input type="text" name="start" />
              -
              <input type="text" name="end" />
            </div>
        </div>
    </div>
        

    <script src="js/date-time/bootstrap-datepicker.js"></script>
    <script type="text/javascript">

        $("#mydate").datepicker({
            autoclose: true,
            todayHighlight: true
        });

    </script>

    </asp:Panel>

    
    
</asp:Content>
