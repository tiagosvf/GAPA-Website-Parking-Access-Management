using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Perfil_mob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
              
            int id = int.Parse(Session["id"].ToString());
            DataTable dados = BaseDados.Instance.devolveDadosUtilizador(id);
            lbUsername.Text = dados.Rows[0]["username"].ToString();
            lbEmail.Text = dados.Rows[0]["email"].ToString();
            lbNome.Text = dados.Rows[0]["nome"].ToString();
            lbPerfil.Text = dados.Rows[0]["perfil"].ToString();
            }
            catch (Exception)
            {
                Response.Redirect("Login.aspx");
            }

        }

        protected void btEditar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil_editar_mob.aspx");
        }

        protected void btnApk_Click(object sender, EventArgs e)
        {
            Response.ContentType = "Application/vnd.android.package-archive";
            Response.AppendHeader("Content-Disposition", "attachment; filename=gapa.apk");
            Response.TransmitFile(Server.MapPath("~/apk/gapa.apk"));
            Response.End();
        }
    }
}