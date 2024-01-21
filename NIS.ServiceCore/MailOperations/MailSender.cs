using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace NIS.ServiceCore.MailOperations
{
  public static class MailSender
  {
    public static bool Send(string _to, string _subject, string _body,string _as,string _bcc)
    {
      try
      {
        MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress(_to));
        msg.From = new MailAddress(_as) ;
        if(_bcc != "")
        msg.Bcc.Add(new MailAddress(_bcc));
        msg.Subject = _subject;
        msg.Body = _body;
        msg.IsBodyHtml = true;
        SmtpClient client = new SmtpClient
        {
          UseDefaultCredentials = false,
          Credentials = new System.Net.NetworkCredential("notification@aztelekom.local", "!N5g87!"),
          Port = 587, // You can use Port 25 if 587 is blocked (mine is!)
          Host = "mail.aztelekom.az",
          // client.
          DeliveryMethod = SmtpDeliveryMethod.Network,
          EnableSsl = true
        };

        client.Send(msg);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}