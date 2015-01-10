<%@ Page Title="tamboprp | error 500" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error500.aspx.cs" Inherits="tamboprp.Error500" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-times"></i> Error 500 (internal server error)</h1>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <p class="bigger-110">
                El servidor web ha encontrado un error en este momento, y no ha podido completar su solicitud.<br/>
                Vuelva atrás o a la página <a href="../Default.aspx">HOME</a> por favor.
            </p>
        </div>
        <div class="col-md-2"></div>
    </div>

</asp:Content>
