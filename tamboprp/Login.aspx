<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tamboprp.Login" %>
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
    <div class="container">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-md-offset-4">
                <h1 class="text-center">
                    <span class="ace-icon fa fa-check-square-o" aria-hidden="true"></span>
                    <span> tambo</span><strong class="text-primary">prp</strong>
                </h1>
                    <form class="form-group-lg">
                    <div class="form-group"><input type="text" class="form-control input-lg" placeholder="Usuario" required autofocus></div>
                    <div class="form-group"><input type="password" class="form-control input-lg" placeholder="Contraseña" required></div>
                    <div class="form-group"><button class="btn btn-lg btn-primary btn-block btn-round" type="submit">Ingresar</button></div>
                    <a href="#" class="pull-right">Me olvide la contraseña! </a><span class="clearfix"></span>
                    </form>
            </div>
        </div>
    </div>
    
</asp:Content>
