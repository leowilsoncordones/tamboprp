<%@ Page Title="tamboPRP" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tamboprp._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <br/>
    <div class="row">
        <div class="col-md-4 wrapper">
            <form class="form-signin">
                <h2 class="form-signin-heading">Ingrese sus credenciales</h2>
                <input type="text" class="form-control" name="username" placeholder="Usuario" required="" autofocus="" />
                <br />
                <input type="password" class="form-control" name="password" placeholder="Password" required="" />
                <br />
                <button class="btn btn-lg btn-primary" type="submit">Login</button>
            </form>
        </div>
        <div class="col-md-8"></div>
    </div>
    <br/>
</asp:Content>