using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPA
{
    public partial class Perfil_redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Genericas.fBrowserIsMobile() == true)
            {
                Response.Redirect("Perfil_mob.aspx");
            }
            else
            {
                Response.Redirect("Perfil.aspx");
            }
        }
    }
}