<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Utilizadores_G.aspx.cs" Inherits="GAPA.Utilizadores_G" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <article>
        <div class="pull-left col-md-4 col-sm-4 input-group">
            <asp:TextBox runat="server" ID="tbPesquisa" CssClass="form-control" />
            <span class="input-group-btn">
                <asp:Button ID="btPesquisa" runat="server" Text="Pesquisar" CssClass="btn btn-default" OnClick="btPesquisa_Click" />
            </span>
        </div>
        <br /><br /><br />
        <div class="container">
            <div class="row">
                <div class="col-md-13">
                    <div class="panel panel-default" style="margin-top: 0px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">Utilizadores</h3>
                        </div>
                        <asp:GridView ID="gvUsers"  BackColor="White" runat="server" CssClass="table table-responsive"  ForeColor="#00649D" PageSize="20"  BorderStyle="None" GridLines="None"  AllowSorting="True" style='height: 100%; width: 100%; object-fit: contain' AllowPaging="True" OnPageIndexChanging="gvUsers_PageIndexChanging" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="Utilizadores" >
                                             <Columns> 
                                                         <asp:ButtonField CommandName="Editar" Text="Editar"  />
                                                 <asp:TemplateField ShowHeader="False">
                                                     <ItemTemplate>
                                                         <asp:LinkButton  ID="btn_delete" runat="server" CausesValidation="False" CommandName="Delete" ForeColor="#cc0000" Text="Eliminar" OnClientClick="return confirm('Tem a certeza que deseja eliminar este utilizador?');"></asp:LinkButton>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                                 <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
                                                 <asp:BoundField DataField="emaiL" HeaderText="Email" SortExpression="emaiL" />
                                                 <asp:BoundField DataField="tipo" HeaderText="Tipo" SortExpression="tipo" />
                                                 <asp:CheckBoxField DataField="ativo" HeaderText="Ativo" SortExpression="ativo" />
                                                 <asp:BoundField DataField="username" HeaderText="Username" SortExpression="username" />
                                                 <asp:BoundField DataField="perfil" HeaderText="Perfil" SortExpression="perfil" />
                                             </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="Utilizadores" runat="server" ConnectionString="<%$ ConnectionStrings:sql %>" SelectCommand="SELECT Utilizadores.Id,Utilizadores.Nome,Utilizadores.Email,Utilizadores.Tipo,Utilizadores.Ativo,Utilizadores.Username,Perfis.Perfil FROM utilizadores INNER JOIN Perfis ON Utilizadores.perfil=Perfis.id" DeleteCommand="Delete From utilizadores where id=@id
">
                            <DeleteParameters>
                                <asp:Parameter Name="id" />
                            </DeleteParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>

    </article>
</asp:Content>
