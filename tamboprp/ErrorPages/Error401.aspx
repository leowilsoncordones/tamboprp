<%@ Page Title="tamboprp | error 401" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error401.aspx.cs" Inherits="tamboprp.Error401" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-times"></i> Error 401 (unauthorized)</h1>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <p class="bigger-110">
                No está autorizado para acceder a la página solicitada, intente acceder luego del login.<br/>
                Vuelva atrás o a la página <a href="Default.aspx">HOME</a> por favor.
            </p>
        </div>
        <div class="col-md-2"></div>
    </div>

</asp:Content>
