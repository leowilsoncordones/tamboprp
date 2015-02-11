<%@ Page Title="tamboprp | análisis" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Analisis.aspx.cs" Inherits="tamboprp.Analisis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-eye"></i> Análisis</h1>
    </div>
    
    <div class="row">
       <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="AnalisisProduccion.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-cogs bigger-200"></i><br/>
                    Análisis Productivo
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="AnalisisMuertes.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-thumbs-o-down bigger-200"></i><br/>
                    Análisis de Muertes
	            </a>
            </div>
        </div>
       <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="AnalisisInseminadores.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-hand-o-right bigger-200"></i><br/>
                    Inseminadores
	            </a>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="AnalisisVentas.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-money bigger-200"></i><br/>
                    Analisis de Ventas
	            </a>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    

</asp:Content>
