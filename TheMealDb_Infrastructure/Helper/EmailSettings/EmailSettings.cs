using System.Net.Mail;
using System.Net;
using TheMealDb_Core.Model;

namespace TheMealDb_Infrastructure.Helper.EmailSettings
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("amrhoji77@gmail.com", "cmll mffi odiw qdgx");
            client.Send("amrhoji77@gmail.com", email.Recivers, email.Subject, email.Body);
        }
    }
}
