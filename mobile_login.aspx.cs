using GAPA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class mobile_login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id_evento = 0;
            int id_u = 2;
            int id_portao = 3;
            try
            {
            

                Utilizadores novo = new Utilizadores();
                novo.username = Request["username"].ToString();
                novo.password = Request["password"].ToString();

                id_u =BaseDados.Instance.devolveIdUsername(novo.username);
                
                DataTable user = BaseDados.Instance.verificarLogin(novo);
                int id2 = int.Parse(user.Rows[0]["id"].ToString());
                string tipo = user.Rows[0]["tipo"].ToString();

                Utilizadores add = new Utilizadores();
                add.id = id2;
                add.cod_sessao = Genericas.GetUniqueKey(10);

                BaseDados.Instance.defenirCod_sessao(add);
                string cod_sessao = BaseDados.Instance.devolverCod_sessao(id2);


                Response.Write(id2 + "," + tipo +"/"+cod_sessao);
          
            }
            catch (Exception ed)
            {
                Eventos add = new Eventos();
                add.tipo = "Login";
                add.id_utilizador = id_u;
                add.descricao = "O inicio de sessão falhou.";
                add.portao = id_portao;
                id_evento = BaseDados.Instance.adicionarEventos(add);

                string ex = ed.Message.ToString();
                Response.Write("-1" + ex);
            }
        }
    }
}