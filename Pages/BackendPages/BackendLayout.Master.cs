using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages.BackendPages
{
    public partial class BackendLayout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logado"] != null && !String.IsNullOrEmpty(Session["logado"].ToString()))
            {
                if (Session["logado"].ToString() == "Formando")
                {
                    Response.Redirect(@"\Pages\errorPages\LogError.aspx");
                }
            }
            else
            {
                Response.Redirect(@"\Pages\errorPages\LogError.aspx");
            }
        }
    }
}