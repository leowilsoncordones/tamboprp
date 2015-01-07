<%@ Page Title="tamboprp | nuevo animal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoAnimal.aspx.cs" Inherits="tamboprp.NuevoAnimal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-edit"></i> Ingreso de parto y nuevos animales</h1>
    </div>
    
    <div class="row">
        <div class="col-sm-12">
            <div id="formulario" class="form-horizontal">
                <div class="form-group">
                    <div class="col-sm-3 control-label no-padding-right">
                        <h4 class="widget-title lighter blue">PARTO</h4>
                    </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- GENERAL - Registro -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Registro madre </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fRegistro" placeholder="Registro" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- GENERAL - Fecha -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Fecha </label>
					<div class="col-sm-2">
						<div class="input-group date">
						    <input type="text" id="mydate" name="mydate" class="form-control col-xs-10 col-sm-5"/>
							<span class="input-group-addon"><i class="ace-icon fa fa-calendar"></i></span>
						</div>
					</div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- GENERAL - Comentarios -->
                <div class="form-group">
			        <label class="col-sm-3 control-label no-padding-right"> Comentarios </label>
			        <div class="col-sm-5">
			            <textarea class="form-control" id="fComentario" rows="3" runat="server"></textarea>
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- SERVICIO y REG PADRE -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Registro padre </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fRegPadre" readonly placeholder="Registro padre" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- GENERAL - Fecha -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Fecha servicio </label>
					<div class="col-sm-2">
						<div class="input-group date">
						    <input type="text" id="mydateServ" name="mydateServ" readonly class="form-control col-xs-10 col-sm-5"/>
							<span class="input-group-addon"><i class="ace-icon fa fa-calendar"></i></span>
						</div>
					</div>
                    <div class="col-sm-12"></div>
                </div>
                <hr/>
                <!-- + CRÍA -->
                <div class="form-group">
                    <div class="col-sm-3 control-label no-padding-right">
                        <h4 class="widget-title lighter blue">INGRESO DE CRÍAS</h4>
                    </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- HIJO - SEXO?, VIVO? -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Sexo </label>
                    <div class="col-sm-2">
					    <label>
					        <input id="checkSexo" name="switchSexo" class="ace ace-switch ace-switch-5" type="checkbox" checked runat="server"/>
						    <span class="lbl"></span>
					    </label>
				    </div>
                    <div class="col-sm-12"></div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Vivo </label>
                    <div class="col-sm-2">
					    <label>
					        <input id="checkMuerto" name="switchMuerto" class="ace ace-switch ace-switch-6" type="checkbox" checked runat="server"/>
						    <span class="lbl"></span>
					    </label>
				    </div>
                    <div class="col-sm-12"></div>
                </div>
                <asp:Panel ID="pnlServicio" runat="server">
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Registro cría </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fRegCria" placeholder="Registro cría" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                <!-- HIJO - Nombre, Origen, TRazab -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Nombre </label>
			        <div class="col-sm-4">
			            <input type="text" runat="server" id="fNombre" placeholder="" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Origen </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fOrigen" placeholder="PROPIETARIO" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Trazabilidad </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fTraz" placeholder="MGAP" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- HIJO - GEN, IDENTIFICACION -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Generación </label>
			        <div class="col-sm-1">
			            <input type="text" runat="server" id="fGen" readonly placeholder="" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Identificación </label>
			        <div class="col-sm-2">
			            <input type="text" runat="server" id="fIdentif" readonly placeholder="" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
                </div>
                </asp:Panel>
                
            </div>
         </div>
    </div>
</asp:Content>
