using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Utilizadores_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] == null || !Session["tipo"].Equals("Administrador"))
                Response.Redirect("login.aspx");
            preencherPerfis();
        }
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
        protected void btAdd_Click(object sender, EventArgs e)
        {
            //validar form
            try
            {
                bool test = BaseDados.Instance.verificarUsername(tbUsername.Text,-1);
                if (test == true)
                    throw new Exception("Já existe um utilizador com o Username escolhido");
              
                if (tbNome.Text.Length < 2)
                    throw new Exception("Nome muito curto");
                if (tbEmail.Text.Contains("@") == false || tbEmail.Text.Contains(".") == false)
                    throw new Exception("Email invalido");

                bool testemail = BaseDados.Instance.verificarEmail(tbEmail.Text, -1);
                if (test == true)
                    throw new Exception("Já existe um utilizador com o email escolhido");

                //Enviar um email com uma Password inicial
                string pass = Genericas.GetUniqueKey(10);
                string email = tbEmail.Text;
                string username = tbUsername.Text;
                string mensagem = "Está agora resgistado em Softinsa GAPA, para alterar a sua password vá ao seu perfil e clique em 'Editar Perfil'. O seu username é: "+ username + " e a sua password inicial é:" + pass + "";
                Helper.enviarMail("softinsaGAPA@gmail.com", "S1o2F3t4", email, "Registo Softinsa GAPA", mensagem);

                Utilizadores update = new Utilizadores();
                update.username = tbUsername.Text;
                update.email = tbEmail.Text;
                update.nome = tbNome.Text;
                update.password = pass;
                update.tipo = ddl_Tipo.SelectedItem.Text;
                update.ativo = cb_ativo.Checked;
                update.perfil = int.Parse(ddl_perfil.SelectedItem.Value);
                BaseDados.Instance.adicionarUtilizador(update);

                Response.Redirect("Utilizadores_G.aspx");
            }
            catch (Exception erro)
            {
                lbErro.Text = "Erro: " + erro.Message;
                lbErro.CssClass = "alert alert-danger";
                return;
            }
        }
        
    }
}