<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="GAPA.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="divAtualizar" runat="server">
       <a href="/Dashboard.aspx" class="btn btn-info">Atualizar <span class="glyphicon glyphicon-refresh"></span></a>
    </div>

    <br />
    
    

    <div id="divCameras" runat="server">
    </div>
    <br />
    <div id="divEventos" runat="server">
         <div class="col-md-13 col-md-pull-13 pull-left">
                                <div class="panel panel-default" style="margin-top: 0px;">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Eventos</h3>
                                    </div>
                                    <!--<div class="panel-body">-->
                                       <asp:GridView ID="gvEventos" runat="server" CssClass="table table-responsive" BorderStyle="None" GridLines="None" ShowHeader="True" style='height: 100%; width: 100%; object-fit: contain'>
          
                                   </asp:GridView>
                                    <!--</div>-->
                                </div>
                            </div>
    </div>

    <asp:Label runat="server" ID="lbteste" CssClass="label label-danger"></asp:Label>
    <asp:HiddenField ID="hfID" runat="server" />
      <asp:HiddenField ID="hfChannel" runat="server" />

</asp:Content>
