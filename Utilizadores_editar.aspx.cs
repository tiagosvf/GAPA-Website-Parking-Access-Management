using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Utilizadores_editar : System.Web.UI.Page
    {
        int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] == null || !Session["tipo"].Equals("Administrador"))
                Response.Redirect("login.aspx");

            id = int.Parse(Request["id"].ToString());
            if (!IsPostBack)
            {

                try
                {

                    DataTable dados = BaseDados.Instance.devolveDadosUtilizador(id);
                    DataTable dados2 = BaseDados.Instance.devolveDadosUtilizador();
                    if (dados == null || dados.Rows.Count == 0)
                        throw new Exception("Não existe nenhum utilizador com o id indicado");

                    tbUsername.Text = dados.Rows[0]["username"].ToString();
                    tbEmail.Text = dados.Rows[0]["email"].ToString();
                    tbNome.Text = dados.Rows[0]["nome"].ToString();
                    
                    //selecionar o tipo
                    if (dados.Rows[0]["tipo"].ToString() == "Administrador")
                        ddl_Tipo.SelectedIndex = 1;
                    else
                        ddl_Tipo.SelectedIndex = 0;
                    //verificar se está ativo
                    if (dados.Rows[0]["ativo"].ToString() == "False")
                        cb_ativo.Checked = false;
                    else
                        cb_ativo.Checked = true;

                    preencherPerfis();
                    //selecionar o perfil
                    for (int i = 0; i < ddl_perfil.Items.Count; i++)
                    {
                        if (dados.Rows[0]["perfil"].ToString() == ddl_perfil.Items[i].Value)
                        {
                            ddl_perfil.SelectedIndex = i;
                        }
                    }



                }
                catch (Exception erro)
                {
                    lbErro.Text = "Erro: " + erro.Message;
                    lbErro.CssClass = "alert alert-danger";
                }
            }

        }
        // Preencher a DDL perfis
        private void preencherPerfis()
        {
            ddl_perfil.Items.Clear();
            List<Perfis> perfis = new List<Perfis>();
            perfis = BaseDados.Instance.listarDadosPerfis();
            for (int i = 0; i < perfis.Count; i++)
            {
                ddl_perfil.Items.Add(new ListItem(perfis[i].perfil, perfis[i].id.ToString()));
            }

        }

        protected void btEditar_Click(object sender, EventArgs e)
        {
            //validar form
            try
            {
                bool test = BaseDados.Instance.verificarUsername(tbUsername.Text, id);
                if (test == true)
                    throw new Exception("Já existe um utilizador com o Username escolhido");
                if (tbNome.Text.Length < 2)
                    throw new Exception("Nome muito curto");
                 if (tbEmail.Text.Contains("@") == false || tbEmail.Text.Contains(".") == false)
                    throw new Exception("Email invalido");

                bool testemail = BaseDados.Instance.verificarEmail(tbEmail.Text, -1);
                if (test == true)
                    throw new Exception("Já existe um utilizador com o email escolhido");


                Utilizadores update = new Utilizadores();
                update.id = id;
                update.username = tbUsername.Text;
                update.email = tbEmail.Text;
                update.nome = tbNome.Text;
                update.tipo = ddl_Tipo.SelectedItem.Text;
                update.ativo = cb_ativo.Checked;
                update.perfil = int.Parse(ddl_perfil.SelectedItem.Value);
                BaseDados.Instance.atualizarUtilizador(update);

                Response.Redirect("Utilizadores_G.aspx");
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
            Response.Redirect("Utilizadores_G.aspx");
        }

        protected void btReset_Click(object sender, EventArgs e)
        {
            DataTable dados = BaseDados.Instance.devolveDadosUtilizador(id);
            string email = dados.Rows[0]["email"].ToString();

            Utilizadores update = new Utilizadores();
            update.id = id;
            update.password = Genericas.GetUniqueKey(10);
            BaseDados.Instance.atualizarPassword(update);

            string mensagem = "A sua password reposta, nova password:" + update.password + "";
            Helper.enviarMail("softinsaGAPA@gmail.com", "S1o2F3t4", email, "Reposição Password", mensagem);

            Response.Redirect("Utilizadores_G.aspx");
        }
    }
}