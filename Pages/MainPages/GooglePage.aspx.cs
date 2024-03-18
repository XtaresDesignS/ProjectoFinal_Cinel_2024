using ProjectoFinal_Cinel_2024.Assets.WebServices;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages
{
    public partial class GooglePage : System.Web.UI.Page
    {
        private static readonly ClientSecrets secrets = new ClientSecrets
        {

            //apanha do WebCondig os dados necessários para o request da authenticação
            ClientId = ConfigurationManager.AppSettings["GoogleClientId"],
            ClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"]
        };

        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = secrets,
                Scopes = new[] { "https://www.googleapis.com/auth/userinfo.email", "https://www.googleapis.com/auth/userinfo.profile" },
                DataStore = new FileDataStore("Google.Apis.Auth")
            });

        protected void Page_Load(object sender, EventArgs e)
        {
            //Apanaha do Webconfig o URL de redirecionamento para ativação e validação da autenticação
            var uri = ConfigurationManager.AppSettings["URIApp"];
            var code = Request.QueryString["code"];

            if (string.IsNullOrEmpty(code))
            {
                var authUrl = $"{flow.CreateAuthorizationCodeRequest(uri).Build()}";
                Response.Redirect(authUrl);
            }
            else
            {
                var token = flow.ExchangeCodeForTokenAsync("user", code, uri, CancellationToken.None).Result;
                var infoRequest = new Google.Apis.Oauth2.v2.Oauth2Service(new Google.Apis.Services.BaseClientService.Initializer
                {
                    HttpClientInitializer = GoogleCredential.FromAccessToken(token.AccessToken),
                    ApplicationName = "My ASP.NET Application"
                }).Userinfo.Get();

                var profile = infoRequest.Execute();
                string nomeUtilizador = profile.Name;
                string email = profile.Email;
                string imageUrl = profile.Picture;
                InfosUtilizador(nomeUtilizador, email, imageUrl);

            }
        }

        bool v = false;

        private void InfosUtilizador(string nomeUtilizador, string email, string imageUrl)
        {
            // Obtém a página mestra atual como um objeto Layout
            var master = this.Master as MainLayout;
            // Cria um novo objeto EncripDesencript
            EncriptDesencript passEncDEnc = new EncriptDesencript();
            // Gera uma nova senha
            string passAuto = master.GerarNovaPalavraPasse();

            byte[] imgBinary = null;
            string imgType;

            if (string.IsNullOrEmpty(email))
            {
                email = $"{nomeUtilizador}t@email.com"; // Substitua por um valor padrão adequado
            }


            // Cria uma solicitação web para a URL da foto
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
            // Obtém a resposta da solicitação web
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Copia o fluxo de resposta para um MemoryStream
                    using (MemoryStream ms = new MemoryStream())
                    {
                        responseStream.CopyTo(ms);
                        // Converte o MemoryStream num array de bytes
                        imgBinary = ms.ToArray();
                    }
                }
                imgType = response.ContentType;
            }
            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NumismaticaConnectionString"].ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("VerificaUtilizador", myConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Nome_Utilizador", nomeUtilizador);
                    myCommand.Parameters.AddWithValue("@Email_Utilizador", email);
                    // Cria um novo parâmetro de retorno
                    SqlParameter returnParameter = myCommand.Parameters.Add("@Retorno", SqlDbType.VarChar, 500);
                    returnParameter.Direction = ParameterDirection.Output;
                    SqlParameter passwordParameter = myCommand.Parameters.Add("@Password_Utilizador", SqlDbType.VarChar, 500);
                    passwordParameter.Direction = ParameterDirection.Output;
                    SqlParameter idParameter = myCommand.Parameters.Add("@Id_Utilizador", SqlDbType.Int);
                    idParameter.Direction = ParameterDirection.Output;

                    myConn.Open();
                    myCommand.ExecuteNonQuery();
                    string result = returnParameter.Value.ToString();
                    if (result == "Utilizador Não Existe")
                    {
                        Session["pw"] = passAuto;
                        RegistaUtilizador(nomeUtilizador, email, passEncDEnc.Encriptar(passAuto.ToUpper()), imgBinary, imgType);
                    }
                    else
                    {
                        string password = passwordParameter.Value.ToString();
                        Session["pw"] = password;
                        int id = (int)idParameter.Value;
                        FazLogin(nomeUtilizador, password, imgBinary, imgType, id);
                    }
                }
            }
        }

        // Método para registar um novo utilizador
        private void RegistaUtilizador(string nomeUtilizador, string email, string passAuto, byte[] imgBinary, string imgType)
        {
            // Obtém a página mestra atual como um objeto Layout
            var master = this.Master as MainLayout;
            // Cria um novo objeto EncripDesencript
            EncriptDesencript passEncDEnc = new EncriptDesencript();
            // Inicializa a variável resultado
            int resultado = 0;
            // Cria uma nova conexão SQL
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NumismaticaConnectionString"].ConnectionString))
            {
                // Cria um novo comando SQL
                using (SqlCommand mycommand = new SqlCommand("RegistaUtilizador", connection))
                {
                    // Abre a conexão
                    connection.Open();
                    // Define o tipo de comando como StoredProcedure
                    mycommand.CommandType = CommandType.StoredProcedure;
                    // Adiciona os parâmetros necessários ao comando
                    mycommand.Parameters.AddWithValue("@Nome_utilizador", nomeUtilizador);
                    mycommand.Parameters.AddWithValue("@Email_Utilizador", email);
                    mycommand.Parameters.AddWithValue("@Pass_Utilizador", passAuto); // Aqui precisas fornecer a senha do utilizador
                    mycommand.Parameters.AddWithValue("@imagem_utilizador", imgBinary);
                    mycommand.Parameters.AddWithValue("@estado_conta", 1);
                    // Cria um novo parâmetro para o ID do utilizador
                    SqlParameter userId = mycommand.Parameters.Add("@Id_Utilizador", SqlDbType.Int);
                    userId.Direction = ParameterDirection.Output;
                    // Executa o comando
                    mycommand.ExecuteNonQuery();
                    // Obtém o resultado do comando

                    using (SqlDataReader reader = mycommand.ExecuteReader())
                    {
                        // Se houver dados para ler
                        if (reader.Read())
                        {
                            resultado = (int)reader["Resultado"];
                        }
                    }
                    // Armazena o ID do utilizador na sessão
                    Session["id_Utilizador"] = userId;
                    // Armazena o nome do utilizador na sessão
                    Session["Nome_Utilizador"] = nomeUtilizador.ToString();
                    // Armazena a imagem do utilizador na sessão
                    Session["Img_utilizador"] = ((byte[])imgBinary).Length > 0 ? $"data:image/{imgType};base64,{Convert.ToBase64String((byte[])imgBinary)}" : $@"https://picsum.photos/200";
                }
                // Fecha a conexão
                connection.Close();
            }
            // Se o resultado for 1
            if (resultado == 1)
            {
                v = true;
                // Define o estado da sessão como "Guest"
                Session["logado"] = "Guest";
                // Redireciona o utilizador para a página de alteração de senha

                // Carrega o XML
                FazLogin(nomeUtilizador, passAuto, imgBinary, imgType, (int)Session["id_Utilizador"]);
            }
        }

        // Método para fazer login do utilizador
        private void FazLogin(string nomeUtilizador, string passAuto, byte[] imgBinary, string imgType, int id)
        {
            // Obtém a página mestra atual como um objeto Layout
            var master = this.Master as MainLayout;
            // Cria uma nova conexão SQL
            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NumismaticaConnectionString"].ConnectionString))
            {
                // Cria um novo comando SQL
                using (SqlCommand myCommand = new SqlCommand("VerificaLogin", myConn))
                {
                    // Define o tipo de comando como StoredProcedure
                    myCommand.CommandType = CommandType.StoredProcedure;
                    // Adiciona os parâmetros necessários ao comando
                    myCommand.Parameters.AddWithValue("@Nome_Utilizador", nomeUtilizador);
                    myCommand.Parameters.AddWithValue("@Pass_Utilizador", passAuto);
                    // Cria um novo parâmetro de retorno
                    SqlParameter returnParameter = myCommand.Parameters.Add("@Retorno", SqlDbType.VarChar, 500);
                    returnParameter.Direction = ParameterDirection.Output;
                    // Abre a conexão
                    myConn.Open();
                    // Executa o comando
                    myCommand.ExecuteNonQuery();
                    // Obtém o resultado do comando
                    string result = returnParameter.Value.ToString();
                    // Se o resultado for "Sucesso"
                    if (result == "Sucesso")
                    {
                        // Lê os dados do comando
                        using (SqlDataReader reader = myCommand.ExecuteReader())
                        {
                            // Se houver dados para ler
                            if (reader.Read())
                            {
                                // Armazena o ID do utilizador na sessão
                                Session["id_Utilizador"] = id.ToString();
                                // Armazena o nome do utilizador na sessão
                                Session["Nome_Utilizador"] = reader["Nome_Utilizador"].ToString();
                                // Armazena a imagem do utilizador na sessão
                                Session["Img_utilizador"] = ((byte[])imgBinary).Length > 0 ? $"data:image/{imgType};base64,{Convert.ToBase64String((byte[])imgBinary)}" : $@"https://picsum.photos/200";
                                // Armazena o perfil do utilizador na sessão
                                Session["logado"] = reader["Nome_Perfil"].ToString();
                            }
                        }
                        if (v)
                        {

                            // Redireciona o utilizador para a página Montra
                            Response.Redirect($"/Pages/alterar.aspx?altPw={passAuto}");
                        }
                        else
                        {

                            Response.Redirect($"/Pages/Landing.aspx");
                        }
                        // Carrega o XML
                        master.CarregaXML();
                    }

                }
            }
        }

    }
}