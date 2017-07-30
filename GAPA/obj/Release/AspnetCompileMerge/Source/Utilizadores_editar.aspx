<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Utilizadores_editar.aspx.cs" Inherits="GAPA.Utilizadores_editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <article>
        <div class="form-group">
            <label for="tbUsername">Username</label>
            <asp:TextBox ID="tbUsername" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="tbEmail">Email</label>
            <asp:TextBox ID="tbEmail" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="tbNome">Nome</label>
            <asp:TextBox ID="tbNome" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="ddl_Tipo">Tipo</label>
            <asp:DropDownList ID="ddl_Tipo" CssClass="dropdown" runat="server">
                <asp:ListItem Value="0">User</asp:ListItem>
                <asp:ListItem Value="1">Administrador</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="ccb_ativo">Ativo</label>
            <asp:CheckBox ID="cb_ativo" runat="server" />
        </div>
        <div class="form-group">
            <label for="ddl_perfil">Perfil</label>
            <asp:DropDownList ID="ddl_perfil" CssClass="dropdown" runat="server"></asp:DropDownList>
        </div>
        <asp:Button CssClass="btn btn-default" ID="btEditar" runat="server" Text="Guardar" OnClick="btEditar_Click" />
        <asp:Button CssClass="btn btn-default" ID="btCancelar" runat="server" Text="Cancelar" OnClick="btCancelar_Click" />
        <asp:Button CssClass="btn btn-danger" ID="btReset" runat="server" Text="Reset Password" OnClick="btReset_Click" />
        <asp:Label ID="lbErro" runat="server" Text=""></asp:Label>
    </article>
</asp:Content>
