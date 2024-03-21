using ProjectoFinal_Cinel_2024.Assets.WebServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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

        protected void btn_registarUser_Click(object sender, EventArgs e)
        {
            //utiliza a encriptação no webservice
            EncriptDesencript passencdenc = new EncriptDesencript();
            //chama eventos da Master
            var master = this.Master as MainLayout;

            //Conversão independente do ficheiro de imagem
            Stream imgStream = fu_image.PostedFile.InputStream;
            int tamanhoFicheiro = fu_image.PostedFile.ContentLength;
            byte[] imgBinary = new byte[tamanhoFicheiro];
            imgStream.Read(imgBinary, 0, tamanhoFicheiro);

            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString))
            using (SqlCommand myCommand = new SqlCommand("RegistarUtilizador", myConn))
            {
                myConn.Open();
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@Nome_Utilizador", tb_nome.Text);
                myCommand.Parameters.AddWithValue("@Email_Utilizado", tb_email.Text);
                myCommand.Parameters.AddWithValue("@Pass_Utilizador", passencdenc.Encriptar(tb_pw.Text));
                myCommand.Parameters.AddWithValue("@Foto_Utilizador", imgBinary);

                SqlParameter valor = new SqlParameter("@Retorno", SqlDbType.Int) { Direction = ParameterDirection.Output };
                myCommand.Parameters.Add(valor);
                int respostaSP = Convert.ToInt32(myCommand.Parameters["@Retorno"].Value);

                // Se o registo foi bem-sucedido, enviar o email de ativação
                if (respostaSP == 1)
                {
                    string token = string.Empty;
                    int idUser = 0;

                    SqlDataReader reader = myCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        token = reader["@TokenAtivacao"].ToString();
                        idUser = (int)reader["@Id_Utilizador"];
                    }
                    myConn.Close();
                    master.EnviaEmail(tb_email.Text, token);
                }
            }
        }
    }
}