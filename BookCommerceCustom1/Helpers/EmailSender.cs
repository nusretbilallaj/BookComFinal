using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace BookCommerceCustom1.Helpers
{
	public class EmailSender:IEmailSender
	{
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
	  //       var emailToSend = new MimeMessage();
   //          emailToSend.From.Add(MailboxAddress.Parse("netktit@gmail.com"));
   //          emailToSend.To.Add(MailboxAddress.Parse(email));
   //          emailToSend.Subject=subject;
   //          emailToSend.Body=new TextPart(MimeKit.Text.TextFormat.Html)
   //          {
   //              Text= htmlMessage
			// };
   //          using (var emailClient = new SmtpClient())
   //          {
	  //           emailClient.Connect("smtp.gmail.com",587,MailKit.Security.SecureSocketOptions.StartTls);
	  //           emailClient.Authenticate("netktit@gmail.com", "");
	  //           emailClient.Send(emailToSend);
   //              emailClient.Disconnect(true);
   //          }
            return Task.CompletedTask;
        }
    }
}
