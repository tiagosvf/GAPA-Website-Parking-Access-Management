<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GAPA.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <article>
        <div id="div_login" runat="server" class="login-page">
            <div class="form">
                Username<asp:TextBox ID="tbUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox><br />
                Password<asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password" /><br />
                <asp:Button CssClass="btn btn-default" ID="Bt_login" runat="server" Text="LogIn" OnClick="Button1_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <a href="Login_repor.aspx" class="alert-link">Esqueceu-se da sua password?</a><br />
                <br />
                <br />
                <asp:Label ID="lbErro" runat="server" /><br />

            </div>
        </div>
    </article>
</asp:Content>
