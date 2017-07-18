<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Portoes_Add.aspx.cs" Inherits="GAPA.Portoes_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <article>
    <div class="form-group">
        <label for="tbNome">Nome</label>
        <asp:TextBox ID="tbNome" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="tbIP">IP</label>
        <asp:TextBox ID="tbIP" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
            <div class="form-group">
        <label for="tbCamera">Camera</label>
        <asp:TextBox ID="tbCamera" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="cbCaptura">Captura</label>
        &nbsp;
        <asp:CheckBox ID="cbCaptura" runat="server" Text=""  />
    </div>

    <div class="form-group">
        <label for="cbAtivo">Ativo</label>
      &nbsp;
        <asp:CheckBox ID="cbAtivo" runat="server" Text=""  />
    </div>




    <br />
    <asp:Button ID="btAdicionar" runat="server" Text="Adicionar" CssClass="btn btn-default" OnClick="btAdicionar_Click" />
    <br />
    <br />
    <br />
    <asp:Label ID="lbErro" runat="server" />

    </article>

</asp:Content>
