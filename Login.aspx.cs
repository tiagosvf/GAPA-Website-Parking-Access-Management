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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int id_u = 2;
            Utilizadores add = new Utilizadores();
            add.username = tbUsername.Text;
            add.password = tbPassword.Text;
            DataTable utilizador = BaseDados.Instance.verificarLogin(add);
            if (utilizador == null)
            {

                Eventos evento = new Eventos();
                evento.tipo = "Login";
                try { id_u = BaseDados.Instance.devolveIdUsername(add.username); } catch { }
                evento.id_utilizador = id_u;

                evento.descricao = "O inicio de sessão falhou.";
                evento.portao = 3;


                int id_evento = BaseDados.Instance.adicionarEventos(evento);

                lbErro.Text = "Login falhou.";
                lbErro.CssClass = "alert alert-danger";
                return;
            }
            Session["nome"] = utilizador.Rows[0]["nome"].ToString();
            Session["tipo"] = utilizador.Rows[0]["tipo"].ToString();
            Session["id"] = utilizador.Rows[0]["id"].ToString();
            Session["produtos"] = "";
            div_login.Visible = false;

            Response.Redirect("dashboard.aspx");
        }
    }
}