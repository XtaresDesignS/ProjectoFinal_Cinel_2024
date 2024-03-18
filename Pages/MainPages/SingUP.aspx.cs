using System;
using System.Collections.Generic;
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
        //protected void btn_Regist_Click(object sender, EventArgs e)
        //{

        //    //chama eventos da Master
        //    var master = this.Master as MainLayout;

        //    //utiliza a encriptação no webservice
        //    EncriptDesencript passencdenc = new EncriptDesencript();

        //    byte[] imgBinaryData;

        //    // Verificar se o usuário forneceu uma imagem
        //    if (fu_image.PostedFile.ContentLength > 0)
        //    {
        //        imgBinaryData = new byte[fu_image.PostedFile.ContentLength];
        //        fu_image.PostedFile.InputStream.Read(imgBinaryData, 0, fu_image.PostedFile.ContentLength);
        //    }
        //    else
        //    {
        //        // Se o usuário não forneceu uma imagem, use uma imagem padrão
        //        string imagePath = Server.MapPath("~/Images/profile.png");
        //        imgBinaryData = File.ReadAllBytes(imagePath);
        //    }

        //    using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString))
        //    using (SqlCommand myCommand = new SqlCommand("RegistarUtilizador", myConn) { CommandType = CommandType.StoredProcedure })
        //    {
        //        myCommand.Parameters.AddWithValue("@Nome_Utilizador", tb_nome.Text);
        //        myCommand.Parameters.AddWithValue("@Email_Utilizado", tb_email.Text);
        //        myCommand.Parameters.AddWithValue("@Pass_Utilizador", passencdenc.Encriptar(tb_pw.Text));
        //        myCommand.Parameters.AddWithValue("@Foto_Utilizador", imgBinaryData);

        //        SqlParameter valor = new SqlParameter("@Retorno", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //        myCommand.Parameters.Add(valor);

        //        // Adicionar o parâmetro de saída para o token de ativação
        //        SqlParameter tokenAtivacao = new SqlParameter("@TokenAtivacao", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output };
        //        myCommand.Parameters.Add(tokenAtivacao);

        //        // Adicionar o parâmetro de saída para a data de expiração do token
        //        SqlParameter dataExpiracaoToken = new SqlParameter("@DataExpiracaoToken", SqlDbType.DateTime) { Direction = ParameterDirection.Output };
        //        myCommand.Parameters.Add(dataExpiracaoToken);

        //        myConn.Open();
        //        myCommand.ExecuteNonQuery();

        //        int respostaSP = Convert.ToInt32(myCommand.Parameters["@Retorno"].Value);

        //        lbl_mensagem.Text = respostaSP == 1 ? "Utilizador inserido com sucesso !!!\n\nVerifique o seu e-mail e ative a sua conta !!!" : "Utilizador já existe !!!\n\nTente outro e-mail por favor !!!";

        //        // Se o registo foi bem-sucedido, enviar o email de ativação
        //        if (respostaSP == 1)
        //        {
        //            string token = tokenAtivacao.Value.ToString();
        //            master.EnviaEmail(tb_email.Text, token);
        //            ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='Inicio.aspx' }, 3000);", true);
        //        }
        //        else
        //            ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignUp.aspx' }, 1500);", true);
        //    }

        //}
    }
}