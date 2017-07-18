<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Alertas.aspx.cs" Inherits="GAPA.Alertas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <article>



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
                        <asp:GridView  ID="gvAlertas" BackColor="White"  runat="server" CssClass="table table-responsive" ForeColor="#00649D" PageIndex="1" PageSize="1" AllowCustomPaging="True" BorderStyle="None" GridLines="None" Style='height: 100%; width: 100%; object-fit: contain' AutoGenerateColumns="False" DataKeyNames="id" >
                  
                        </asp:GridView>
                   
                    </div>
                     <br /><asp:Label ID="lbErro" runat="server" />
                </div>
            </div>

        </div>
    </article>

</asp:Content>
