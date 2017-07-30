<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login_repor.aspx.cs" Inherits="GAPA.Login_repor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <article>
        <a style="color: #00649d">Digite o seu email</a>
        <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox><br />
        <asp:Button CssClass="btn btn-default" ID="btRepor" runat="server" Text="Repor Password" OnClick="btRepor_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lbErro" runat="server" /><br />
    </article>
</asp:Content>
