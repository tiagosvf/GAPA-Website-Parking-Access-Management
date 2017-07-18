using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Dashboard : System.Web.UI.Page
    {
        int id_evento = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["tipo"] == null || !Session["tipo"].Equals("Administrador") && !Session["tipo"].Equals("User"))
                Response.Redirect("login.aspx");

            Page.Title = "Dashboard - GAPA";
            preencherGV_Eventos();

            string link = Server.MapPath("/apk/gapa.apk");
           
            if (!IsPostBack)
            {
                atualizaDivCameras();


                
            }
            divAtualizar.Visible = false;
            if (Request["id_portao"] != null)
            {
                int id = int.Parse(Request["id_portao"]);

                hfID.Value = id.ToString();

                Thread myThread = new Thread(new ThreadStart(Tarefa));
                myThread.Start();
                Response.Redirect("/Dashboard.aspx#n" + id);
                
            }


        }

        private string getWebTex(int imagem)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            // create reader & open file
            TextReader tr = new StreamReader(Server.MapPath("/ipMatriculas.txt"));
            string caminho = tr.ReadLine();
            string ip = tr.ReadLine();
            tr.Close();
            string url = caminho + ip + "/Imagens_eventos/i" + imagem + ".jpg";
            try
            {
                string webData = wc.DownloadString(caminho + ip + "/Imagens_eventos/i" + imagem + ".jpg");
                if (webData.Contains("No license plates found.") == true)
                {
                    webData = "00-00-00";
                }
                else
                {
                    webData = webData.Substring(6, webData.IndexOf("confidence") - 8);
                }

                BaseDados.Instance.definirMatricula(webData, imagem);
                return webData;
            }
            catch {
                
                BaseDados.Instance.definirMatricula("00-00-00", imagem);
                return "00-00-00";
            }
        }

        protected void preencherGV_Eventos()
        {
            int id = int.Parse(Session["ID"].ToString());
            gvEventos.DataSource = null;
            gvEventos.Columns.Clear();

            DataTable perfis = BaseDados.Instance.lista5Eventos(id);

            gvEventos.DataSource = perfis;

            
            gvEventos.DataBind();


        }



        public void Tarefa()
        {

            DataTable dados = BaseDados.Instance.devolveIPPortao(int.Parse(hfID.Value));

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + dados.Rows[0][0] + "/gate/index.php?trigger=1");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                int id_portao = 3;
                int id_u = 2;
                id_portao = int.Parse(hfID.Value);
                try
                {
                    id_u = int.Parse(Session["id"].ToString());
                }
                catch
                { }



                bool horario = Genericas.Verificarhora(id_u, id_portao);
                if (horario == true)
                {

                    Eventos adicionar = new Eventos();
                    adicionar.tipo = "Abertura fora de horas";
                    adicionar.id_utilizador = id_u;
                    adicionar.descricao = "O portão foi aberto fora de horas!";
                    adicionar.portao = id_portao;
                    id_evento = BaseDados.Instance.adicionarEventos(adicionar);
                    try
                    {
                        getWebTex(id_evento);
                    }
                    catch { }
                }
                else if (horario == false)
                {
                    Eventos add = new Eventos();
                    add.tipo = "Ação Portão";
                    add.id_utilizador = id_u;
                    add.descricao = "Estado do portão alterado com sucesso.";
                    add.portao = id_portao;
                    id_evento = BaseDados.Instance.adicionarEventos(add);
                    try
                    {
                        getWebTex(id_evento);
                    }
                    catch { }
                }








            }
            catch
            {
                try
                {
                    int id_portao = 3;
                    int id_u = 2;
                    id_portao = int.Parse(hfID.Value);
                    try
                    {
                        id_u = int.Parse(Session["id"].ToString());
                    }
                    catch
                    { }

                    Eventos add = new Eventos();
                    add.tipo = "Ação Portão";
                    add.id_utilizador = id_u;
                    add.descricao = "Erro ao alterar o estado do portão.";
                    add.portao = id_portao;
                    id_evento = BaseDados.Instance.adicionarEventos(add);
                    try
                    {
                        getWebTex(id_evento);
                    }
                    catch { }
                }
                catch
                {
                    Eventos add = new Eventos();
                    add.tipo = "Ação Portão";
                    add.id_utilizador = 2;
                    add.descricao = "Erro ao alterar o estado do portão.";
                    add.portao = 3;
                    BaseDados.Instance.adicionarEventos(add);
                    try
                    {
                        getWebTex(id_evento);
                    }
                    catch { }
                }


            }
            preencherGV_Eventos();
            try
            {
                receberImagem();
            }
            catch (Exception ed)
            {
                string ex = ed.Message.ToString();
               
                try { ex = ed.InnerException.ToString();  }
               catch { }

            }


        }

        public void receberImagem()
        {

            DataTable dados = BaseDados.Instance.devolveChannelPortao(int.Parse(hfID.Value));
            string nomeLocal = Server.MapPath("/Imagens_eventos/i" + id_evento + ".jpg");
            WebClient client = new WebClient();
            var token = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("gapa:gapa1234"));
            client.Headers.Add("Authorization", "Basic " + token);
            string url = "http://192.168.1.9/ISAPI/Streaming/channels/" + dados.Rows[0][0] + "/picture?videoResolutionWidth=1920&videoResolutionHeight=1080";
            File.Delete(nomeLocal);
            client.DownloadFile(url, nomeLocal);

            using (var imagem = System.Drawing.Image.FromFile(nomeLocal))
            using (var novaImagem = Genericas.redimensionarImagem(imagem, 600, 800))
            {
                novaImagem.Save(Server.MapPath("/Imagens_eventos_mini/i" + id_evento + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }


        public void atualizaDivCameras()
        {

            //DataTable dados = BaseDados.Instance.devolveConsulta("SELECT camera, id, nome FROM Portoes WHERE ativo=1 AND captura=1");
            DataTable dados = BaseDados.Instance.listarPortoesUser(int.Parse(Session["id"].ToString()));



            if (dados == null || dados.Rows.Count == 0)
            {
                divCameras.InnerHtml = "";
                return;
            }

            string grelha = "<div class='container'>";
            grelha += " <div class='row'>";

            for (int i = 0; i < dados.Rows.Count; i++)
            {
                try
                {
                    string nomeLocal = Server.MapPath("/Imagens_portoes/i" + dados.Rows[i][0] + ".jpg");
                    WebClient client = new WebClient();
                    var token = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("gapa:gapa1234"));
                    client.Headers.Add("Authorization", "Basic " + token);
                    string url = "http://192.168.1.9/ISAPI/Streaming/channels/" + dados.Rows[i][4] + "/picture?videoResolutionWidth=1920&videoResolutionHeight=1080";
                    File.Delete(nomeLocal);
                    client.DownloadFile(url, nomeLocal);
                }
                catch { }





                if (i % 2 == 0)
                {
                    grelha += " <div class='left'>";
                    grelha += " <div class='col-md-6 col-md-pull-13 pull-left'>";
                }
                else
                {
                    grelha += " <div class='right'>";
                    grelha += " <div class='col-md-6 col-md-pull-0 pull-right'>";
                }
                //    grelha += "<div class='col-md-5'>";
                grelha += "<a name='n" + dados.Rows[i][1] + "'></a>";
                grelha += "<div class='panel panel-default'>";

                grelha += "<div class='panel-heading'>";

                grelha += "<h3 class='panel-title'>" + dados.Rows[i][1] + " <a href='/Dashboard.aspx#n" + dados.Rows[i][0] + "' class='pull-right' onclick='location.reload()' ><span class='glyphicon glyphicon-refresh'></span></a></h3> ";
                //    grelha += "<a href='/Dashboard.aspx' class='btn pull-right'>Atualizar <span class='glyphicon glyphicon-refresh'></span></a>";
                grelha += "</div>";
                try
                {

                    if (File.Exists(Server.MapPath("/Imagens_portoes/i" + dados.Rows[i][0] + ".jpg")))
                        grelha += " <img src='/Imagens_portoes/i" + dados.Rows[i][0] + ".jpg' style='height: 100%; width: 100%; object-fit: contain' />"; //O style serve para a imagem ficar do tamanho da div
                    else
                        throw new Exception("O ficheiro não existe");

                }
                catch (Exception)
                {
                    //TODO: PASSAR A IMAGEM PARA PRETO E ALTERAR O TAMANHO
                    grelha += "<img src='/Imagens_eventos/default.png' style='height: 100%; width: 100%; object-fit: contain'/>";
                }

                grelha += "<div class='panel-body'>";
                grelha += "<a href='/Dashboard.aspx?id_portao=" + dados.Rows[i][0] + "' class='btn btn-default pull-right'>Abrir / Fechar</a>";
                grelha += "</div>";
                grelha += "</div></div>";







            }
            grelha += "</div></div>";
            divCameras.InnerHtml += grelha;




        }

        protected void btnApk_Click(object sender, EventArgs e)
        {
            Response.ContentType = "Application/apk";
            Response.AppendHeader("Content-Disposition", "attachment; filename=gapa.apk");
            Response.TransmitFile(Server.MapPath("~/apk/gapa.apk"));
            Response.End();
        }
    }
}