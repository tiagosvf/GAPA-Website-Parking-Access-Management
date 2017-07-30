using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Login_repor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btRepor_Click(object sender, EventArgs e)
        {
            try
            {
                Utilizadores repor = new Utilizadores();
                repor.cod_seguranca = Genericas.GetUniqueKey(10);
                repor.email = tbEmail.Text;
                bool verificar = BaseDados.Instance.verificarEmail(repor);
                if (verificar == false)
                    throw new Exception("O email indicado não existe");
                BaseDados.Instance.defenirRepor(repor);

                // create reader & open file
                TextReader tr = new StreamReader(Server.MapPath("/ipMatriculas.txt"));
                string caminho = tr.ReadLine();
                string ip = tr.ReadLine();
                tr.Close();

                string mensagem = ip+"/Login_confirmar.aspx?cod_seguranca="+repor.cod_seguranca+"&email="+repor.email;
                Helper.enviarMail("softinsaGAPA@gmail.com", "S1o2F3t4", repor.email, "Repor Password", mensagem);
                Response.Redirect("Login.aspx");
            }
            catch (Exception erro)
            {
                lbErro.Text = erro.Message;
                lbErro.CssClass = "alert alert-danger";
                return;
            }
        }
    }
}