<%@ Page Title="tamboprp | análisis de bajas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisisVentas.aspx.cs" Inherits="tamboprp.AnalisisVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-money"></i> Análisis de ventas</h1>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />  
    <div class="row">
        <div class="col-md-6">
            <div class="well">
				<h4 class="blue smaller">Ventas <asp:Label ID="lblVentas" runat="server" ></asp:Label></h4>
                <ul class="list-unstyled spaced2">
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titCantVentas" runat="server" Text="Cantidad total de ventas: " ></asp:Label>
                    <strong><asp:Label ID="lblCantVentas" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titAFrigorifico" runat="server" Text="Ventas a Frigorífico: " ></asp:Label>
                    <strong><asp:Label ID="lblAFrigorifico" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titRecienNac" runat="server" Text="Ventas recién nacidos: " ></asp:Label>
                    <strong><asp:Label ID="lblRecienNac" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                <li><h5><i class="ace-icon fa fa-caret-right blue"></i>
                    <asp:Label ID="titPorVieja" runat="server" Text="Ventas por edad avanzada: " ></asp:Label>
                    <strong><asp:Label ID="lblPorVieja" CssClass="text-success" runat="server" ></asp:Label></strong></h5></li>
                </ul>
			</div>
        </div>
        
        <div class="col-md-4">
            <div class="row">
                    <div class="input-group">
                        <span class="input-group-btn">
                            <asp:Button ID="btnListar" runat="server" Text="Mostrar" onclick="btnListar_Click" CssClass="btn btn-white btn-default" />
                        </span>
                        <asp:DropDownList ID="ddlFechasGraf" cssClass="form-control" runat="server"  AutoPostBack="False"></asp:DropDownList>
                    </div>
                </div>
                <hr/>
                <div class="row">
                    
                    <asp:Panel ID="pnlFechasGraf" runat="server" class="input-group">
                        <h5 class="header smaller lighter blue">Entre dos fechas</h5> 
                        <div class="input-group">                 
                        <span class="input-group-btn">                   
                        <input type="button" onclick="cargarDatos(document.getElementById('fechasVentas').value)" value="Mostrar" class="btn btn-white btn-default"/>
                            
                        </span>
                        <input type="text" id="fechasVentas" name="fechasVentas" placeholder="Fechas" class="form-control"/>  
                            </div>                			                         
                    </asp:Panel>
                    
                </div>
        </div>
        <div class="col-md-2"></div>
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

        $("#fechasVentas").daterangepicker({
            locale: {
                applyLabel: 'Confirma',
                cancelLabel: 'Cancela',
                fromLabel: 'Desde',
                toLabel: 'Hasta',
            },
            format: 'DD/MM/YYYY'
        });

        function cargarDatos(data) {

            var res = data.split(" - ");
            var fecha1 = formatoFecha(res[0]);
            var fecha2 = formatoFecha(res[1]);
            PageMethods.GetCantVentasPor2fechas(fecha1, fecha2, function(response) { document.getElementById('<%=lblCantVentas.ClientID%>').innerHTML = response; });
            PageMethods.GetCantVentasAFrigPor2fechas(fecha1, fecha2, function(response) { document.getElementById('<%=lblAFrigorifico.ClientID%>').innerHTML = response; });
            PageMethods.GetCantVentasRecienNacidosPor2fechas(fecha1, fecha2, function(response) { document.getElementById('<%=lblRecienNac.ClientID%>').innerHTML = response; });
            PageMethods.GetCantVentasViejasPor2fechas(fecha1, fecha2, function(response) { document.getElementById('<%=lblPorVieja.ClientID%>').innerHTML = response; });
            
        };

        function formatoFecha(fecha) {
            var res = fecha.split("/");
            var salida = "";
            salida = res[2] + "-" + res[1] + "-" + res[0];
            return salida;
        }

    </script>
    

</asp:Content>
