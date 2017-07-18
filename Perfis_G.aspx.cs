using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Permissoes_G : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["tipo"] == null || !Session["tipo"].Equals("Administrador"))
                Response.Redirect("login.aspx");

            Page.Title = "Gerir Perfis - GAPA";
            if (!IsPostBack)
            {

                preencherCBL_Permissoes();
                preencherCBL_Users();
                cbl_users.Enabled = false;
                cbl_permissoes.Enabled = false;
                btnGuardar.Enabled = false;
                btnGuardarUsers.Enabled = false;

                //     lb_portoes.Visible = false;
            }
            //   preencherGV_Perfis(); //https://stackoverflow.com/questions/4793955/how-to-add-a-confirm-delete-option-in-asp-net-gridview

            gvPerfis.RowCommand += new GridViewCommandEventHandler(this.gvPerfis_RowCommand);


        }

        public List<int> portoes_perfil
        {
            get
            {
                if (HttpContext.Current.Session["Portoes_perfil"] == null)
                {
                    HttpContext.Current.Session["Portoes_perfil"] = new List<int>();
                }
                return HttpContext.Current.Session["Portoes_perfil"] as List<int>;
            }
            set
            {
                HttpContext.Current.Session["Portoes_perfil"] = value;
            }

        }

        public List<int> users_perfil
        {
            get
            {
                if (HttpContext.Current.Session["Users_perfil"] == null)
                {
                    HttpContext.Current.Session["Users_perfil"] = new List<int>();
                }
                return HttpContext.Current.Session["Users_perfil"] as List<int>;
            }
            set
            {
                HttpContext.Current.Session["Users_perfil"] = value;
            }

        }
        public  string id_perfilx
        {
            get
            {
                if (HttpContext.Current.Session["Id_perfil"] == null)
                {
                    HttpContext.Current.Session["Id_perfil"] = "";
                }
                return HttpContext.Current.Session["Id_perfil"] as string;
            }
            set
            {
                HttpContext.Current.Session["Id_perfil"] = value;
            }

        }

        private void preencherCBL_Permissoes()
        {
            cbl_permissoes.Items.Clear();

            List<Portoes> portoes = BaseDados.Instance.listarDadosPortoes();

            for (int i = 0; i < portoes.Count; i++)
            {
                cbl_permissoes.Items.Add(new ListItem("&nbsp;" + portoes[i].nome.ToString(), portoes[i].id.ToString()));
            }
            cbl_permissoes.DataBind();
        }

        private void preencherCBL_PermissoesClick()
        {
            portoes_perfil.Clear();

            cbl_permissoes.Enabled = true;
            for (int i = 0; i < cbl_permissoes.Items.Count; i++)
            {
                cbl_permissoes.Items[i].Selected = false;
            }
            DataTable permissoes = BaseDados.Instance.listaPermissoesPerfis(int.Parse(id_perfilx));
            btnGuardar.Enabled = true;
            for (int i = 0; i < permissoes.Rows.Count; i++)
            {
                for (int u = 0; u < cbl_permissoes.Items.Count; u++)
                {
                    if (permissoes.Rows[i][0].ToString() == cbl_permissoes.Items[u].Value)
                    {
                        portoes_perfil.Add(int.Parse(cbl_permissoes.Items[u].Value));
                        cbl_permissoes.Items[u].Selected = true;

                    }
                }

            }
        }

        private void preencherCBL_Users()
        {
            cbl_users.Items.Clear();

            DataTable users = BaseDados.Instance.devolveDadosUtilizador();

            for (int i = 0; i < users.Rows.Count; i++)
            {
                cbl_users.Items.Add(new ListItem("&nbsp;" + users.Rows[i][1].ToString() + " (" + users.Rows[i][5].ToString() + ")", users.Rows[i][0].ToString()));
            }
            cbl_users.DataBind();
        }

        private void preencherCBL_UsersClick()
        {
            users_perfil.Clear();

            cbl_users.Enabled = true;
            for (int i = 0; i < cbl_users.Items.Count; i++)
            {
                cbl_users.Items[i].Selected = false;
            }
            DataTable users = BaseDados.Instance.listaUsersPerfis(int.Parse(id_perfilx));
            btnGuardarUsers.Enabled = true;
            for (int i = 0; i < users.Rows.Count; i++)
            {
                for (int u = 0; u < cbl_users.Items.Count; u++)
                {
                    if (users.Rows[i][0].ToString() == cbl_users.Items[u].Value)
                    {
                        users_perfil.Add(int.Parse(cbl_users.Items[u].Value));
                        cbl_users.Items[u].Selected = true;

                    }
                }

            }
        }


        protected void preencherGV_Perfis()
        {

            gvPerfis.DataSource = null;
            gvPerfis.Columns.Clear();

            DataTable perfis = BaseDados.Instance.listaPerfis();

            gvPerfis.DataSource = perfis;

            ButtonField btn_permissoes = new ButtonField();
            btn_permissoes.HeaderText = "Permissões";

            btn_permissoes.Text = "Ver permissões";
            btn_permissoes.ButtonType = ButtonType.Button;

            btn_permissoes.ControlStyle.CssClass = "btn btn-default";
            btn_permissoes.CommandName = "Permissoes";
            gvPerfis.Columns.Add(btn_permissoes);

            ButtonField btn_eliminar = new ButtonField();
            btn_eliminar.HeaderText = "Eliminar";
            btn_eliminar.Text = "Eliminar perfil";
            btn_eliminar.ControlStyle.CssClass = "btn btn-danger";
            btn_eliminar.ButtonType = ButtonType.Button;


            btn_eliminar.CommandName = "Eliminar";
            gvPerfis.Columns.Add(btn_eliminar);

            
                gvPerfis.DataBind();


        }



        private void gvPerfis_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int linha = int.Parse(e.CommandArgument as string);
                id_perfilx = gvPerfis.Rows[linha].Cells[2].Text;
                if (e.CommandName == "Permissoes")
                {

                    preencherCBL_PermissoesClick();
                    preencherCBL_UsersClick();

                    // Response.Redirect("detalhescompra.aspx?id=" + id_venda);
                }
                if (e.CommandName == "Eliminar")
                {

                    /* ScriptManager.RegisterStartupScript(this, typeof(string), "confirm",
                     "myTestFunction();", true);*/
                   bool verificacao =  BaseDados.Instance.verificarperfis(int.Parse(id_perfilx));
                   if (verificacao == true )
                   {
                        lbErro.Text = "Existem utilizadores com esse perfil";               
                    }
                    else
                    {
                        BaseDados.Instance.removerPerfil(int.Parse(id_perfilx));

                        preencherGV_Perfis();
                    }
                    

                }
            }
            catch { }
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Perfis_Add.aspx");
        }

        protected void cbl_permissoes_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_permissoes.Items.Count; i++)
            {
                if (cbl_permissoes.Items[i].Selected == true)
                {
                    bool existe = false;
                    for (int u = 0; u < portoes_perfil.Count; u++)
                    {
                        if (portoes_perfil[u] == int.Parse(cbl_permissoes.Items[i].Value))
                        {
                            existe = true;
                            break;
                        }

                    }
                    if (existe == false)
                    {
                        Permissoes permissao = new Permissoes();
                        permissao.portao = int.Parse(cbl_permissoes.Items[i].Value);
                        permissao.id_perfil = int.Parse(id_perfilx);
                        BaseDados.Instance.adicionarPermissao(permissao);

                    }

                }
                else
                {
                    bool existe = false;
                    for (int u = 0; u < portoes_perfil.Count; u++)
                    {
                        if (portoes_perfil[u] == int.Parse(cbl_permissoes.Items[i].Value))
                        {
                            existe = true;
                            break;
                        }

                    }
                    if (existe == true)
                    {
                        Permissoes permissao = new Permissoes();
                        permissao.portao = int.Parse(cbl_permissoes.Items[i].Value);
                        permissao.id_perfil = int.Parse(id_perfilx);
                        BaseDados.Instance.removerPermissao(permissao);

                    }
                }

            }

            preencherCBL_PermissoesClick();

        }


        protected void cblUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

   
        protected void btnGuardarUsers_Click1(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_users.Items.Count; i++)
            {
                if (cbl_users.Items[i].Selected == true)
                {
                    bool existe = false;
                    for (int u = 0; u < users_perfil.Count; u++)
                    {
                        if (users_perfil[u] == int.Parse(cbl_users.Items[i].Value))
                        {
                            existe = true;
                            break;
                        }

                    }
                    if (existe == false)
                    {
                        Utilizadores utilizador = new Utilizadores();
                        utilizador.id= int.Parse(cbl_users.Items[i].Value);
                        utilizador.perfil = int.Parse(id_perfilx);
                        BaseDados.Instance.atualizarPerfil(utilizador);

                    }

                }
                else
                {
                    bool existe = false;
                    for (int u = 0; u < users_perfil.Count; u++)
                    {
                        if (users_perfil[u] == int.Parse(cbl_users.Items[i].Value))
                        {
                            existe = true;
                            break;
                        }

                    }
                    if (existe == true)
                    {
                        Utilizadores utilizador = new Utilizadores();
                        utilizador.id = int.Parse(cbl_users.Items[i].Value);
                        utilizador.perfil = int.Parse(id_perfilx);
                        BaseDados.Instance.atualizarPerfil(utilizador);

                    }
                }

            }

            preencherCBL_UsersClick();

        }
    }
}