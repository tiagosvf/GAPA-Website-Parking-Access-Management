<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Perfil_mob.aspx.cs" Inherits="GAPA.Perfil_mob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <article>
        <div id="divContainer" runat="server">
            <label style="color: #00649d; font-size: 15px;">Username: </label>
            <asp:Label CssClass="form-group" runat="server" ID="lbUsername"></asp:Label><br />

            <label style="color: #00649d; font-size: 15px;">Email: </label>
            <asp:Label CssClass="form-group" runat="server" ID="lbEmail"></asp:Label><br />

            <label style="color: #00649d; font-size: 15px;">Nome: </label>
            <asp:Label CssClass="form-group" runat="server" ID="lbNome"></asp:Label><br />

            <label style="color: #00649d; font-size: 15px;">Perfil: </label>
            <asp:Label CssClass="form-group" runat="server" ID="lbPerfil"></asp:Label><br />
            <br />
            <asp:Button CssClass="btn btn-default" runat="server" ID="btEditar" Text="Editar Perfil" OnClick="btEditar_Click" />
       
            
            <asp:Button CssClass="btn btn-default pull-right" runat="server" Text="Aplicação Móvel" ID="btnApk" OnClick="btnApk_Click" />

        </div>
    </article>
</asp:Content>
