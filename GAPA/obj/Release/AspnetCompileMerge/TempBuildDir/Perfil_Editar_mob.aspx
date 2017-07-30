<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Perfil_Editar_mob.aspx.cs" Inherits="GAPA.Perfil_Editar_mob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <article>
        <div id="divContainer" runat="server">
            <div class="form-group">
                <label for="tbUsername" style="color: #00649d; font-size: 15px">Username</label>
                <asp:TextBox ID="tbUsername" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tbEmail" style="color: #00649d; font-size: 15px">Email</label>
                <asp:TextBox ID="tbEmail" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tbNome" style="color: #00649d; font-size: 15px">Nome</label>
                <asp:TextBox ID="tbNome" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button CssClass="btn btn-default" ID="btEditar" runat="server" Text="Guardar" OnClick="btEditar_Click" />
            <asp:Button CssClass="btn btn-default" ID="btCancelar" runat="server" Text="Cancelar" OnClick="btCancelar_Click" /><br />
            <asp:Button CssClass="btn btn-danger" ID="btReset" runat="server" Text="Alterar Password" OnClick="btReset_Click" Style="margin-top:7px" /><br /><br />
            <asp:Label ID="lbErro" runat="server" Text=""></asp:Label>
        </div>
    </article>
</asp:Content>
