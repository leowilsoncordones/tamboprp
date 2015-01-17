<%@ Page Title="tamboprp | análisis de inseminadores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisisInseminadores.aspx.cs" Inherits="tamboprp.AnalisisInseminadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-list-ol"></i> Ranking de inseminadores <small><i class="ace-icon fa fa-angle-double-right"></i> según efectividad en sus servicios</small></h1>
    </div>
    <div class="row">
        <div class="col-md-6">
            <asp:GridView ID="gvRanking" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
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
    </div>
</asp:Content>
