<%@ Page Title="tamboprp | contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="tamboprp.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-envelope" ></i> Contacto</h1>
    </div>
    <div class="row">
        <div class="col-md-5">

            <section class="contact">
                <header>
                    <h3>Teléfonos:</h3>
                </header>
                <p>
                    <span class="label">Principal:</span>
                    <span>(+598) </span>
                </p>
                <p>
                    <span class="label">Después de hora:</span>
                    <span>(+598) </span>
                </p>
            </section>

            <section class="contact">
                <header>
                    <h3>Email:</h3>
                </header>
                <p>
                    <span class="label">Soporte:</span>
                    <span><a href="mailto:soporte@tamboprp.uy">soporte@tamboprp.uy</a></span>
                </p>
                <p>
                    <span class="label">Marketing:</span>
                    <span><a href="mailto:marketing@tamboprp.uy">marketing@tamboprp.uy</a></span>
                </p>
                <p>
                    <span class="label">General:</span>
                    <span><a href="mailto:general@tamboprp.uy">general@tamboprp.uy</a></span>
                </p>
            </section>
            
            <section class="contact">
                <header>
                    <h3>Web:</h3>
                </header>
                <p>
                    <span class="label">Sitio:</span>
                    <span><a href="http://www.tamboprp.uy">www.tamboprp.uy</a></span>
                </p>
                <p>
                    <span class="label">Blog:</span>
                    <span><a href="http://blog.tamboprp.uy">blog.tamboprp.uy</a></span>
                </p>
            </section>

            <section class="contact">
                <header>
                    <h3>Dirección:</h3>
                </header>
                <p>
                    Calle Rivera, CP 11300<br />
                    Montevideo, UY 
                </p>
            </section>
            
    </div>

        <div class="col-md-7">
            <h1 class="text-center">
                <span class="ace-icon fa fa-check-square-o" aria-hidden="true"></span>
                <span> tambo</span><strong class="text-primary">prp</strong>                
            </h1>
            <div class="center">
                <img src="img_tamboprp/corporativo/tamboprp1.png" />
            </div>
        </div>
    </div>

</asp:Content>