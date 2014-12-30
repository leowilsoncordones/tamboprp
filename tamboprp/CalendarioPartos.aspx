<%@ Page Title="tamboprp | partos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalendarioPartos.aspx.cs" Inherits="tamboprp.CalendarioPartos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script src="js/otros/bs.pagination.js"></script>

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
        <h1><i class="menu-icon fa fa-calendar"></i> Calendario de partos</h1>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="input-group">
                <span class="input-group-btn">
                    <asp:Button ID="btnListar" runat="server" Text="Listar" onclick="btnListar_Click" CssClass="btn btn-white btn-default" />
                </span>
                <asp:DropDownList ID="ddlMeses" class="form-control" runat="server" ></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-8"></div>
    </div>
    <hr/>
    <div class="row">
        <div class="col-md-8">
            <asp:GridView ID="gvPartos" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" 
                    CssClass="table table-hover table-striped table-bordered table-condensed dataTable" PagerStyle-CssClass="bs-pagination text-center" 
                        AllowPaging="true" AllowSorting="true" PageSize="40" OnPageIndexChanging="GvPartos_PageIndexChanging" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Registro" HeaderText="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="FechaServicio" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Fecha de servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="RegistroPadre" HeaderText="Caravana de servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="CantServicios" HeaderText="Numero de servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="FechaParto" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Fecha de Parto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                </Columns>
                <FooterStyle />
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
                </asp:GridView>
            <asp:Label ID="titCantAnimales" runat="server" Text="Cantidad de animales en la lista: " Visible="False"></asp:Label><asp:Label ID="lblCantAnimales" runat="server" Visible="False"></asp:Label><br/>
    </div>
    <div class="col-md-4"></div>
    </div>
</asp:Content>
