<%@ Page Title="tamboprp | error 404" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error404.aspx.cs" Inherits="tamboprp.Error404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-times"></i> Error 404 (not found)</h1>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <p class="bigger-110">
                La página que busca no existe, o ha ocurrido un error en este momento.<br/>
                Vuelva atrás o a la página <a href="Default.aspx">HOME</a> por favor.
            </p>
        </div>
        <div class="col-md-2"></div>
    </div>

</asp:Content>
