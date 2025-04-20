using System.Net;
using System.Net.Mail;

namespace LinkDev.IKEA.PL.Utility
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email) {
            var Client= new SmtpClient("smtp.gmail.com",587);
           Client.EnableSsl = true;
            Client.Credentials=new NetworkCredential("mm4834424@gmail.com", "ifruukzdxdnldmgh");
            Client.Send("mm4834424@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
