using Microsoft.AspNet.Identity;
using SendGrid;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class EmailService : IIdentityMessageService
    {
        public static Task SendEmailAsync(IdentityMessage message)
        {
            // serviço para enviar email
            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new System.Net.Mail
                            .MailAddress("myappwebtest94@gmail.com", "LettuceBrain.com");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;
            
            var credentials = new NetworkCredential(
                 ConfigurationManager.AppSettings["mailAccount"],
                 ConfigurationManager.AppSettings["mailPassword"]
                 );
            // Cria um transporte web para enviar email
            var transporteWeb = new Web(credentials);
            // Envia o email
            if (transporteWeb != null)
            {
                return transporteWeb.DeliverAsync(myMessage);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                return Task.FromResult(0);
            }
        }

        public async Task SendAsync(IdentityMessage message)
        {
            await SendEmailAsync(message);
        }
    }
}