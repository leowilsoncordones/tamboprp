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
            try
            {
                string from = "soporte@tamboprp.uy";
                string to = "soporte@tamboprp.uy";
                string message = msj;
                string Subject = asunto;
                string servidor = "smtp.tamboprp.uy";

                MailMessage email;
                email = new MailMessage(from, to, Subject, message);
                SmtpClient smtpMail = new SmtpClient(servidor);

                email.IsBodyHtml = false;
                smtpMail.UseDefaultCredentials = false;
                smtpMail.Credentials = new System.Net.NetworkCredential("soporte", "PRPsoporte2014*");
                smtpMail.Send(email);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void EnviarMailHtml(string userMail, string subject, string htmlCode)
        {
            try
            {
                string from = "notificaciones@tamboprp.uy";
                string bcc = "notificaciones@tamboprp.uy";
                string to = userMail;
                string servidor = "smtp.tamboprp.uy";
                
                MailMessage email;
                email = new MailMessage(from, to);
                email.Subject = subject + " | tamboprp";
                email.Bcc.Add(bcc);
                email.IsBodyHtml = true;
                email.Body = htmlCode;

                SmtpClient smtpMail = new SmtpClient(servidor);
                //smtpMail.Port = 26;
                smtpMail.UseDefaultCredentials = false;
                smtpMail.Credentials = new System.Net.NetworkCredential("notificaciones", "PRPnotif2014*");
                smtpMail.Send(email); // revisar a gmail
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }

}
