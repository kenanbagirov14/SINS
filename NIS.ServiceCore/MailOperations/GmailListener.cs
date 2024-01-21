using ImapX;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.Department;
using NIS.BLCore.Models.MainTask;
using NIS.ServiceCore.Controllers;
using NIS.ServiceCore.MailOperations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using System.Text;
using NIS.BLCore.DTO;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Attachment;
using NIS.BLCore.Models.Region;

namespace NIS.Service
{
  public static class GmailListener
  {
    public static ICollection<DepartmentViewModel> departmentList;


    public static void Initalize()
    {
      // After you created the client, set

      var client = new ImapClient("imap.gmail.com", 993, SslProtocols.Tls, true);
      if (client.Connect())
      {
        if (client.Capabilities.Idle)
        {
          //todo
        }
        if (client.Login("aztelekomlistener@gmail.com", "Aztelekom2017!"))
        {
          client.OnNewMessagesArrived += HandleNewMessageEvent;
          //client.Behavior.NoopIssueTimeout = 120;
          //client.IsDebug = true;
          client.Folders.Inbox.StartIdling();

        }
      }

      var aznetclient = new ImapClient("imap.gmail.com", 993, SslProtocols.Tls, true);
      if (aznetclient.Connect())
      {
        if (aznetclient.Capabilities.Idle)
        {
          //todo
        }
        if (aznetclient.Login("broadband.aztelekom@gmail.com", "Aztelekom.net@2017"))
        {
          aznetclient.OnNewMessagesArrived += AznetHandleNewMessageEvent;
          //aznetclient.Behavior.NoopIssueTimeout = 120;
          //client.IsDebug = true;
          aznetclient.Folders.Inbox.StartIdling();

        }

      }

    }
    public static void HandleNewMessageEvent(object sender, IdleEventArgs e)
    {
      // var message = e.Messages;
      foreach (Message message in e.Messages)
      {
        try
        {
          var regionLogic = new RegionLogic();
          RegionFindModel dfm = new RegionFindModel
          {
            Email = message.From.Address
          };
          if (message.Subject.ToLower().Contains("reseller") && !message.Subject.ToLower().Contains("re:"))
          {
            var region = regionLogic.FindAll(dfm).Output.FirstOrDefault(s => s.Email == message.From.Address);

            CustomerRequestController crc = new CustomerRequestController();
            MainTaskController mtc = new MainTaskController();
            var customerRequestLogic = new CustomerRequestLogic();
            MainTaskLogic taskLogic = new MainTaskLogic();
            RequestCreateModel rcm = new RequestCreateModel()
            {
              Email = message.From.Address,
              Description = message.Subject,
              CustomerName = message.From.DisplayName,
              RegionId = region?.Id,
              CustomerNumber = region?.Phone,
              SourceTypeId = 1,
              StartDate = DateTime.Now,
              CustomerRequestTypeId = 22,
              Text = message.Body.Text
            };
            rcm.MainTask.Add(new TaskDto
            {
              Description = region?.Name + " | Reseller",
              Note = message.Body.Text,
              DepartmentId = 7,
              StartDate = DateTime.Now,
              ProjectId = 2
            });
            //  message.Seen = true;
            RequestViewModel request = customerRequestLogic.Add(rcm).Output;
            //if (request != null)
            //{
            //  TaskCreateModel tcm = new TaskCreateModel()
            //  {
            //    Description = request.Region?.Name + " | " + request.CustomerRequestType.Name,
            //    Note = request.Text,
            //    DepartmentId = 7,
            //    StartDate = request.CreatedDate,
            //    CustomerRequestId = request.Id,
            //    ProjectId = 1
            //  };

            //  TaskViewModel task = taskLogic.Add(tcm).Output;

              //if (task!=null)
              //{
              //    if (message.Attachments.Length>0)
              //    {
              //        foreach (var file in message.Attachments)
              //        {
              //            AttachmentCreateModel attachment = new AttachmentCreateModel
              //            {
              //                MainTaskId = task.MainTaskId,
              //                Container = Convert.ToBase64String(file.FileData),
              //                Extension = Path.GetExtension(file.FileName) //Todo

              //            };
              //        }
              //    }

              //}

            //Send email back

              string _to = message.From.Address;
              string _subject = "Reseller: TicketID:[#" + request.Id + "]";
              string _body = "Sizin sifarişiniz qəbul olundu və " + request.Id + " nömrəsi ilə sorğu yaradıldı\n" + (char)13 +
                          "Sifarişinizə 48 saat ərzində cavab veriləcəkdir \n" + (char)13 +
                         "Bu müddət ərzində cavab almasanız 3440000/441 nömrəsi ilə əlaqə saxlamağınızı xahiş edirik\n" + (char)13 +
                         "Əlaqə saxlayarkən sorğu nömrəsini təqdim etməyi unutmayın";
              MailSender.Send(_to, _subject, _body, "notification@aztelekom.az", "");
            
          }
        }
        catch (Exception ex)
        {
          MailSender.Send("ismayil.ismayilov@aztelekom.az", "nis error", "exception occured at line 156 GmailListener", "notification@aztelekom.az", "");
        }

      }
    }

