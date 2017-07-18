using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Alertas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            gvAlertas.RowEditing += new GridViewEditEventHandler(this.gvAlertas_RowEditing);
            gvAlertas.RowCancelingEdit += new GridViewCancelEditEventHandler(this.gvAlertas_RowCancelingEdit);
            gvAlertas.RowUpdating += new GridViewUpdateEventHandler(this.gvAlertas_RowUpdating);

            if (!IsPostBack)
                atualizargv();
        }

        private void gvAlertas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int linha = e.RowIndex;
            Alertas_class alerta = new Alertas_class();
            try
            {
                alerta.id = int.Parse(gvAlertas.Rows[linha].Cells[1].Text);

                alerta.hora_inicial = TimeSpan.Parse(((TextBox)gvAlertas.Rows[linha].Cells[2].Controls[0]).Text);
                if (alerta.hora_inicial.ToString().Contains(".") == true)
                    throw new Exception("Formato incorreto");

                bool teste = alerta.hora_inicial.ToString().Contains(".");
                alerta.hora_final = TimeSpan.Parse(((TextBox)gvAlertas.Rows[linha].Cells[3].Controls[0]).Text);
                if (alerta.hora_final.ToString().Contains(".") == true)
                    throw new Exception("Formato incorreto");

                alerta.Tipo_evento = ((TextBox)gvAlertas.Rows[linha].Cells[4].Controls[0]).Text;
                if (alerta.Tipo_evento == null) throw new Exception("Preencha o evento");

                BaseDados.Instance.atualizarAnomalia(alerta);
                gvAlertas.EditIndex = -1;

                atualizargv();
                lbErro.Visible = false;

            }
            catch (Exception erro)
            {
                lbErro.Text = "" + erro.Message;
                lbErro.CssClass = "alert alert-danger";
                lbErro.Visible = true;
            }
            //gvAlertas.UpdateMethod = "UPDATE Anomalias SET hora_inicial=@hora_inicial,hora_final=@hora_final,tipo_evento=@tipo_evento WHERE id=@id";
         

        }

        private void gvAlertas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
           gvAlertas.EditIndex = -1;
            atualizargv();
            lbErro.Visible = false;
        }

        private void gvAlertas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int linha = e.NewEditIndex;
            gvAlertas.EditIndex = linha;
            lbErro.Visible = false;
            atualizargv();
        }

        private void atualizargv()
        {
            DataTable dados = BaseDados.Instance.devolveAnomalias();

            gvAlertas.DataSource = dados;
            gvAlertas.AutoGenerateColumns = true;
            gvAlertas.AutoGenerateEditButton = true;
            

            
                gvAlertas.DataBind();

        }
    }
}