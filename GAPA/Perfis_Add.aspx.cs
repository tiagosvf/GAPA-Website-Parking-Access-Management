using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Perfis_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["tipo"] == null || !Session["tipo"].Equals("Administrador"))
                Response.Redirect("index.aspx");

            Page.Title = "Adicionar Perfil - GAPA";
            if (!IsPostBack)
            {
                preencherCBL_Permissoes();
                preencherCBL_Users();
            }
        }


        private void preencherCBL_Permissoes()
        {
            cbl_permissoes.Items.Clear();

            List<Portoes> portoes = BaseDados.Instance.listarDadosPortoes();

            for (int i = 0; i < portoes.Count; i++)
            {
                cbl_permissoes.Items.Add(new ListItem("&nbsp;" + portoes[i].nome.ToString(), portoes[i].id.ToString()));
            }
            cbl_permissoes.DataBind();
        }

        private void preencherCBL_Users()
        {
            cbl_users.Items.Clear();


            DataTable users = BaseDados.Instance.devolveDadosUtilizador();

            for (int i = 0; i < users.Rows.Count; i++)
            {
                cbl_users.Items.Add(new ListItem("&nbsp;" + users.Rows[i][1].ToString() + " (" + users.Rows[i][5].ToString() + ")", users.Rows[i][0].ToString()));
            }
            cbl_users.DataBind();
        }

        protected void btAdicionar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbNome.Text))
            {
                lbErro.Text = "Deve preencher o campo do nome";
                lbErro.CssClass = "alert alert-danger";
            }
            else
            {
                int id_perfilx = BaseDados.Instance.adicionarPerfil(tbNome.Text);

                for (int i = 0; i < cbl_permissoes.Items.Count; i++)
                {
                    if (cbl_permissoes.Items[i].Selected == true)
                    {

                        Permissoes permissao = new Permissoes();
                        permissao.portao = int.Parse(cbl_permissoes.Items[i].Value);
                        permissao.id_perfil = id_perfilx;
                        BaseDados.Instance.adicionarPermissao(permissao);



                    }


                }


                for (int i = 0; i < cbl_users.Items.Count; i++)
                {
                    if (cbl_users.Items[i].Selected == true)
                    {

                        Utilizadores utilizador = new Utilizadores();
                        utilizador.id = int.Parse(cbl_users.Items[i].Value);
                        utilizador.perfil = id_perfilx;
                        BaseDados.Instance.atualizarPerfil(utilizador);



                    }

                }

                Response.Redirect("/Perfis_G.aspx");

            }


        }
     
    }
}