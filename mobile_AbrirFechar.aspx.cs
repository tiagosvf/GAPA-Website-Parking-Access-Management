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
using System.Drawing;

namespace GAPA
{
    public partial class mobile_AbrirFechar : System.Web.UI.Page
    {
        string ip;
        int id_evento = 0;
        int channel=0;
        string cod_sessao = "";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            int id_portao = 3;
            int id_u = 2;
            try
            {
                id_portao = int.Parse(Request["id"].ToString());
                id_u = int.Parse(Request["utilizador"].ToString());
                cod_sessao = Request["pw"].ToString();
                DataTable novo = BaseDados.Instance.devolveDadosPortoes(id_portao, id_u,cod_sessao);
                if (novo.Rows == null || novo.Rows.Count == 0 || novo.ToString() == "{}")
                {
                    Response.Write("-2");
                }
                ip = novo.Rows[0]["ip"].ToString();
               
                channel = int.Parse(novo.Rows[0]["camera"].ToString());

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + ip + "/gate/index.php?trigger=1");
                    request.Timeout = 2000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                }
                catch(Exception)
                {
                   
                    throw new Exception("Não foi possivel aceder ao portão");
                }

               
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

               



                Thread myThread = new Thread(new ThreadStart(Tarefa));
                myThread.Start();
                
                Response.Write("Comando efetuado com sucesso.");
           
            }
            catch(Exception erro)
            {
                string erro2 = erro.ToString();
                try { string erro3 = erro.InnerException.ToString(); } catch { }
                Eventos add = new Eventos();
                add.tipo = "Ação Portão";
                add.id_utilizador = id_u;
                add.descricao = "Erro ao alterar o estado do portão.";
                add.portao = id_portao;
                id_evento = BaseDados.Instance.adicionarEventos(add);
                Thread myThread = new Thread(new ThreadStart(Tarefa));
                myThread.Start();

                Response.Write("-1");
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

        public void Tarefa()
        {
        
            try
            {
                receberImagem();
            }
            catch (Exception ed) { string ex = ed.Message.ToString();
                //ex = ed.InnerException.ToString();

            }
        }

        public void receberImagem()
        {
            /*
            WebRequest requestPic = WebRequest.Create("http://gapa:gapa1234@192.168.1.9/ISAPI/Streaming/channels/" + channel + "/picture?videoResolutionWidth=1920&videoResolutionHeigh=1080");
            WebResponse responsePic = requestPic.GetResponse();
            Image webImage = Image.FromStream(responsePic.GetResponseStream());*/

            string nomeLocal =  Server.MapPath("/Imagens_eventos/i" + id_evento + ".jpg");
            WebClient client = new WebClient();
            var token = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("gapa:gapa1234"));
            client.Headers.Add("Authorization", "Basic " + token);
            string url = "http://192.168.1.9/ISAPI/Streaming/channels/" + channel + "/picture?videoResolutionWidth=1920&videoResolutionHeight=1080";
           //string url = "http://cdn01.ib.infobae.com/adjuntos/162/imagenes/014/014/0014014674.jpg";
            client.DownloadFile(url,nomeLocal);

            using (var imagem = System.Drawing.Image.FromFile(nomeLocal))
                using (var novaImagem = Genericas.redimensionarImagem(imagem, 600, 800))
            {
                novaImagem.Save(Server.MapPath("/Imagens_eventos_mini/i" + id_evento + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            }


        }



    }
}