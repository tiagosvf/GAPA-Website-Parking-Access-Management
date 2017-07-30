<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Portoes_G.aspx.cs" Inherits="GAPA.Portoes_G" %>

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
                        <asp:GridView ID="gvPortoes" BackColor="White" runat="server" CssClass="table table-responsive"  ForeColor="#00649D" PageIndex="1" PageSize="1" AllowCustomPaging="True"  BorderStyle="None" GridLines="None" style='height: 100%; width: 100%; object-fit: contain' AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="Portoes">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Atualizar"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" ForeColor="#cc0000" CommandName="Delete" Text="Eliminar" OnClientClick="return confirm('Tem a certeza que deseja eliminar este portão?');"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
                                <asp:BoundField DataField="ip" HeaderText="IP" SortExpression="ip" />
                                <asp:CheckBoxField DataField="captura" HeaderText="Captura" SortExpression="captura" />
                                <asp:BoundField DataField="camera" HeaderText="Camera" SortExpression="camera" />
                                <asp:CheckBoxField DataField="ativo" HeaderText="Ativo" SortExpression="ativo" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="Portoes" runat="server" ConnectionString="<%$ ConnectionStrings:sql %>" SelectCommand="SELECT * FROM Portoes" DeleteCommand="DELETE FROM Portoes WHERE id=@id" UpdateCommand="UPDATE Portoes SET nome=@nome,ip=@ip,captura=@captura,camera=@camera,ativo=@ativo WHERE id=@id">
                            <DeleteParameters>
                                <asp:Parameter Name="id" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="nome" />
                                <asp:Parameter Name="ip" />
                                <asp:Parameter Name="captura" />
                                <asp:Parameter Name="camera" />
                                <asp:Parameter Name="ativo" />
                                <asp:Parameter Name="id" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        
    </div>
        </article>
</asp:Content>
