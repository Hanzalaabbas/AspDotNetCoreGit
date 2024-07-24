using AspMVCCoreGit.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AspMVCCoreGit.Services
{
    public class EmailService : IEmailService
    {
        private const string tempaltePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;
        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject =UpdatePlaceHolders("Hello {{UserName}} , This is test email subject from Book Store web app", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"),userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }
        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            try { 
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };
            foreach (var toEmail in userEmailOptions.ToEmail)
            {
                mail.To.Add(toEmail);
            }
            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }
    catch (Exception ex)
    {
        Console.WriteLine($"Error sending email: {ex.Message}");
        throw;
    }

}
        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(tempaltePath, templateName));
            return body;
        }
        private string UpdatePlaceHolders(string text,List<KeyValuePair<string,string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeHolder in keyValuePairs)
                {
                    if(text.Contains(placeHolder.Key))
                    {
                      text = text.Replace(placeHolder.Key, placeHolder.Value);
                    }

                }
            }
            return text;
        }
    }
}
