<%@ Page Title="tamboprp | sanidad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sanidad.aspx.cs" Inherits="tamboprp.Sanidad" %>
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
    <div class="page-header">
        <h1><i class="menu-icon fa fa-stethoscope"></i> Sanidad</h1>
    </div>
    
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="AnalisisMuertes.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-thumbs-o-down bigger-200"></i><br/>
                    Análisis de Muertes
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Enfermedades.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-stethoscope bigger-200"></i><br/>
                    Lista de enfermedades
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row">
       <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="Cmt.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-history bigger-200"></i><br/>
                    C.M.T. histórico
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 align-center lighter">
                
            </div>
        </div>
       <div class="col-md-1"></div>
    </div>
    
    
    <asp:Panel ID="pnlLinks" Visible="false" runat="server">
    <ul>
        <li><i class="menu-icon fa fa-stethoscope blue"></i><asp:HyperLink ID="hypEnfermedades" NavigateUrl="Enfermedades.aspx" runat="server"> Lista de enfermedades</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-thumbs-o-down blue"></i><asp:HyperLink ID="hypAnalisisMuertes" NavigateUrl="AnalisisMuertes.aspx" runat="server"> Análisis de Muertes</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-history blue"></i><asp:HyperLink ID="hypCmt" NavigateUrl="Cmt.aspx" runat="server">  C.M.T. histórico</asp:HyperLink></li>
    </ul>
    </asp:Panel>

</asp:Content>
