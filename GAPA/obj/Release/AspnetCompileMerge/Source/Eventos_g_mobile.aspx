<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Eventos_g_mobile.aspx.cs" Inherits="GAPA.Eventos_g_mobile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="pull-left col-md-4 col-sm-4 input-group">
        <asp:TextBox runat="server" ID="tbPesquisa" CssClass="form-control" />
        <span class="input-group-btn">
            <asp:Button ID="btPesquisa" runat="server" Text="Pesquisar" CssClass="btn btn-default" OnClick="btPesquisa_Click"/>
        </span>
    </div>


    <br />
    <br />
    <br />

       <div class="container">
        <div class="row">

          
                <div class="col-md-13">
                    <div class="panel panel-default" style="margin-top: 0px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">Portões</h3>
                        </div>
    <asp:gridview id="gvEventos"   BackColor="White" runat="server" CssClass="table table-responsive"  ForeColor="#00649D" ShowFooter="False" ShowHeaderWhenEmpty="False" PageIndex="0" PageSize="20"  BorderStyle="None" GridLines="None"  AllowSorting="true" ShowHeader="True" style='height: 100%; width: 100%; object-fit: contain' AllowPaging="True" OnPageIndexChanging="gvEventos_PageIndexChanging" >
        <PagerStyle HorizontalAlign="Left" VerticalAlign="Bottom" Wrap="False" />
                        </asp:GridView>
            </div>
                </div>
            </div>
        
    </div>
</asp:Content>