    public static void AznetHandleNewMessageEvent(object sender, IdleEventArgs e)
    {
      foreach (Message message in e.Messages)
      {
        try
        {
          var regionLogic = new RegionLogic();
          RegionFindModel dfm = new RegionFindModel
          {
            Email = message.From.Address
          };

          if (!message.Subject.ToLower().Contains("re:"))
          {
            var region = regionLogic.FindAll(dfm).Output.FirstOrDefault(s => s.Email == message.From.Address);
            CustomerRequestController crc = new CustomerRequestController();
            MainTaskController mtc = new MainTaskController();
            MainTaskLogic taskLogic = new MainTaskLogic();
            var customerRequestLogic = new CustomerRequestLogic();
            AttachmentController attachmentController = new AttachmentController();
            RequestCreateModel rcm = new RequestCreateModel()
            {
              Email = message.From.Address,
              Description = message.Subject,
              CustomerName = message.From.DisplayName,
              RegionId = region?.Id,
              CustomerNumber = region?.Phone,
              SourceTypeId = 1,
              StartDate = DateTime.Now,
              CustomerRequestTypeId = 20,
              Text = message.Body.Text
            };
            //  message.Seen = true;
            RequestViewModel request = customerRequestLogic.Add(rcm).Output;
            if (request != null)
            {
              TaskCreateModel tcm = new TaskCreateModel()
              {
                Description = request.Region?.Name + " | " + request.CustomerRequestType.Name,
                Note = request.Text,
                DepartmentId = 4,
                StartDate = request.CreatedDate,
                CustomerRequestId = request.Id,
                ProjectId=3
              };

              TaskViewModel task = taskLogic.Add(tcm).Output;

              if (task != null)
              {
                if (message.Attachments.Length > 0)
                {
                  foreach (var file in message.Attachments)
                  {
                    AttachmentCreateModel attachment = new AttachmentCreateModel
                    {
                      MainTaskId = task.MainTaskId,
                      Container = Convert.ToBase64String(file.FileData),
                      Extension = Path.GetExtension(file.FileName), //Todo
                      FileType = 1
                    };
                    attachmentController.Add(attachment);
                  }
                }

              }

              string to = message.From.Address;
              string subject = "Aztelekom.net: TicketID:[#" + request.Id + "]";
              string body = "Sizin sifarişiniz qəbul olundu və " + request.Id + " nömrəsi ilə sorğu yaradıldı\n" + (char)13 +
                          "Sifarişinizə 48 saat ərzində cavab veriləcəkdir \n" + (char)13 +
                         "Bu müddət ərzində cavab almasanız 3440000/251 nömrəsi ilə əlaqə saxlamağınızı xahiş edirik\n" + (char)13 +
                         "Əlaqə saxlayarkən sorğu nömrəsini təqdim etməyi unutmayın";
              MailSender.Send(to, subject, body, "notification@aztelekom.az", "");
            }
          }
        }
        catch (Exception ex)
        {
          // Console.WriteLine(ex.Message); //TODO handle error
        }

      }
    }

  }
}
