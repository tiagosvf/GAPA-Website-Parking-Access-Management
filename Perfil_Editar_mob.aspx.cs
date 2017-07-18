using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Perfil_Editar_mob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] == null)
            {
                Response.Redirect("login.aspx");
            }

            int id = int.Parse(Session["id"].ToString());
            if (!IsPostBack)
            {
                DataTable dados = BaseDados.Instance.devolveDadosUtilizador(id);
                tbUsername.Text = dados.Rows[0]["username"].ToString();
                tbEmail.Text = dados.Rows[0]["email"].ToString();
                tbNome.Text = dados.Rows[0]["nome"].ToString();
            }


        }

        protected void btEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Session["id"].ToString());
                DataTable dados = BaseDados.Instance.devolveDadosUtilizador(id);

                bool test = BaseDados.Instance.verificarUsername(tbUsername.Text, id);
                if (test == true)
                    throw new Exception("Já existe um utilizador com o Username escolhido");
                if (tbNome.Text.Length < 2)
                    throw new Exception("Nome muito curto");
                if (tbEmail.Text.Contains("@") == false || tbEmail.Text.Contains(".") == false)
                    throw new Exception("Email invalido");

                Utilizadores update = new Utilizadores();
                update.id = int.Parse(dados.Rows[0]["id"].ToString());
                update.username = tbUsername.Text;
                update.email = tbEmail.Text;
                update.nome = tbNome.Text;
                update.tipo = dados.Rows[0]["tipo"].ToString();
                update.ativo = bool.Parse(dados.Rows[0]["ativo"].ToString());
                update.perfil = int.Parse(dados.Rows[0]["Id_Perfil"].ToString());
                BaseDados.Instance.atualizarUtilizador(update);
                Response.Redirect("Perfil_mob.aspx");
            }
            catch (Exception erro)
            {
                lbErro.Text = "Erro: " + erro.Message;
                lbErro.CssClass = "alert alert-danger";
                return;
            }
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil_mob.aspx");
        }

        protected void btReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil_AlterPass_mob.aspx");
        }
    }
}