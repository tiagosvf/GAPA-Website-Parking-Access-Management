<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Perfil_AlterPass_mob.aspx.cs" Inherits="GAPA.Perfil_AlterPass_mob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <article>
        <div id="divPC" runat="server">
            <label style="color: #00649d; font-size: 15px;">Digite a nova Password</label>
            <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox><br />
            <label style="color: #00649d; font-size: 15px;">Confirme a nova Password</label>
            <asp:TextBox ID="tbConfirmar" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox><br />
            <asp:Button CssClass="btn btn-default" ID="btRepor" runat="server" Text="Alterar Password" OnClick="btRepor_Click" /><br />
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lbErro" runat="server" /><br />
        </div>
    </article>
</asp:Content>
