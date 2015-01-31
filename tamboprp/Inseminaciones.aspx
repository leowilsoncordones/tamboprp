<%@ Page Title="tamboprp | inseminaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inseminaciones.aspx.cs" Inherits="tamboprp.Inseminaciones" %>
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
    
    <script src="js/otros/bs.pagination.js"></script>

    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/excanvas.js"></script>
    
    <script type="text/javascript">
        function pageLoad() {
            $('.bs-pagination td table').each(function (index, obj) {
                convertToPagination(obj);
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-hand-o-right"></i> Inseminaciones con diágnostico de preñez confirmado </h1>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="pull-right">
                <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm" Text=" Excel" onclick="excelExport_Click"><span><i class="fa fa-file-excel-o bigger-110 green"></i></span> Excel</asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" PDF" onclick="pdfExport_Click"><span><i class="fa fa-file-pdf-o bigger-110 red"></i></span> PDF</asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" Print" onclick="print_Click"><span><i class="fa fa-print bigger-110 grey"></i></span> Print</asp:LinkButton>
            </div>
        </div>
        <div class="col-md-8"></div>
    </div>
    <div class="space-6"></div>
    <div class="row">
        <div class="col-md-8">
        <asp:GridView ID="gvInseminaciones" runat="server" AutoGenerateColumns="False" GridLines="Both" HorizontalAlign="Left" 
                CssClass="table table-hover table-striped table-bordered table-condensed dataTable"  PagerStyle-CssClass="bs-pagination text-center"  
                    AllowPaging="true" AllowSorting="true" PageSize="20" OnPageIndexChanging="GvInseminaciones_PageIndexChanging" >
            <RowStyle HorizontalAlign="Left"  />
            <Columns>
                <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />  
                <asp:BoundField DataField="FechaServicio" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Fecha serv." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="RegistroPadre" HeaderText="Reg. Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="FechaDiagnostico" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Fecha diag." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Diagnostico" HeaderText="Diag." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Inseminador" HeaderText="Inseminador" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <FooterStyle />
            <PagerSettings mode="NumericFirstLast" pagebuttoncount="5" />
            <SelectedRowStyle />
            <HeaderStyle />
            <EditRowStyle />
            <AlternatingRowStyle />
        </asp:GridView>
        <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad total de animales: " Visible="True"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" Visible="False"></asp:Label><br/>
        </div>
        <!-- RESUMEN EN COLUMNA DERECHA -->
        <div class="col-md-4">
            <div class="row">
               <div class="well">
					<h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Resumen por inseminador</h4>
                    <ul class="list-unstyled spaced2" runat="server" id="listaInseminadores">
                    </ul>
                    <hr/>
                    <ul class="list-unstyled spaced2">
                    <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total preñadas &nbsp;&nbsp;</span>
                        <strong><asp:Label ID="lblTotal" runat="server" ></asp:Label></strong></li>                        
                    </ul>
			  </div> 
            </div>
            <!-- Comienzo DDL Datepicker -->
                        <div class="row">
                           <div class="input-group">
                            <span class="input-group-btn">
                                <asp:Button ID="btnListar" runat="server" Text="Mostrar" onclick="btnListar_Click" CssClass="btn btn-white btn-default" />
                            </span>
                            <asp:DropDownList ID="ddlFechas" cssClass="form-control" runat="server"  AutoPostBack="False"></asp:DropDownList>
                            </div> 
                        </div>
                        <div class="row"></div>
                        <div class="row">                   
                            <asp:Panel ID="panel01" runat="server" class="input-group">
                                <h5 class="header smaller lighter blue">Entre dos fechas</h5> 
                                <div class="input-group">                 
                                <span class="input-group-btn">                   
                                <asp:Button ID="Button1" runat="server" Text="Mostrar" onclick="btnListar_Inseminaciones" CssClass="btn btn-white btn-default" />
                                </span>
                                <input type="text" id="fechas" name="fechas" placeholder="Fechas" class="form-control"/>  
                                    </div>                			                         
                            </asp:Panel>                   
                        </div>
                <!-- Fin DDL Datepicker -->
        </div>
    </div>
    
    
    

    <script src="js/date-time/moment.js"></script>
    <script src="js/date-time/daterangepicker.js"></script>
    
    
    <link href="css/daterangepicker.css" rel="stylesheet" />
    
    <script type="text/javascript">

        $("#fechas").daterangepicker({
            locale: {
                applyLabel: 'Confirma',
                cancelLabel: 'Cancela',
                fromLabel: 'Desde',
                toLabel: 'Hasta',
            },
            format: 'DD/MM/YYYY'
        });

    </script>

</asp:Content>
