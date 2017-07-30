<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Eventos_Detalhes.aspx.cs" Inherits="GAPA.Eventos_Detalhes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <article>
    <div class="container">



        <div class="row">

            <div class="col-md-8">
                <asp:image id="img" runat="server" width="1000" align="center" cssclass="img-thumbnail" />
                <br />
                <br />
            </div>

            <div class="col-md-4">
                <h2>Evento</h2>
                <p>
                    <div class="form-group">
                        <label for="tbTipo">Tipo</label>
                        <br />
                        <asp:label id="lbTipo" runat="server" text="Label"></asp:label>
                    </div>

                </p>

                <p>

                    <div class="form-group">
                        <label for="tbPortao">Portão</label>
                        <br />
                        <asp:label id="lbPortao" runat="server" text="Label"></asp:label>
                    </div>
                </p>
                <p>
                    <div class="form-group">
                        <label for="tbUtilizador">Utilizador</label>
                        <br />
                        <asp:label id="lbUtilizador" runat="server" text="Label"></asp:label>
                    </div>

                </p>
                <p>
                    <div class="form-group">
                        <label for="tbData">Data e hora</label>
                        <br />
                        <asp:label id="lbData" runat="server" text="Label"></asp:label>
                    </div>
                </p>

                  <p>
                    <div class="form-group">
                        <label for="tbMatricula">Matrícula</label>
                        <br />
                        <asp:label id="lbMatricula" runat="server" text="Label"></asp:label>
                    </div>
                </p>
                <br />


            </div>

        </div>







        <div class="form-group">
            <label for="tbDescricao">Descrição</label>
            <br />
            <asp:textbox id="lbDescricao" runat="server" text="Label" textmode="Multiline" enabled="false" cssclass="form-control"></asp:textbox>
        </div>

    </div>



        </article>

</asp:Content>
