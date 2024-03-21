using ProjectoFinal_Cinel_2024.Assets.WebServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages.MainPages
{
    public partial class SingUP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void RegistarUtilizador()
        {
            EncriptDesencript pass = new EncriptDesencript();

            //Conversão independente do ficheiro de imagem
            Stream imgStream = fu_image.PostedFile.InputStream;
            int tamanhoFicheiro = fu_image.PostedFile.ContentLength;
            byte[] imgBinary = new byte[tamanhoFicheiro];
            imgStream.Read(imgBinary, 0, tamanhoFicheiro);

            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString))
            using (SqlCommand myCommand = new SqlCommand("RegistarUtilizador", myConn) { CommandType = CommandType.StoredProcedure })
            {
                myCommand.Parameters.AddWithValue("@Nome_Utilizador", tb_nome.Text);
                myCommand.Parameters.AddWithValue("@Email_Utilizado", tb_email.Text);
                myCommand.Parameters.AddWithValue("@NIF", tb_nif.Text);
                myCommand.Parameters.AddWithValue("@Pass_Utilizador", pass.Encriptar(tb_pw.Text));
                myCommand.Parameters.AddWithValue("@Foto_Utilizador", imgBinary);

                SqlParameter valor = new SqlParameter("@Retorno", SqlDbType.Int) { Direction = ParameterDirection.Output };
                myCommand.Parameters.Add(valor);

                // Adicionar o parâmetro de saída para o token de ativação
                SqlParameter tokenAtivacao = new SqlParameter("@TokenAtivacao", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output };
                myCommand.Parameters.Add(tokenAtivacao);

                // Adicionar o parâmetro de saída para a data de expiração do token
                SqlParameter dataExpiracaoToken = new SqlParameter("@DataExpiracaoToken", SqlDbType.DateTime) { Direction = ParameterDirection.Output };
                myCommand.Parameters.Add(dataExpiracaoToken);

                myConn.Open();
                myCommand.ExecuteNonQuery();

                int respostaSP = Convert.ToInt32(myCommand.Parameters["@Retorno"].Value);

                lbl_mensagem.Text = respostaSP == 1 ? "Utilizador inserido com sucesso !!!\n\nVerifique o seu e-mail e ative a sua conta !!!" : "Utilizador já existe !!!\n\nTente outro e-mail por favor !!!";

                // Se o registo foi bem-sucedido, enviar o email de ativação
                if (respostaSP == 1)
                {
                    string token = tokenAtivacao.Value.ToString();
                    var master = this.Master as MainLayout;
                    master.EnviaEmail(tb_email.Text, token);
                    ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='Inicio.aspx' }, 3000);", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignUp.aspx' }, 1500);", true);
            }
        }

        protected void btn_registarUser_Click(object sender, EventArgs e)
        {
            RegistarUtilizador();
        }
    }
}