

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using ProjectoFinal_Cinel_2024.Assets.WebServices;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.UI;

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
            var uri = ConfigurationManager.AppSettings["URIApp"].ToString();
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

       
        private void InfosUtilizador(string nomeUtilizador, string email, string imageUrl)
        {
            bool v = false;
            var master = this.Master as MainLayout;
            EncriptDesencript passEncDEnc = new EncriptDesencript();
            string passAuto = master.GerarNovaPalavraPasse();
            string password = passEncDEnc.Encriptar(passAuto);
            byte[] imgBinary = null;
            string imgType;
            if (string.IsNullOrEmpty(email))
            {
                email = $"{nomeUtilizador}t@email.com"; 
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        responseStream.CopyTo(ms);
                        imgBinary = ms.ToArray();
                    }
                }
                imgType = response.ContentType;
            }
            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("VerificaUtilizador", myConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Nome_Utilizador", nomeUtilizador);
                    myCommand.Parameters.AddWithValue("@Email_Utilizador", email);
                    SqlParameter returnParameter = myCommand.Parameters.Add("@Retorno", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.Output;            
                    SqlParameter passwordParameter = myCommand.Parameters.Add("@Password_Utilizador", SqlDbType.VarChar, 500);
                    passwordParameter.Direction = ParameterDirection.Output;
                    SqlParameter idParameter = myCommand.Parameters.Add("@Id_Utilizador", SqlDbType.Int);
                    idParameter.Direction = ParameterDirection.Output;
                    myConn.Open();
                    myCommand.ExecuteNonQuery();
                    int result = (int)returnParameter.Value;
                    
                    if (result != 1)
                    {
                        v = true;
                        Session["pw"] = password;
                        RegistaUtilizador(nomeUtilizador, email, password, imgBinary, imgType);                   
                    }
                    else
                    {
                        v = false;
                        string user = nomeUtilizador;
                        string eml = email;
                        string pass =  passwordParameter.Value.ToString();
                        Session["pw"] = passEncDEnc.Desencriptar((string)pass);
                        int id = (int)idParameter.Value;
                        FazLogin(eml, pass, imgBinary, imgType, id, v);
                    }
                    myConn.Close();
                }
            }
        }

        // Método para registar um novo utilizador
        private void RegistaUtilizador(string nomeUtilizador, string email, string passAuto, byte[] imgBinary, string imgType)
        {
            var master = this.Master as MainLayout;
            int resultado = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString))
            {
                using (SqlCommand mycommand = new SqlCommand("RegistaUtilizadorGoogle", connection))
                {
                    connection.Open();
                    mycommand.CommandType = CommandType.StoredProcedure;
                    mycommand.Parameters.AddWithValue("@Nome_utilizador", nomeUtilizador);
                    mycommand.Parameters.AddWithValue("@Email_Utilizador", email);
                    mycommand.Parameters.AddWithValue("@Pass_Utilizador", passAuto);
                    mycommand.Parameters.AddWithValue("@imagem_utilizador", imgBinary);
                    mycommand.Parameters.AddWithValue("@estado_conta", 1);
                    SqlParameter userId = mycommand.Parameters.Add("@Id_Utilizador", SqlDbType.Int);
                    userId.Direction = ParameterDirection.Output;
                    mycommand.ExecuteNonQuery();
                    using (SqlDataReader reader = mycommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resultado = (int)reader["Resultado"];

                        }
                    }

                    Session["id_Utilizador"] = userId.Value;
                    Session["Nome_Utilizador"] = nomeUtilizador.ToString();
                    Session["Img_utilizador"] = ((byte[])imgBinary).Length > 0 ? $"data:image/{imgType};base64,{Convert.ToBase64String((byte[])imgBinary)}" : $@"https://picsum.photos/200";
                    Session["logado"] = "Utilizador";
                    connection.Close();
                }
                
            }
            if (resultado == 1)
            {
               bool p = true;
                FazLogin(nomeUtilizador, passAuto, imgBinary, imgType, (int)Session["id_Utilizador"], p);
            }
           
        }

        // Método para fazer login do utilizador
        private void FazLogin(string email, string passAuto, byte[] imgBinary, string imgType, int id, bool v)
        {           
            var master = this.Master as MainLayout;
            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("LoginGoogle", myConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Email_Utilizador", email);
                    myCommand.Parameters.AddWithValue("@Pass_Utilizador", passAuto);
                    // Adicione o parâmetro de saída @Message
                    SqlParameter messageParameter = myCommand.Parameters.Add("@Message", SqlDbType.VarChar, 255);
                    messageParameter.Direction = ParameterDirection.Output;
                    SqlParameter returnParameter = myCommand.Parameters.Add("@Retorno", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.Output;
                    myConn.Open();
                    myCommand.ExecuteNonQuery();
                    string result = returnParameter.Value.ToString();
                    string message = messageParameter.Value.ToString(); // Obtenha o valor do parâmetro @Message

                    if (result == "1")
                    {
                        using (SqlDataReader reader = myCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Session["id_Utilizador"] = (int)reader["id_Utilizador"];
                                Session["Nome_Utilizador"] = reader["Nome_Utilizador"].ToString();
                                Session["Img_utilizador"] = ((byte[])imgBinary).Length > 0 ? $"data:image/{imgType};base64,{Convert.ToBase64String((byte[])imgBinary)}" : $@"https://picsum.photos/200";
                                Session["logado"] = reader["Perfis"].ToString();
                            }
                        }                      
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "redirectUser", 
                            "alert(' " +
                            $"UPS parece que existiu um erro, {message}" +
                            "')", true);
                    }
                    myConn.Close();
                  
                    master.CarregaXML();
                }
                if (v==true)
                {
                    Response.Redirect($@"/Pages/alteraPW.aspx?pwG={passAuto}");
                }
                else
                {
                    Response.Redirect($@"/Pages/MainPages/Perfil_User.aspx?U={id}");
                }
            }
         
        }

    }
}