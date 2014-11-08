<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tamboprp._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrapper">
        <form class="form-signin">
            <h2 class="form-signin-heading">tamboprp</h2>
            <input type="text" class="form-control" name="username" placeholder="Usuario" required="" autofocus="" />
            <input type="password" class="form-control" name="password" placeholder="Password" required="" />
            <label class="checkbox">
                <input type="checkbox" value="recordarme" id="recordarme" name="Recordarme">
                Recordarme
            </label>
            <button class="btn btn-lg btn-primary btn-block" type="submit">Login</button>
        </form>
  </div>

</asp:Content>