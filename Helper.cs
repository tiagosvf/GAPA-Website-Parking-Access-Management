using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Helper
{
    public static void enviarMail(string nomeDe, string passwordDe, string para, string assunto, string texto, string anexo = null)
    {
        //objetos mail
        System.Net.Mail.MailMessage mensagem = new System.Net.Mail.MailMessage();
        System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential(nomeDe, passwordDe);
        System.Net.Mail.MailAddress dequem = new System.Net.Mail.MailAddress(nomeDe);
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

        //mensagem
        mensagem.To.Add(para);
        mensagem.From = dequem;
        mensagem.Subject = assunto;
        mensagem.Body = texto;
        mensagem.IsBodyHtml = true;
        //servidor
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = credenciais;

        //anexo
        if (anexo != null && anexo != "")
        {
            if (System.IO.File.Exists(anexo) == true)
            {
                System.Net.Mail.Attachment ficheiroAnexo = new System.Net.Mail.Attachment(anexo);
                mensagem.Attachments.Add(ficheiroAnexo);
            }
        }
        //enviar
        smtp.Send(mensagem);
    }
}
