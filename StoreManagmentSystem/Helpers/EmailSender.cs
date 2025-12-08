using System.Net;
using System.Net.Mail;

namespace StoreManagmentSystem.Help
{
    public static class EmailSender
    {
        private static string SmtpServer = "smtp.gmail.com";
        private static int SmtpPort = 587;
        private static string SenderEmail = "tuncaytasim24@gmail.com";
        private static string SenderPassword = "klsg ttjg xxzn dxbg";

        public static async Task SendEmail(string to, string subject, string body)
        {
            var client = new SmtpClient(SmtpServer, SmtpPort)
            {
                Port = SmtpPort,
                Credentials = new NetworkCredential(SenderEmail, SenderPassword),
                EnableSsl = true

            };
            
            var mail = new MailMessage(SenderEmail, to, subject, body);
            await client.SendMailAsync(mail);
        }

    }
}
