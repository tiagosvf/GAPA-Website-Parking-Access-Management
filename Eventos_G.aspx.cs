using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Eventos_G : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] == null || !Session["tipo"].Equals("Administrador") && !Session["tipo"].Equals("User"))
                Response.Redirect("login.aspx");

            Page.Title = "Gerir Eventos - GAPA";
            DataTable dados = BaseDados.Instance.listarDataTableEventos();
            if (Session["tipo"].ToString() == "Administrador")
            {
                dados= BaseDados.Instance.listarDataTableEventos();

            }
            else if (Session["tipo"].ToString() == "User")
            {
                int id = int.Parse(Session["id"].ToString());
                dados = BaseDados.Instance.listarDataTableEventos(id);
            }

            if (dados == null || dados.Rows.Count == 0) return;
            if (!IsPostBack)
            {
                atualizarGv(dados);
            }

       
        
            gvEventos.Sorting += new GridViewSortEventHandler(this.gvEventos_Sorting);
            gvEventos.RowCommand += new GridViewCommandEventHandler(this.gvEventos_RowCommand);
          

        }

        private void gvEventos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int linha = int.Parse(e.CommandArgument as string);
                int id_evento = int.Parse(gvEventos.Rows[linha].Cells[2].Text);
                if (e.CommandName == "detalhes")
                {
                    Response.Redirect("Eventos_Detalhes.aspx?id=" + id_evento);
                }
            }
            catch { }
        }

        private void gvEventos_Sorting(object sender, GridViewSortEventArgs e)
        {

            //Retrieve the table from the session object.
            DataTable dt = Session["TaskTable"] as DataTable;

            if (dt != null)
            {

                //Sort the data.
                gvEventos.Columns.Clear();
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gvEventos.DataSource = Session["TaskTable"];

                ImageField ifImagem = new ImageField();
                ifImagem.HeaderText = "Imagem";
              
                ifImagem.DataImageUrlFormatString = "~/Imagens_eventos/i{0}.jpg";
                ifImagem.NullImageUrl = "~/Imagens_eventos/default.jpg";

                ifImagem.DataImageUrlField = "id";
                ifImagem.ControlStyle.Width = 100;
                gvEventos.Columns.Add(ifImagem);

                ButtonField btDetalhes = new ButtonField();
                btDetalhes.HeaderText = "Detalhes";
                btDetalhes.Text = "Detalhes";
                btDetalhes.ButtonType = ButtonType.Button;
                btDetalhes.CommandName = "detalhes";
                btDetalhes.ControlStyle.CssClass = "btn btn-default";
                gvEventos.Columns.Add(btDetalhes);


                gvEventos.DataBind();
                //gvEventos.Columns[1].ControlStyle.Width = 100;
            //    gvEventos.Rows[gvEventos.Rows.Count].Cells[1].ControlStyle.Width = 100;
            }
        }

        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }
        
        private void atualizarGv(DataTable dados)
        {
            //limpar grid
            gvEventos.Columns.Clear();


            gvEventos.DataSource = dados;
            gvEventos.AutoGenerateColumns = true;
        

            ImageField ifImagem = new ImageField();
            ifImagem.HeaderText = "Imagem";
 
            ifImagem.DataImageUrlFormatString = "~/Imagens_eventos/i{0}.jpg";
            ifImagem.NullImageUrl = "~/Imagens_eventos/default.jpg";
                  ifImagem.DataImageUrlField = "id";
            ifImagem.ControlStyle.Width = 100;
            gvEventos.Columns.Add(ifImagem);

            ButtonField btDetalhes = new ButtonField();
            btDetalhes.HeaderText = "Detalhes";
            btDetalhes.Text = "Detalhes";
            btDetalhes.ButtonType = ButtonType.Button;
            btDetalhes.CommandName = "detalhes";
            btDetalhes.ControlStyle.CssClass = "btn btn-default";
            gvEventos.Columns.Add(btDetalhes);

            Session["TaskTable"] = dados;

            gvEventos.DataBind();
        }



        private void atualizarGv()
        {
            //limpar grid
            DataTable dados = BaseDados.Instance.listarDataTableEventos();
            gvEventos.Columns.Clear();


            gvEventos.DataSource = dados;
            gvEventos.AutoGenerateColumns = true;
            gvEventos.AllowPaging = true;
          
            ImageField ifImagem = new ImageField();
            ifImagem.HeaderText = "Imagem";
      
            ifImagem.DataImageUrlFormatString = "~/Imagens_eventos/i{0}.jpg";
            ifImagem.NullImageUrl = "~/Imagens_eventos/default.jpg";
           
            ifImagem.DataImageUrlField = "id";
            ifImagem.ControlStyle.Width = 100;          
            gvEventos.Columns.Add(ifImagem);

            ButtonField btDetalhes = new ButtonField();
            btDetalhes.HeaderText = "Detalhes";
            btDetalhes.Text = "Detalhes";
            btDetalhes.ButtonType = ButtonType.Button;
            btDetalhes.CommandName = "detalhes";
            btDetalhes.ControlStyle.CssClass = "btn btn-default";
            gvEventos.Columns.Add(btDetalhes);

            Session["TaskTable"] = dados;

            gvEventos.DataBind();
        }

        protected void btPesquisa_Click(object sender, EventArgs e)
        {
            
            string text = tbPesquisa.Text;
            DataTable dados = BaseDados.Instance.pesquisarEventos(text);

            if (Session["tipo"].ToString() == "Administrador")
            {
                dados = BaseDados.Instance.pesquisarEventos(text);

            }
            else if (Session["tipo"].ToString() == "User")
            {
                int id = int.Parse(Session["id"].ToString());
                dados = BaseDados.Instance.pesquisarEventos(text,id);
            }

            atualizarGv(dados);

        }

        protected void gvEventos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEventos.PageIndex = e.NewPageIndex;
            DataTable dados = BaseDados.Instance.listarDataTableEventos();
            if (Session["tipo"].ToString() == "Administrador")
            {
                dados = BaseDados.Instance.listarDataTableEventos();

            }
            else if (Session["tipo"].ToString() == "User")
            {
                int id = int.Parse(Session["id"].ToString());
                dados = BaseDados.Instance.listarDataTableEventos(id);
            }
            atualizarGv(dados);
        }
    }
}