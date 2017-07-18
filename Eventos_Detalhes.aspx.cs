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
    public partial class Eventos_Detalhes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["tipo"] == null || !Session["tipo"].Equals("Administrador") && !Session["tipo"].Equals("User"))
               Response.Redirect("login.aspx");

            if (Request["id"] == null)
                Response.Redirect("Eventos_G.aspx");

            try
            {
                int id = int.Parse(Request["id"].ToString());


                DataTable dados = BaseDados.Instance.devolveDadosEvento(id);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("Não existe nenhum evento com o id indicado");


                Page.Title = dados.Rows[0][3].ToString() + " - GAPA";
                lbTipo.Text = dados.Rows[0][1].ToString();
                lbPortao.Text = dados.Rows[0][6].ToString();
                lbUtilizador.Text = dados.Rows[0][7].ToString();
                lbData.Text = dados.Rows[0][3].ToString();
                lbDescricao.Text = dados.Rows[0][4].ToString();
                lbMatricula.Text = dados.Rows[0][8].ToString();


                //capa
                string ficheiro = @"~\Imagens_eventos\default.jpg";
                if (File.Exists(Server.MapPath("/Imagens_eventos/i" + dados.Rows[0][0].ToString() + ".jpg")))
                   ficheiro = @"~\Imagens_eventos\i" + dados.Rows[0][0].ToString() + ".jpg";
                img.ImageUrl = ficheiro;
                img.Width = 1000;
              
            }
            catch
            {
                Response.Redirect("Eventos_G.aspx");
            }
        }
    }
}