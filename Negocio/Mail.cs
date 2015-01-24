using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class Mail
    {
        public Mail()
        {
        }

        public void EnviarMail(string msj, string asunto)
        {
            string from = "lostpassword@tamboprp.uy";
            string to = "soporte@tamboprp.uy";
            string message = msj;
            string Subject = asunto;
            string servidor = "mail.tamboprp.uy";

            MailMessage email;
            email = new MailMessage(from, to, Subject, message);
            SmtpClient smtpMail = new SmtpClient(servidor);

            email.IsBodyHtml = false;
            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = new System.Net.NetworkCredential("soporte", "tamboprpMAILsoporte2014*/pass");
            smtpMail.Send(email);

        }

        public void EnviarMailHtml(string userMail, string subject, string htmlCode)
        {
            string from = "notificaciones@tamboprp.uy";
            string to = userMail;
            //string servidor = "mail.tamboprp.uy";
            string servidor = "mail.stiler.com.uy";

            MailMessage email;
            email = new MailMessage(from, to);
            email.Subject = subject + " | tamboprp";
            email.IsBodyHtml = true;
            email.Body = htmlCode;

            SmtpClient smtpMail = new SmtpClient(servidor);
            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = new System.Net.NetworkCredential("fg16630", "Lasmisiones401212");
            smtpMail.Send(email);

        }

    }

}
