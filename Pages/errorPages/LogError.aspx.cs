﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages.errorPages
{
    public partial class LogError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LinkLog.HRef = (
                 Session["logado"] == null || String.IsNullOrEmpty(Session["logado"].ToString()) ?
                  @"/Pages/MainPages/SignIN.aspx" : 
                  Session["logado"] != null && Session["logado"].ToString() == "Formando" ?
                  @"/Pages/MainPages/Landing.aspx" :
                  "#"
                  );
        }
    }
}