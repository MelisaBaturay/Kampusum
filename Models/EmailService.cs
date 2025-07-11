using System.Net.Mail;
using System.Net;

namespace Kampusum.Models
{
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _emailFrom;
        private readonly string _emailPassword;

        public EmailService(string smtpHost, int smtpPort, string emailFrom, string emailPassword)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _emailFrom = emailFrom;
            _emailPassword = emailPassword;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_emailFrom, _emailPassword);
                    client.EnableSsl = true;

                    var message = new MailMessage
                    {
                        From = new MailAddress(_emailFrom),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    message.To.Add(to);

                    await client.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendVerificationEmailAsync(string to, string verificationCode)
        {
            var subject = "E-posta Doğrulama";
            var body = $@"
                <html>
                <body>
                    <h2>E-posta Doğrulama</h2>
                    <p>Hesabınızı doğrulamak için aşağıdaki kodu kullanın:</p>
                    <h3 style='color: #007bff; font-size: 24px;'>{verificationCode}</h3>
                    <p>Bu kod 10 dakika geçerlidir.</p>
                    <p>Eğer bu işlemi siz yapmadıysanız, bu e-postayı görmezden gelebilirsiniz.</p>
                </body>
                </html>";

            return await SendEmailAsync(to, subject, body);
        }
    }
}