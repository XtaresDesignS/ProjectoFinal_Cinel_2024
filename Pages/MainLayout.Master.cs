using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages
{
    public partial class MainLayout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            string pageName = Path.GetFileNameWithoutExtension(url);

            // Mapeamento dos nomes das páginas para os títulos
            var titleMap = new Dictionary<string, string>
            {
                { "Landing", "Bem Vindo" },
                { "SignIN", "Entrar" },
                { "SignUP", "Registar" },
                { "Collect", "Coleção" },
                { "Montra", "Galleria" },
                { "Backoffice", "Administração" },
                { "alterar", "Mantém o teu segredo" },
                { "ActivaContas", "Estás ON / OFF" },
                { "Carrega_Coins", "Criação de Coins" },
                { "Details", "Coins ao detalhe" },
                { "Details_user", "Detalhes do Utilizador" },
                { "Error404", "Fora de Orbita" },
                { "Errors", "Estamos Perdidos" },
                {"EditaCoins","XcoinS Edit" },
            };

            string title;
            if (titleMap.TryGetValue(pageName, out title))
            {
                this.Page.Title = $"Cinel | {title}";
            }
            else
            {
                this.Page.Title = "Cinel";
            }
            CarregaXML();
        }
        //carrega os elementos do menu
        public void CarregaXML()
        {
            StringBuilder xmlBuilder = new StringBuilder();
            xmlBuilder.Append("<data>");

            // Adicione a informação do perfil do usuário logado ao XML
            xmlBuilder.Append("<user>");
            xmlBuilder.AppendFormat("<id_Utilizador>{0}</id_Utilizador>", Session["id_Utilizador"]);
            xmlBuilder.AppendFormat("<nome>{0}</nome>", Session["Nome_Utilizador"]);
            xmlBuilder.AppendFormat("<perfil>{0}</perfil>", Session["logado"]);
            xmlBuilder.AppendFormat("<imagem>{0}</imagem>", Session["Img_utilizador"]);
            xmlBuilder.Append("</user>");
            xmlBuilder.Append("</data>");

            xml_menuContnt.DocumentContent = xmlBuilder.ToString();
            xml_Avatar.DocumentContent = xmlBuilder.ToString();
            VerificaBtns();

        }
        // mostra e esconde os btns do log out
        public void VerificaBtns()
        {
            //Este código irá garantir que o btn_Logout só seja visível se o Utilizador estiver logado, mesmo na 1ª vez da app

            // Verifica se o Utilizador está logado, ou seja se a Session["logado"] está com algum valor

            // aqui está a verificar se a string é nula ou vazia. Se a string for nula ou vazia, com base no caso retorna True ou False
            bool isUserLoggedIn = !string.IsNullOrEmpty((string)Session["logado"]);

            // Define a visibilidade e a capacidade de habilitação do botão com base no status de login do usuário
            btn_Logout.Visible = isUserLoggedIn;
            btn_Logout.Enabled = isUserLoggedIn;

        }
        //eventClick do BTn Log out, Carrega o XML,e limpa as sessions   
        protected void btn_Logout_Click1(object sender, EventArgs e)
        {
            Session.Clear();
            Session["logado"] = string.Empty;
            Response.Redirect("/Pages/Login.aspx");
            CarregaXML();
        }
        public void EnviaEmail(string to, string token)
        {
            // As configurações do servidor SMTP foram obtidas.
            string smtpServer = ConfigurationManager.AppSettings["SMTP_URL"];
            string from = ConfigurationManager.AppSettings["SMTPMailUser"];

            // O cliente SMTP foi criado.
            SmtpClient client = new SmtpClient(smtpServer);

            // SSL foi ativado e a porta foi definida.
            client.EnableSsl = true;
            client.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

            // As credenciais foram fornecidas para autenticação no servidor SMTP.
            client.Credentials = new System.Net.NetworkCredential(from, ConfigurationManager.AppSettings["SMTP_PASSWORD"]);

            // O link de ativação foi criado.
            string linkAtivacao = $"https://localhost:44365/Pages/ativar.aspx?token={token}";

            // O corpo do e-mail foi criado, incluindo o link de ativação.
            string body = $"A equipa da CINEL da-lhe as boas-vindas,\n\nQueira por favor, clicar no link abaixo para ativar a sua conta:\n\n{linkAtivacao}\n\nCumprimentos,\nEquipa COINCLUB\n\n";

            // A mensagem de e-mail foi criada, incluindo o remetente, o destinatário e o assunto.
            MailMessage mail = new MailMessage(from, to);
            mail.From = new MailAddress(from, "COINCLUB");
            mail.Subject = "Ativação de conta";

            // Um objeto LinkedResource foi criado para a imagem do logotipo.
            LinkedResource logo = new LinkedResource("/Images/logo.png", MediaTypeNames.Image.Jpeg);
            logo.ContentId = "LogoEmpresa";

            // A imagem foi adicionada ao corpo do e-mail.
            AlternateView av = AlternateView.CreateAlternateViewFromString(body + "<img src='cid:LogoEmpresa'/>", null, MediaTypeNames.Text.Html);
            av.LinkedResources.Add(logo);

            mail.AlternateViews.Add(av);

            // O e-mail foi enviado.
            client.Send(mail);
        }
        public string GerarNovaPalavraPasse()
        {
            // Define o comprimento da palavra-passe
            int passwordLength = 8;

            // Define os caracteres que serão usados na palavra-passe
            string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            //  Random para selecionar caracteres aleatórios
            Random random = new Random();

            // Inicializa a palavra-passe
            string password = "";

            for (int i = 0; i < passwordLength; i++)
            {
                // Selecione um caractere aleatório dos caracteres válidos
                char randomChar = validChars[random.Next(validChars.Length)];

                // Adicione o caractere à palavra-passe
                password += randomChar;
            }

            return password;
        }
    }
}