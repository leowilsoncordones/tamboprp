<%@ Page Title="tamboprp | error 401" Language="C#" MasterPageFile="~/SitePublic.Master" AutoEventWireup="true" CodeBehind="Error401.aspx.cs" Inherits="tamboprp.Error401" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="error-container">
		<div class="well">
			<h1 class="grey lighter smaller"><span class="blue bigger-125"><i class="ace-icon fa fa-sitemap"></i> 401</span> unauthorized</h1>
            <hr>
			<h3 class="lighter smaller">No está autorizado para acceder a la página solicitada</h3>
            <div>
			    <div class="space"></div>
			    <h4 class="smaller">Intenta lo siguiente:</h4>

			    <ul class="list-unstyled spaced inline bigger-110 margin-15">
			        <li> <i class="ace-icon fa fa-hand-o-right blue"></i> Intenta acceder luego del login</li>
				    <li> <i class="ace-icon fa fa-hand-o-right blue"></i> Chequea la dirección de la página</li>
                    <li><i class="ace-icon fa fa-hand-o-right blue"></i> Lee las FAQs </li>
                    <li><i class="ace-icon fa fa-hand-o-right blue"></i> Cuéntanos que pasó, puedes crear un caso de soporte</li>
			    </ul>
			</div>
            <hr>
			<div class="space"></div>
            <div class="center">
				<a href="javascript:history.back()" class="btn btn-grey"><i class="ace-icon fa fa-arrow-left"></i> Ir atrás</a>
				<a href="../Default.aspx" class="btn btn-primary"><i class="ace-icon fa fa-home"></i> Home</a>
			</div>
		</div>
	</div>
</asp:Content>
