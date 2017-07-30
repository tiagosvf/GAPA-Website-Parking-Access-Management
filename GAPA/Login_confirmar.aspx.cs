using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Login_confirmar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
            string cod_reposicao = Request["cod_seguranca"].ToString();
            string email = Request["email"].ToString();
            Utilizadores novo = new Utilizadores();
            novo.password = Genericas.GetUniqueKey(10);
            novo.cod_seguranca = cod_reposicao;
            BaseDados.Instance.atualizarPasswordLink(novo);

            string mensagem = "A sua password reposta, nova password:" + novo.password + "";
            Helper.enviarMail("softinsaGAPA@gmail.com", "S1o2F3t4", email, "Reposição Password", mensagem);
            }
            catch (Exception)
            {

                Response.Redirect("login.aspx");
            }
        }
    }
}