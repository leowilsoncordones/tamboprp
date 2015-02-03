<%@ Page Title="tamboprp | contacto" Language="C#" MasterPageFile="~/SitePublic.Master" AutoEventWireup="true" CodeBehind="ContactPublic.aspx.cs" Inherits="tamboprp.ContactPublic" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="page-header">
        <h1><i class="menu-icon fa fa-envelope" ></i> Contáctenos!</h1>
    </div>
    <div class="row">
        <div class="col-md-5">
            <section class="contact">
                <p>
                    Gracias por visitar nuestro sitio, estamos a sus órdenes para recibir consultas y brindarle información 
                    acerca de las potencialidades de nuestra aplicación.<br /><br />
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
                    <span>PROXIMAMENTE!</span>
                </p>
                <br/>
            </section>
            <section class="contact">
                <p>
                    Comience su experiencia <small><span class="ace-icon fa fa-check-square-o" aria-hidden="true"></span></small><strong> tambo<span class="text-primary">prp</span></strong> cuanto antes!
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