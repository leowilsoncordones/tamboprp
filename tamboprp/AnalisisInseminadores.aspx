<%@ Page Title="tamboprp | análisis de inseminadores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisisInseminadores.aspx.cs" Inherits="tamboprp.AnalisisInseminadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-list-ol"></i> Ranking de inseminadores <small><i class="ace-icon fa fa-angle-double-right"></i> según efectividad en sus servicios</small></h1>
    </div>
    
    <div class="row">
            <div class="col-md-6">
                <div class="pull-right">
                    <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm" Text=" Excel" onclick="excelExport_Click"><span><i class="fa fa-file-excel-o bigger-110 green"></i></span> Excel</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" PDF" onclick="pdfExport_Click"><span><i class="fa fa-file-pdf-o bigger-110 red"></i></span> PDF</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="btn btn-white btn-default btn-sm"  Text=" Print" onclick="print_Click"><span><i class="fa fa-print bigger-110 grey"></i></span> Print</asp:LinkButton>
                </div>
            </div>
            <div class="col-md-6"></div>
        </div>
    <div class="row">
        <div class="col-md-6">
            <asp:GridView ID="gvRanking" runat="server" AutoGenerateColumns="False" GridLines="Both" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="NombreCompleto" HeaderText="Inseminador" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />  
                    <asp:BoundField DataField="CantServicios" HeaderText="# Servicios" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />    
                    <asp:BoundField DataField="CantPrenadas" HeaderText="# Preñadas" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PorcEfectividad" HeaderText="% Efectividad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <FooterStyle />
                <PagerSettings />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
            </asp:GridView>
            <asp:Label ID="titCantInsem" runat="server" Text="Cantidad de inseminadores: " Visible="False"></asp:Label><asp:Label ID="lblCantInsem" runat="server" Visible="False"></asp:Label><br/>
        </div>
        
        <div class="col-md-2"></div>

        <!-- RESUMEN EN COLUMNA DERECHA -->
        <div class="col-md-4">
            <div class="row">
            <div class="well">
					<h4 class="header smaller lighter blue"><i class="menu-icon fa fa-paperclip"></i> Resumen</h4>
                    <ul class="list-unstyled spaced2">
                        <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total servicios &nbsp;&nbsp;</span>
                        <strong><asp:Label ID="lblTotalServicios" runat="server" ></asp:Label></strong></li> 
                    <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Total preñadas &nbsp;&nbsp;</span>
                        <strong><asp:Label ID="lblTotalPrenadas" runat="server" ></asp:Label></strong></li>    
                    <li class="bigger-110"><i class="ace-icon fa fa-caret-right blue"></i><span> Efectividad &nbsp;&nbsp;</span>
                        <strong><asp:Label ID="lblPorcEfectividad" CssClass="blue" runat="server" ></asp:Label></strong></li>                     
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
                                <asp:Button ID="Button1" runat="server" Text="Mostrar" onclick="btnListar_Inseminadores" CssClass="btn btn-white btn-default" />
                                </span>
                                <input type="text" id="fechas" name="fechas" placeholder="Fechas" class="form-control"/>  
                                    </div>                			                         
                            </asp:Panel>                   
                        </div>
                <!-- Fin DDL Datepicker -->
        </div>
    </div>
    
    <script src="js/ace-extra.js" ></script>

    <script src="js/date-time/moment.js"></script>
    <script src="js/date-time/daterangepicker.js"></script>
    
    
    <link href="css/daterangepicker.css" rel="stylesheet" />
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
