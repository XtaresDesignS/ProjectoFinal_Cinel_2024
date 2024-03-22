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
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            if (string.IsNullOrEmpty(token))
            {
                lbl_mensagemAtivar.Text = "Token não fornecido";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
                return;
            }

            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString))
            {
                myConn.Open();
                using (SqlCommand command = new SqlCommand("SELECT id_Estado, DataExpiracaoToken FROM dbo.Utilizadores WHERE TokenAtivacao = @token", myConn))
                {
                    command.Parameters.AddWithValue("@token", new Guid(token));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id_Estado = reader.GetInt32(0);
                            bool ativo = id_Estado != 0 ? true : false;
                            DateTime dataExpiracaoToken = reader.GetDateTime(1);

                            if (ativo)
                            {
                                lbl_mensagemAtivar.Text = "Esta conta já foi ativada.";
                                btn_PedirAtivacao.Visible = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
                            }
                            else if (dataExpiracaoToken < DateTime.Now)
                            {
                                lbl_mensagemAtivar.Text = "O tempo de ativação expirou. Peça nova ativação no botão abaixo !!!";
                                btn_PedirAtivacao.Visible = true;
                            }
                            else
                            {
                                AtivarConta(new Guid(token));
                                lbl_mensagemAtivar.Text = "A sua conta foi ativada com sucesso!";
                                btn_PedirAtivacao.Visible = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
                            }
                        }
                        else
                        {
                            lbl_mensagemAtivar.Text = "Entrada inválida.";
                            btn_PedirAtivacao.Visible = false;
                            ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
                        }
                    }
                }
            }
        }

        private void AtivarConta(Guid token)
        {
            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString))
            {
                myConn.Open();
                SqlTransaction transaction = myConn.BeginTransaction();
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT id_Estado FROM dbo.Utilizadores WHERE TokenAtivacao = @token", myConn, transaction))
                    {
                        command.Parameters.AddWithValue("@token", token);
                        bool? ativo = command.ExecuteScalar() as bool?;

                        if (ativo == null)
                        {
                            throw new Exception("Token inválido");
                        }
                        else if (ativo == true)
                        {
                            throw new Exception("Conta já ativada");
                        }
                        else
                        {
                            using (SqlCommand updateCommand = new SqlCommand("UPDATE dbo.Utilizadores SET id_Estado = 1, TokenAtivacao = NULL WHERE TokenAtivacao = @token", myConn, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@token", token);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lbl_mensagemAtivar.Text = ex.Message;
                }
            }
        }


        protected void btn_PedirAtivacao_Click(object sender, EventArgs e)
        {
            // Obter o token a partir da URL e desencriptá-lo
            var token = Request.QueryString["token"];

            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString))
            {
                myConn.Open();
                using (SqlCommand command = new SqlCommand("ReativarConta", myConn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TokenAtivacao", new Guid(token));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var novoToken = reader.GetGuid(0);
                            var novaDataExpiracao = reader.GetDateTime(1);
                            var email = reader.GetString(2);

                            if (novoToken != Guid.Empty)
                            {
                                var master = this.Master as MainLayout;
                                // Enviar o email
                                master.EnviaEmail(email, novoToken.ToString());

                                lbl_mensagemAtivar.Text = "Um novo token de ativação foi enviado para o seu e-mail.";
                                btn_PedirAtivacao.Visible = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='Inicio.aspx' }, 3000);", true);
                            }
                            else
                            {
                                lbl_mensagemAtivar.Text = "Não foi possível a reativação da sua conta. Por favor entre em contato por email: geral@coinclub.pt";
                                btn_PedirAtivacao.Visible = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='Inicio.aspx' }, 3000);", true);
                            }
                        }
                        else
                        {
                            lbl_mensagemAtivar.Text = "Entrada inválida.";
                            ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", "setTimeout(function(){ window.location='SignIn.aspx' }, 2000);", true);
                        }
                    }
                }
            }
        }
    }
}