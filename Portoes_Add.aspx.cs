using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Portoes_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] == null || !Session["tipo"].Equals("Administrador"))
                Response.Redirect("login.aspx");

            Page.Title = "Adicionar Portoes - GAPA";
        }

        protected void btAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                Portoes portao = new Portoes();
                portao.nome = tbNome.Text;
                portao.ip = tbIP.Text;
                try
                {
                    portao.camera = int.Parse(tbCamera.Text);
                }
                catch {
                    if(String.IsNullOrEmpty(tbCamera.Text))
                    portao.camera = 0;
                    else
                    {
                        throw new Exception("O campo camera deve ser o número do canal da camera.");
                    }
                }

                portao.captura = cbCaptura.Checked;
                portao.ativo = cbAtivo.Checked;

                if (String.IsNullOrEmpty(portao.nome) || String.IsNullOrEmpty(portao.ip)) throw new Exception("Deve preencher os campos \"Nome\" e \"IP\"");
            
                int id = BaseDados.Instance.adicionarPortao(portao);
             
                

                Response.Redirect("Portoes_G.aspx");

            }
            catch (Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErro.CssClass = "alert alert-danger";
            }
        }
    }
}