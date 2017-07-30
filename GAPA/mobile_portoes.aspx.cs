using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class mobile_portoes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id_utilizador = int.Parse(Request["id_utilizador"].ToString());
                List<Portoes> Portao = BaseDados.Instance.listarDadosPortoesUser(id_utilizador);
                string strg = JsonConvert.SerializeObject(Portao);
                Response.Write(strg);
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
                Response.Write("-1");
            }
           
        }
    }
}