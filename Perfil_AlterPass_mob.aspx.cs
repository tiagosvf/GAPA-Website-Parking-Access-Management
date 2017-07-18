using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Perfil_AlterPass_mob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] == null)
            {
                Response.Redirect("login.aspx");
            }
        }
        protected void btRepor_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Session["id"].ToString());

                if (tbPassword.Text != tbConfirmar.Text)
                    throw new Exception("As Passwords não coincidem ");
                if (tbPassword.Text.Length < 5)
                    throw new Exception("Password muito pequena");

                Utilizadores novo = new Utilizadores();
                novo.password = tbPassword.Text;
                novo.id = id;
                BaseDados.Instance.atualizarPassword(novo);

                Response.Redirect("Perfil.aspx");
            }
            catch (Exception erro)
            {
                lbErro.Text = "Erro: " + erro.Message;
                lbErro.CssClass = "alert alert-danger .label-erro";
                return;
            }

        }
    }
}