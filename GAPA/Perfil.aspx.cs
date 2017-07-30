using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
            int id = int.Parse(Session["id"].ToString());
            DataTable dados = BaseDados.Instance.devolveDadosUtilizador(id);
            lbUsername.Text = "&nbsp;&nbsp;" + dados.Rows[0]["username"].ToString();
            lbEmail.Text = "&nbsp;&nbsp;" + dados.Rows[0]["email"].ToString();
            lbNome.Text = "&nbsp;&nbsp;" + dados.Rows[0]["nome"].ToString();
            lbPerfil.Text = "&nbsp;&nbsp;" + dados.Rows[0]["perfil"].ToString();
            }
            catch (Exception)
            {
                Response.Redirect("Login.aspx");
            }

        }
        
        protected void btEditar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil_editar.aspx");
        }
    }
}