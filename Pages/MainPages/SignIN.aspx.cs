using ProjectoFinal_Cinel_2024.Assets.WebServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages.MainPages
{
    public partial class SignIN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            string emailUtilizador = tb_email_utilizador.Text;
            string senha = tb_pass_utilizador.Text;
            LoginUsuario(emailUtilizador, senha);
        }

        public void LoginUsuario(string utilizador, string senha)
        {
            var master = this.Master as MainLayout;
            EncriptDesencript passEncDEnc = new EncriptDesencript();
            string pass = passEncDEnc.Encriptar(senha);
            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("AuthenticaUser", myConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Email_Utilizado", utilizador);
                    myCommand.Parameters.AddWithValue("@Pass_Utilizador", pass);
                    SqlParameter returnParameter = myCommand.Parameters.Add("@Retorno", SqlDbType.Int);
                    SqlParameter messageParameter = myCommand.Parameters.Add("@Message", SqlDbType.VarChar, 255);
                    returnParameter.Direction = ParameterDirection.Output;
                    messageParameter.Direction = ParameterDirection.Output;
                    myConn.Open();
                    using (SqlDataReader reader = myCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Armazena todas as informações do usuário na sessão aqui
                            Session["id_Utilizador"] = reader["id_Utilizador"].ToString();
                            Session["Nome_Utilizador"] = reader["Nome_Utilizador"].ToString();
                            Session["Img_utilizador"] = ((byte[])reader["Foto_Utilizador"]).Length > 0 ? $"data:image/PNG;base64,{Convert.ToBase64String((byte[])reader["Foto_Utilizador"])}" : $@"https://picsum.photos/200";
                            Session["logado"] = reader["Perfis"].ToString();
                        }
                    }
                    int result = (int)returnParameter.Value;
                    string message = (string)messageParameter.Value;
                    if (result == 1)
                    {
                        Response.Redirect("/Pages/MainPages/Landing.aspx");
                        // Chama o método
                        Session["pw"] = senha;
                        master.CarregaXML();
                    }
                    else
                    {
                        lb_Mensagem.Text = message;
                    }
                }
            }
        }



    }
}