using ProjectoFinal_Cinel_2024.Assets.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages.MainPages
{
    public partial class Perfil_User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["U"]))
            {
               string User = Request.QueryString["U"];

                ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "alert(" +
                    $"'Por agora vamos voltar, já agora o id de utilizadorGoogle é {User} ')" +
                    ",setTimeout(function(){ window.location='Landing.aspx' }, 2000);", true);
            }
        }
    }
}