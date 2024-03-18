using System;
using System.Collections.Generic;
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
        //    //Chama o webservice de encriptação e desencriptação
        //    EncriptDesencript passEncDenc = new EncriptDesencript();
        //    //chama eventos da master
        //    var master = this.Master as MainLayout;

        //    lbl_mensagem.Text = "";
        //    using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CasaDasMoedas_DBConnectionString"].ConnectionString))
        //    {
        //        using (SqlCommand myCommand = new SqlCommand("GetUtilizadoresData", myConn))
        //        {
        //            myCommand.CommandType = CommandType.StoredProcedure;
        //            myCommand.Parameters.AddWithValue("@Nome_Utilizador", nomeUtilizador);
        //            myCommand.Parameters.AddWithValue("@Pass_Utilizador", passEncDenc.Encriptar(senha));
        //            myCommand.Parameters.Add("@Retorno", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            myCommand.Parameters.Add("@Ativo", SqlDbType.Bit).Direction = ParameterDirection.Output;
        //            myCommand.Parameters.Add("@Foto", SqlDbType.VarBinary, -1).Direction = ParameterDirection.Output;
        //            myCommand.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            myCommand.Parameters.Add("@Perfil_Nome", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output; // Especifique o tamanho aqui
        //            myCommand.Parameters.Add("@Perfil_id", SqlDbType.Int).Direction = ParameterDirection.Output;


        //            myConn.Open();
        //            myCommand.ExecuteNonQuery();

        //            bool logado = false;
        //            Session["logado"] = logado;
        //            int respostaSP = Convert.ToInt32(myCommand.Parameters["@Retorno"].Value);

        //            if (respostaSP == 1)
        //            {
        //                bool ativo = Convert.ToBoolean(myCommand.Parameters["@Ativo"].Value);
        //                byte[] foto = (byte[])myCommand.Parameters["@Foto"].Value;
        //                int id = (int)myCommand.Parameters["@Id"].Value;
        //                int perfil_id = (int)myCommand.Parameters["@Perfil_Id"].Value;
        //                string perfil_Nome = (string)myCommand.Parameters["@Perfil_Nome"].Value;

        //                if (ativo == false)
        //                {
        //                    lbl_mensagem.Text = "Por favor, confira o seu email e ative sua conta !!!";
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
        //                }
        //                else
        //                {
        //                    // Armazene todas as informações do usuário na sessão aqui
        //                    Session["Pass_Utilizador"] = tb_pw.Text;
        //                    Session["id_Utilizador"] = id;
        //                    Session["Nome_Utilizador"] = tb_utilizador.Text;
        //                    Session["ativo"] = ativo;
        //                    string imgDataURL = "data:image/jpg;base64," + Convert.ToBase64String(foto);
        //                    Session["Img_utilizador"] = imgDataURL;
        //                    Session["logado"] = perfil_Nome;

        //                    // Define-se logado como true após um login bem-sucedido
        //                    Session["logado"] = true;

        //                    Response.Redirect("Inicio.aspx");


        //                }
        //                master.CarregaXML();
        //            }
        //            else
        //            {
        //                lbl_mensagem.Text = "Utilizador e/ou palavra-passe errados !!!";
        //                Session["logado"] = false;
        //                ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
        //            }

        //        }
        //    }
        //}
    }
}