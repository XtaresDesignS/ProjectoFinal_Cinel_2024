using ProjectoFinal_Cinel_2024.Assets.WebServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages
{
    public partial class alteraPW : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {                
            if (string.IsNullOrEmpty((string)Session["logado"]))
            {
                tb_pass_Antiga.Enabled = false;
                tb_pass_nova.Enabled = false;
                tb_Confirma_Pass.Enabled = false;
                lb_Mensagem.Text = "Deve efectuar o Login para poder utilizar esta página";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", " alert('Deve efectuar o Login para poder utilizar esta página')," +
                    "setTimeout(function(){" +
                    $@"window.location='/Pages/MainPages/SignIN.aspx'" +
                    " }, 2000);", true);
            }   
            if(!string.IsNullOrEmpty(Request.QueryString["pwG"]))
            {
                EncriptDesencript passEncDEnc = new EncriptDesencript();
                tb_pass_Antiga.Text = (string)Session["pw"];
                tb_pass_Antiga.Enabled = false;
                lb_Mensagem.Text = "Porfavor altere a sua password";
            }
                   
        }

        protected void btn_altera_Click(object sender, EventArgs e)
        {
            EncriptDesencript passEncDEnc = new EncriptDesencript();
            string pass_actual = passEncDEnc.Encriptar(tb_pass_Antiga.Text);
            string pass_nova = passEncDEnc.Encriptar(tb_Confirma_Pass.Text);
            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NumismaticaConnectionString"].ConnectionString))
            {
                myConn.Open();

                SqlCommand command = new SqlCommand("AtualizarPalavraPasse", myConn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_utilizador", int.Parse(Session["id_Utilizador"].ToString()));
                command.Parameters.AddWithValue("@Palavra_Passe_Atual", pass_actual);
                command.Parameters.AddWithValue("@nova_Palavra_Passe", pass_nova);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    lb_Mensagem.Text = reader["Message"].ToString();
                }

            }
        }
    }
}