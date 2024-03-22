using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages.MainPages
{
    public partial class ativar : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    string token = Request.QueryString["token"];
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        lbl_mensagemAtivar.Text = "Token não fornecido";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
        //        return;
        //    }

        //    using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CasaDasMoedas_DBConnectionString"].ConnectionString))
        //    {
        //        myConn.Open();
        //        using (SqlCommand command = new SqlCommand("SELECT ativo, DataExpiracaoToken FROM dbo.Utilizadores WHERE TokenAtivacao = @token", myConn))
        //        {
        //            command.Parameters.AddWithValue("@token", new Guid(token));
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    bool ativo = reader.GetBoolean(0);
        //                    DateTime dataExpiracaoToken = reader.GetDateTime(1);


        //                    if (ativo)
        //                    {
        //                        lbl_mensagemAtivar.Text = "Esta conta já foi ativada.";
        //                        btn_PedirAtivacao.Visible = false;
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
        //                    }
        //                    else if (dataExpiracaoToken < DateTime.Now)
        //                    {
        //                        lbl_mensagemAtivar.Text = "O tempo de ativação expirou. Peça nova ativação no botão abaixo !!!";
        //                        btn_PedirAtivacao.Visible = true;
        //                    }
        //                    else
        //                    {
        //                        AtivarConta(new Guid(token));
        //                        lbl_mensagemAtivar.Text = "A sua conta foi ativada com sucesso!";
        //                        btn_PedirAtivacao.Visible = false;
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
        //                    }
        //                }
        //                else
        //                {
        //                    lbl_mensagemAtivar.Text = "Entrada inválida.";
        //                    btn_PedirAtivacao.Visible = false;
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
        //                }
        //            }
        //        }
        //    }
        //}

        //private void AtivarConta(Guid token)
        //{
        //    using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CasaDasMoedas_DBConnectionString"].ConnectionString))
        //    {
        //        myConn.Open();
        //        using (SqlCommand command = new SqlCommand("AtivarConta", myConn))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@token", token);
        //            try
        //            {
        //                command.ExecuteNonQuery();
        //            }
        //            catch (SqlException ex)
        //            {
        //                lbl_mensagemAtivar.Text = ex.Message;
        //            }
        //        }
        //    }
        //}

        //protected void btn_PedirAtivacao_Click(object sender, EventArgs e)
        //{
        //    // Obter o token a partir da URL e desencriptá-lo
        //    var token = Request.QueryString["token"];

        //    using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CasaDasMoedas_DBConnectionString"].ConnectionString))
        //    {
        //        myConn.Open();
        //        using (SqlCommand command = new SqlCommand("ReativarConta", myConn))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@TokenAtivacao", new Guid(token));
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    var novoToken = reader.GetGuid(0);
        //                    var novaDataExpiracao = reader.GetDateTime(1);
        //                    var email = reader.GetString(2);

        //                    if (novoToken != Guid.Empty)
        //                    {
        //                        // Enviar o email
        //                        EnviaEmail(email, novoToken.ToString());

        //                        lbl_mensagemAtivar.Text = "Um novo token de ativação foi enviado para o seu e-mail.";
        //                        btn_PedirAtivacao.Visible = false;
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='Inicio.aspx' }, 3000);", true);
        //                    }
        //                    else
        //                    {
        //                        lbl_mensagemAtivar.Text = "Não foi possível a reativação da sua conta. Por favor entre em contato por email: geral@coinclub.pt";
        //                        btn_PedirAtivacao.Visible = false;
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='Inicio.aspx' }, 3000);", true);
        //                    }
        //                }
        //                else
        //                {
        //                    lbl_mensagemAtivar.Text = "Entrada inválida.";
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}