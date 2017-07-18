<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Perfis_Add.aspx.cs" Inherits="GAPA.Perfis_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <article>
        <div class="form-group">
            <label for="tbNome">Nome</label>
            <asp:TextBox ID="tbNome" runat="server" CssClass="form-control"></asp:TextBox>
        </div>


        <div class="text" style="margin-left: 1px;">
            <div class="left">
                <div class="col-md-14 col-md-pull-13 pull-left" style="margin-left: 0px;">
                    <div class="panel panel-default" style="margin-top: 0px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">Permissões</h3>
                        </div>
                        <div class="panel-body">
                            <asp:CheckBoxList ID="cbl_permissoes" runat="server" />
                        </div>
                    </div>
                </div>

            </div>
        </div>

          <div id="divUsers">
        
        <div class="container">
            <div class="row">
                <!--<div class="col-md-12">-->
                
                  <!--  <div class="row" style="margin-left: 0px;">-->
                       
                        <div class="text" style="margin-left: 1px;">
                            <div class="center">
                                <div class="col-md-14 col-md-pull-13 pull-left" style="margin-left: 0px;">
                                    <div class="panel panel-default" style="margin-top: 0px;">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Utilizadores</h3>
                                        </div>
                                        <div class="panel-body">
                                            <asp:CheckBoxList ID="cbl_users" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" style='height: 100%; width: 100%; object-fit: contain'/>
                                         
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    <!--</div>-->

               <!-- </div>-->
            </div>
        </div>
    </div>

        <br />
        &nbsp;
    <asp:Button ID="btAdicionar" runat="server" Text="Adicionar" CssClass="btn btn-default" OnClick="btAdicionar_Click" />
        <br />
        <br />
        <br />
        <asp:Label ID="lbErro" runat="server" />
        <asp:HiddenField ID="hfId_perfil" runat="server" />
    </article>
</asp:Content>
