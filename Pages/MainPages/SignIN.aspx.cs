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

        //protected void btn_signin_Click(object sender, EventArgs e)
        //{
        //    string nomeUtilizador = tb_utilizador.Text;
        //    string senha = tb_pw.Text;
        //    LoginUsuario(nomeUtilizador, senha);
        //}

        //public void LoginUsuario(string nomeUtilizador, string senha)
        //{
        //    var master = this.Master as MainLayout;
        //    EncriptDesencript passEncDEnc = new EncriptDesencript();
        //    string nomeUtilizador = tb_nome_utilizador.Text;
        //    string pass = passEncDEnc.Encriptar(tb_pass_utilizador.Text);
        //    using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NumismaticaConnectionString"].ConnectionString))
        //    {
        //        using (SqlCommand myCommand = new SqlCommand("AuthenticaUser", myConn))
        //        {
        //            myCommand.CommandType = CommandType.StoredProcedure;
        //            myCommand.Parameters.AddWithValue("@Nome_Utilizador", nomeUtilizador);
        //            myCommand.Parameters.AddWithValue("@Pass_Utilizador", pass);
        //            SqlParameter returnParameter = myCommand.Parameters.Add("@Retorno", SqlDbType.VarChar, 500);
        //            returnParameter.Direction = ParameterDirection.Output;
        //            myConn.Open();
        //            myCommand.ExecuteNonQuery();
        //            string result = returnParameter.Value.ToString();
        //            if (result == "Sucesso")
        //            {
        //                using (SqlDataReader reader = myCommand.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {   // Armazena todas as informações do usuário na sessão aqui
        //                        Session["id_Utilizador"] = reader["id_Utilizador"].ToString();
        //                        Session["Nome_Utilizador"] = reader["Nome_Utilizador"].ToString();
        //                        Session["Img_utilizador"] = ((byte[])reader["Foto_Utilizador"]).Length > 0 ? $"data:image/PNG;base64,{Convert.ToBase64String((byte[])reader["Foto_Utilizador"])}" : $@"https://picsum.photos/200";
        //                        Session["logado"] = reader["Nome_Perfil"].ToString();
        //                    }
        //                }
        //                Response.Redirect("/MainPages/Landing.aspx");
        //                // Chama o método
        //                master.CarregaXML();
        //            }
        //            else
        //            {
        //                lb_Mensagem.Text = result;
        //            }
        //        }
        //    }
        //}
        //}
    }
}