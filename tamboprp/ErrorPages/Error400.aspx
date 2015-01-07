<%@ Page Title="tamboprp | error 400" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error400.aspx.cs" Inherits="tamboprp.Error400" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-times"></i> Error 400 (bad request)</h1>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <p class="bigger-110">
                Ocurrió un error al tratar de acceder a la página solicitada.<br/>
                Vuelva atrás o a la página <a href="Default.aspx">HOME</a> por favor.
            </p>
        </div>
        <div class="col-md-2"></div>
    </div>

</asp:Content>
