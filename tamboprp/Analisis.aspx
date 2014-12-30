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
                <a href="AnalisisReprod.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-flask bigger-200"></i><br/>
                    Análisis Reproductivo
	            </a>
            </div>
        </div>
       <div class="col-md-1"></div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10 container">
            <div class="col-md-5 jumbotron align-center lighter">
                <a href="AnalisisBajas.aspx" class="bigger-160">
		            <i class="ace-icon fa fa-money bigger-200"></i><br/>
                    Bajas y ventas
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
            <div class="col-md-5 align-center">
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
    
    
    <asp:Panel ID="pnlLinks" Visible="false" runat="server">
        <ul>
        <li><i class="menu-icon fa fa-cogs blue"></i><asp:HyperLink ID="hypProductivo" NavigateUrl="AnalisisProduccion.aspx" runat="server">  Análisis Productivo</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-flask blue"></i><asp:HyperLink ID="hypReproductivo" NavigateUrl="AnalisisReprod.aspx" runat="server"> Análisis Reproductivo</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-money blue"></i><asp:HyperLink ID="hypBajas" NavigateUrl="AnalisisBajas.aspx" runat="server">  Análisis de bajas y ventas</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-thumbs-o-down blue"></i><asp:HyperLink ID="hypMuertes" NavigateUrl="AnalisisMuertes.aspx" runat="server">  Análisis de Muertes</asp:HyperLink></li>
        <li><i class="menu-icon fa fa-hand-o-right blue"></i><asp:HyperLink ID="hypAnalisisInseminadores" NavigateUrl="AnalisisInseminadores.aspx" runat="server">  Análisis de inseminadores</asp:HyperLink></li>
        </ul>    
    </asp:Panel>
    
</asp:Content>
