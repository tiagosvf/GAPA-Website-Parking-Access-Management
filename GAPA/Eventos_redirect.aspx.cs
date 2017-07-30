using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Eventos_redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Genericas.fBrowserIsMobile() == true)
            {
                Response.Redirect("Eventos_g_mobile.aspx");
            }
            else
            {
                Response.Redirect("Eventos_G.aspx");
            }
        }
    }
}