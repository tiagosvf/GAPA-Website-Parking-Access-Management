using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class mobile_ListaEventos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                List<Eventos> lista = new List<Eventos>();
                int id_utilizador = int.Parse(Request["id"].ToString());
                string tipo = BaseDados.Instance.devolveTipoUtilizador(id_utilizador);
                if (tipo.Equals("Administrador")) { lista = BaseDados.Instance.listarDadosEventos(); }
                else {lista = BaseDados.Instance.listarDadosEventosUtilizador(id_utilizador); }

                string strg = JsonConvert.SerializeObject(lista);
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