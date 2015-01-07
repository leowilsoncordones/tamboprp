<%@ Page Title="tamboprp | personal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListPersonal.aspx.cs" Inherits="tamboprp.ListPersonal" %>
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
<asp:Content ID="ContentPersonal" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-male"></i> Personal</h1>
    </div>
    <div class="row">
        <div class="col-md-5">
            <!-- botones -->
            <div class="clearfix">
                <div class="pull-right tableTools-container">
                    <div class="btn-group btn-overlap">
                        <div class="ColVis btn-group" title="" data-original-title="Show/hide columns">
                            <button class="ColVis_Button ColVis_MasterButton btn btn-white btn-info btn-bold"><span><i class="fa fa-search"></i></span></button>
                        </div>
                        <a class="DTTT_button btn btn-white btn-primary btn-bold" id="ToolTables_dynamic-table_0" tabindex="0" aria-controls="dynamic-table"><span><i class="fa fa-copy bigger-110 pink"></i></span><div title="" style="position: absolute; left: 0px; top: 0px; width: 41px; height: 35px; z-index: 99;" data-original-title="Copy to clipboard"><embed id="ZeroClipboard_TableToolsMovie_1" src="../assets/js/dataTables/extensions/TableTools/swf/copy_csv_xls_pdf.swf" loop="false" menu="false" quality="best" bgcolor="#ffffff" width="41" height="35" name="ZeroClipboard_TableToolsMovie_1" align="middle" allowscriptaccess="always" allowfullscreen="false" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" flashvars="id=1&amp;width=41&amp;height=35" wmode="transparent"></div></a>
                        <a class="DTTT_button btn btn-white btn-primary  btn-bold" id="ToolTables_dynamic-table_1" tabindex="0" aria-controls="dynamic-table"><span><i class="fa fa-file-excel-o bigger-110 green"></i></span><div title="" style="position: absolute; left: 0px; top: 0px; width: 40px; height: 35px; z-index: 99;" data-original-title="Export to CSV"><embed id="ZeroClipboard_TableToolsMovie_2" src="../assets/js/dataTables/extensions/TableTools/swf/copy_csv_xls_pdf.swf" loop="false" menu="false" quality="best" bgcolor="#ffffff" width="40" height="35" name="ZeroClipboard_TableToolsMovie_2" align="middle" allowscriptaccess="always" allowfullscreen="false" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" flashvars="id=2&amp;width=40&amp;height=35" wmode="transparent"></div></a>
                        <a class="DTTT_button btn btn-white btn-primary  btn-bold" id="ToolTables_dynamic-table_2" tabindex="0" aria-controls="dynamic-table"><span><i class="fa fa-file-pdf-o bigger-110 red"></i></span><div title="" style="position: absolute; left: 0px; top: 0px; width: 39px; height: 35px; z-index: 99;" data-original-title="Export to PDF"><embed id="ZeroClipboard_TableToolsMovie_3" src="../assets/js/dataTables/extensions/TableTools/swf/copy_csv_xls_pdf.swf" loop="false" menu="false" quality="best" bgcolor="#ffffff" width="39" height="35" name="ZeroClipboard_TableToolsMovie_3" align="middle" allowscriptaccess="always" allowfullscreen="false" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" flashvars="id=3&amp;width=39&amp;height=35" wmode="transparent"></div></a>
                        <a class="DTTT_button btn btn-white btn-primary  btn-bold" id="ToolTables_dynamic-table_3" title="" tabindex="0" aria-controls="dynamic-table" data-original-title="Print view"><span><i class="fa fa-print bigger-110 grey"></i></span></a>
                    </div>
                </div>
            </div>
            <!-- grilla -->
        <asp:PlaceHolder ID="phPersonal" runat="server">
            <asp:GridView ID="gvEmpleados" runat="server" AutoGenerateColumns="False" GridLines="None" 
                HorizontalAlign="Left" CssClass="table table-hover table-striped table-bordered table-condensed dataTable" >
                <RowStyle HorizontalAlign="Left"  />
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
                    <asp:BoundField DataField="Iniciales" HeaderText="Iniciales" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />               
                    <asp:BoundField DataField="Activo" HeaderText="Activo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <FooterStyle />
                <PagerStyle HorizontalAlign="Left" />
                <SelectedRowStyle />
                <HeaderStyle />
                <EditRowStyle />
                <AlternatingRowStyle />
            </asp:GridView>    
        </asp:PlaceHolder><!-- Fin de tabla personal -->

            <!-- Botones para exportar en diversos formatos -->
            <div class="pull-right">
                <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="excelExport" Text=" Excel" onclick="excelExport_Click" />
                <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="pdfExport" Text=" PDF" onclick="pdfExport_Click" />
                <button type="button" runat="server" class="btn btn-default btn-sm" onclick="pdfExport_Click"><span class="fa fa-file-pdf-o" aria-hidden="true"></span> PDF</button>
            </div>
        </div>
        <div class="col-md-7"></div>
    </div>
</asp:Content>
