<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Perfis_G.aspx.cs" Inherits="GAPA.Permissoes_G" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div id="divBotao" runat="server">
    <!--    <asp:Button CssClass="btn btn-info" runat="server" Text="Adicionar" ID="btnAdd" OnClick="btnAdd_Click" />-->

    </div>
        <script src="js/jquery-1.8.2.min.js"></script>
    <script src="Scripts/checkBoxGrp.js"></script>


   


    <div id="divGV" runat="server">
        <br />

        <div class="container">
            <div class="row">
                <!--<div class="col-md-12">-->
                
                  <!--  <div class="row" style="margin-left: 0px;">-->
                        <div class="left">
                            <div class="col-md-8 col-md-pull-13 pull-left">
                                <div class="panel panel-default" style="margin-top: 0px;">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Perfis <a href="/Perfis_Add.aspx" class="pull-right" ><span class="glyphicon glyphicon-plus"></span></a></h3>
                                    </div>
                                    <!--<div class="panel-body">-->
                                        <asp:GridView ID="gvPerfis" runat="server" CssClass="table table-responsive" BorderStyle="None" GridLines="None" ShowHeader="False" style='height: 100%; width: 100%; object-fit: contain' AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="Perfis2">
                                    <Columns>
                                                 <asp:ButtonField CommandName="Permissoes" Text="Ver permissões" ControlStyle-CssClass="btn btn-default" />
                                                <asp:TemplateField ShowHeader="False">

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_delete" runat="server" CausesValidation="False" CommandName="Delete" CssClass="btn btn-danger" Text="Eliminar" OnClientClick="return confirm('Tem a certeza que deseja eliminar este perfil? \nAVISO: Não é possível eliminar perfis que estejam definidos a algum utilizador.');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                                <asp:BoundField DataField="perfil" HeaderText="perfil" SortExpression="perfil" />
                                               
                                            </Columns>
          
                                   </asp:GridView>
                                    <asp:SqlDataSource ID="Perfis2" runat="server" ConnectionString="<%$ ConnectionStrings:sql %>" DeleteCommand="DELETE FROM [Perfis] WHERE [id] = @id" InsertCommand="INSERT INTO [Perfis] ([perfil]) VALUES (@perfil)" SelectCommand="SELECT [id], [perfil] FROM [Perfis]" UpdateCommand="UPDATE [Perfis] SET [perfil] = @perfil WHERE [id] = @id">
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="perfil" Type="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="perfil" Type="String" />
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="Perfis" runat="server" ConnectionString="<%$ ConnectionStrings:sql %>" DeleteCommand="DELETE FROM [Perfis] WHERE [id] = @id" InsertCommand="INSERT INTO [Perfis] ([perfil]) VALUES (@perfil)" SelectCommand="SELECT [id], [perfil] FROM [Perfis]" UpdateCommand="UPDATE [Perfis] SET [perfil] = @perfil WHERE [id] = @id">
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="perfil" Type="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="perfil" Type="String" />
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                    <!--</div>-->
                                    <asp:Label ID="lbErro" runat="server" /><br />
                                </div>
                            </div>
                        </div>
                        <div class="text" style="margin-left: 1px;">
                            <div class="right">
                                <div class="col-md-4 col-md-pull-0 pull-right" style="margin-left: 0px;">
                                    <div class="panel panel-default" style="margin-top: 0px;">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Permissões</h3>
                                        </div>
                                        <div class="panel-body">
                                            <asp:CheckBoxList ID="cbl_permissoes" runat="server"  OnSelectedIndexChanged="cbl_permissoes_SelectedIndexChanged" style='height: 100%; width: 100%; object-fit: contain'/>
                                            <asp:Button CssClass="btn btn-default pull-right" runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click" />
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

    <div id="divUsers">
        
        <div class="container">
            <div class="row">
                <!--<div class="col-md-12">-->
                
                  <!--  <div class="row" style="margin-left: 0px;">-->
                       
                        <div class="text" style="margin-left: 1px;">
                            <div class="center">
                                <div class="col-md-13 col-md-pull-13 pull-left" style="margin-left: 0px;">
                                    <div class="panel panel-default" style="margin-top: 0px;">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Utilizadores</h3>
                                        </div>
                                        <div class="panel-body">
                                            <asp:CheckBoxList ID="cbl_users" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" OnSelectedIndexChanged="cblUsers_SelectedIndexChanged" style='height: 100%; width: 100%; object-fit: contain'/>
                                          
                                            <asp:Button CssClass="btn btn-default pull-right" runat="server" Text="Guardar" ID="btnGuardarUsers" OnClick="btnGuardarUsers_Click1" />
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
    <script type="text/javascript">
        function foo() {
            HideProductGroupCheckBox("cblDemo", "0,3,6", "Bold");
        }

    </script>
 
                                       
</asp:Content>
