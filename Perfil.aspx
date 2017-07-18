<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="GAPA.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css_nav/css_perfil.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <article>
        <div id="divContainer" class="center" runat="server">
            <label class="label">Username: </label>
            <br />
            <asp:Label CssClass="form-group" runat="server" ID="lbUsername"></asp:Label><br />
            <label class="label">Email: </label>
            <br />
            <asp:Label CssClass="form-group" runat="server" ID="lbEmail"></asp:Label><br />
            <label class="label">Nome: </label>
            <br />
            <asp:Label CssClass="form-group" runat="server" ID="lbNome"></asp:Label><br />
            <label class="label">Perfil: </label>
            <br />
            <asp:Label CssClass="form-group" runat="server" ID="lbPerfil"></asp:Label><br />
            <br />
            <asp:Button CssClass="btn btn-default" runat="server" ID="btEditar" Text="Editar Perfil" OnClick="btEditar_Click" />
        </div>
    </article>
</asp:Content>
