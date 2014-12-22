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
    
</asp:Content>
