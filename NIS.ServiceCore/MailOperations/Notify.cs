//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Exchange.WebServices.Data;
//using System.Net.Sockets;
//using System.Net.Security;
//using System.Security.Cryptography.X509Certificates;
//using System.Threading;
//using NIS.BLCore.Models.Region;
//using NIS.ServiceCore.Controllers;
//using NIS.BLCore.Logics;
//using NIS.BLCore.Models.CustomerRequest;
//using NIS.BLCore.DTO;
//using System.Net.Mail;
//using System.Security.Claims;

//namespace NIS.ServiceCore.MailOperations
//{
//    public class Notify
//    {
//        private ExchangeService _service;
//        private TcpListener _listener;
//        private Thread _th;
//        private ClaimsPrincipal User;

//        public string SubscriptionId { get; set; }

//        public string Name
//        {
//            get
//            {
//                return "EWSConsoleNotify";
//            }
//        }

//        public Notify(ClaimsPrincipal user)
//        {
//            User = user;
//        }
//        public void WriteLog(string Message)
//        {
//            Console.WriteLine(Message);
//        }

//        public void Start()
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (
//                Object obj,
//                X509Certificate certificate,
//                X509Chain chain,
//                SslPolicyErrors errors)
//            {
//                return true;
//                // TODO: check for a valid certificate
//            };

//            // Create the subscription
//            _service = new ExchangeService();
//            // Set credentials: user, password and domain
//            _service.Credentials = new WebCredentials("listener", "!D76f9!", "aztelekom.local");
//            // URL of the EWS
//            _service.Url = new Uri("https://mail.aztelekom.az/ews/exchange.asmx");

//            PushSubscription ps = _service.SubscribeToPushNotificationsOnAllFolders(new Uri("http://91.135.244.20:5051"),
//                5 // Every 5 minutes Exchange will send us a status message
//                , null, EventType.NewMail);

//            SubscriptionId = ps.Id;

//            // Listen on port 5051
//#pragma warning disable CS0618 // Type or member is obsolete
//            _listener = new TcpListener(50517);
//#pragma warning restore CS0618 // Type or member is obsolete
//            _listener.Start();

//            _th = new Thread(new ThreadStart(ProcessIncomingMessages));
//            _th.Start();
//        }

//        public void ShowMessage(string UniqueId)
//        {
//            var itemId = new ItemId(UniqueId);
//            EmailMessage mail = ((EmailMessage)Item.Bind(_service, itemId));

//            EmailMessage message = EmailMessage.Bind(_service, itemId, BasePropertySet.IdOnly);


//            //responseMessage.BodyPrefix = myReply;

//            // Send the response message.
//            // This method call results in a CreateItem call to EWS.

//            //responseMessage.SendAndSaveCopy();
//            // Check that the response was sent by calling FindRecentlySent.
//            //FindRecentlySent(message);

//            try
//            {
//                if (mail.DisplayTo.ToLower().Contains("internet"))
//                {
//                    if (!mail.Subject.ToLower().Contains("TicketID") && !mail.Subject.ToLower().Contains("re:"))
//                    {
//                        var regionLogic = new RegionLogic(accessor.HttpContext.User);
//                        RegionFindModel dfm = new RegionFindModel
//                        {
//                            Email = mail.From.Address
//                        };
//                        var region = regionLogic.FindAll(dfm).Output.FirstOrDefault(s => s.Email == mail.From.Address);
//                        //if (region == null) return;
//                        CustomerRequestController crc = new CustomerRequestController(IHttpContextAccessor accessor);
//                        MainTaskController mtc = new MainTaskController(IHttpContextAccessor accessor);
//                        var customerRequestLogic = new CustomerRequestLogic(accessor.HttpContext.User);
//                        MainTaskLogic taskLogic = new MainTaskLogic(accessor.HttpContext.User);
//                        RequestCreateModel rcm = new RequestCreateModel()
//                        {
//                            Email = mail.From.Address,
//                            Description = mail.Subject,
//                            CustomerName = mail.From.Name,
//                            RegionId = region?.Id,
//                            CustomerNumber = region?.Phone,
//                            SourceTypeId = 1,
//                            StartDate = DateTime.Now,
//                            CustomerRequestTypeId = 20,
//                            Text = mail.Body.Text,
//                            MailUniqueId = UniqueId

//                        };
//                        rcm.MainTask.Add(new TaskDto
//                        {
//                            Description = region?.Name + " | Broad Band",
//                            Note = mail.Body.Text,
//                            DepartmentId = 4,//ToDo
//                            StartDate = DateTime.Now,
//                            ProjectId = 3 //ToDo
//                        });

//                        RequestViewModel request = customerRequestLogic.Add(rcm).Output;

//                        //Send email back
//                        // Bind to the email message to reply to by using the ItemId.
//                        // This method call results in a GetItem call to EWS.
//                        //EmailMessage message = EmailMessage.Bind(_service, itemId, BasePropertySet.IdOnly);
//                        // Create the reply response message from the original email message.
//                        // Indicate whether the message is a reply or reply all type of reply.


//                        // Prepend the reply to the message body. 


//                        bool replyToAll = false;
//                        message.From = new Microsoft.Exchange.WebServices.Data.EmailAddress("software.support@aztelekom.az");
//                        var responseMessage = message.CreateReply(replyToAll);
//                        responseMessage.BodyPrefix = "Sizin sifarişiniz qəbul olundu və " + request.Id + " nömrəsi ilə sorğu yaradıldı\n" + (char)13 +
//                                    "Sifarişinizə 48 saat ərzində cavab veriləcəkdir \n" + (char)13 +
//                                   "Bu müddət ərzində cavab almasanız 3440000/441 nömrəsi ilə əlaqə saxlamağınızı xahiş edirik\n" + (char)13 +
//                                   "Əlaqə saxlayarkən sorğu nömrəsini təqdim etməyi unutmayın"; ;
//                        EmailMessage reply = responseMessage.Save();
//                        reply.Subject = "Software Support TicketID:[#" + request.Id + "]";
//                        reply.From = new Microsoft.Exchange.WebServices.Data.EmailAddress("software.support@aztelekom.az");
//                        reply.Update(ConflictResolutionMode.AutoResolve);
//                        reply.SendAndSaveCopy();

//                        //MailSender.Send(_to, _subject, _body, "notification@aztelekom.az", "");
//                    }
//                }
//                else

//                {
//                    var regionLogic = new RegionLogic(accessor.HttpContext.User);

//                    RegionFindModel dfm = new RegionFindModel
//                    {
//                        Email = mail.From.Address
//                    };
//                    if (mail.Subject.ToLower().Contains("reseller") && !mail.From.Address.ToLower().Contains("software.support") && !mail.Subject.ToLower().Contains("TicketID") && !mail.Subject.ToLower().Contains("re:"))
//                    {
//                        var region = regionLogic.FindAll(dfm).Output.FirstOrDefault(s => s.Email == mail.From.Address);
//                        //if (region == null) return;
//                        CustomerRequestController crc = new CustomerRequestController(IHttpContextAccessor accessor);
//                        MainTaskController mtc = new MainTaskController(IHttpContextAccessor accessor);
//                        var customerRequestLogic = new CustomerRequestLogic(accessor.HttpContext.User);
//                        MainTaskLogic taskLogic = new MainTaskLogic(accessor.HttpContext.User);
//                        RequestCreateModel rcm = new RequestCreateModel()
//                        {
//                            Email = mail.From.Address,
//                            Description = mail.Subject,
//                            CustomerName = mail.From.Name,
//                            RegionId = region?.Id,
//                            CustomerNumber = region?.Phone,
//                            SourceTypeId = 1,
//                            StartDate = DateTime.Now,
//                            CustomerRequestTypeId = 22,
//                            Text = mail.Body.Text,
//                            MailUniqueId = UniqueId

//                        };
//                        rcm.MainTask.Add(new TaskDto
//                        {
//                            Description = region?.Name + " | Reseller",
//                            Note = mail.Body.Text,
//                            DepartmentId = 7,
//                            StartDate = DateTime.Now,
//                            ProjectId = 2
//                        });

//                        RequestViewModel request = customerRequestLogic.Add(rcm).Output;

//                        //Send email back
//                        // Bind to the email message to reply to by using the ItemId.
//                        // This method call results in a GetItem call to EWS.
//                        //EmailMessage message = EmailMessage.Bind(_service, itemId, BasePropertySet.IdOnly);
//                        // Create the reply response message from the original email message.
//                        // Indicate whether the message is a reply or reply all type of reply.


//                        // Prepend the reply to the message body. 


//                        bool replyToAll = false;
//                        message.From = new Microsoft.Exchange.WebServices.Data.EmailAddress("software.support@aztelekom.az");
//                        var responseMessage = message.CreateReply(replyToAll);
//                        responseMessage.BodyPrefix = "Sizin sifarişiniz qəbul olundu və " + request.Id + " nömrəsi ilə sorğu yaradıldı\n" + (char)13 +
//                                    "Sifarişinizə 48 saat ərzində cavab veriləcəkdir \n" + (char)13 +
//                                   "Bu müddət ərzində cavab almasanız 3440000/441 nömrəsi ilə əlaqə saxlamağınızı xahiş edirik\n" + (char)13 +
//                                   "Əlaqə saxlayarkən sorğu nömrəsini təqdim etməyi unutmayın"; ;
//                        EmailMessage reply = responseMessage.Save();
//                        reply.Subject = "Reseller: TicketID:[#" + request.Id + "]";
//                        reply.From = new Microsoft.Exchange.WebServices.Data.EmailAddress("software.support@aztelekom.az");
//                        reply.Update(ConflictResolutionMode.AutoResolve);
//                        reply.SendAndSaveCopy();

//                        //MailSender.Send(_to, _subject, _body, "notification@aztelekom.az", "");

//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                while (ex.InnerException != null)
//                    ex = ex?.InnerException;
//                MailSender.Send("ismayil.ismayilov@aztelekom.az", "nis error", ex.Message, "notification@aztelekom.az", "");
//            }

//            Console.WriteLine("New mail from: {0}, Subject: {1}", mail.From.Name, mail.Subject);
//        }

//        public bool SendMessage(string UniqueId, string _to, string _subject, string _body, string _as, string _bcc)
//        {
//            var itemId = new ItemId(UniqueId);
//            _service = new ExchangeService();
//            // Set credentials: user, password and domain
//            _service.Credentials = new WebCredentials("listener", "!D76f9!", "aztelekom.local");
//            // URL of the EWS
//            _service.Url = new Uri("https://mail.aztelekom.az/ews/exchange.asmx");

//            EmailMessage mail = ((EmailMessage)Item.Bind(_service, itemId));

//            try
//            {
//                EmailMessage message = EmailMessage.Bind(_service, itemId, BasePropertySet.IdOnly);

//                //Send email back
//                // Bind to the email message to reply to by using the ItemId.
//                // This method call results in a GetItem call to EWS.
//                //EmailMessage message = EmailMessage.Bind(_service, itemId, BasePropertySet.IdOnly);
//                // Create the reply response message from the original email message.
//                // Indicate whether the message is a reply or reply all type of reply.


//                // Prepend the reply to the message body. 


//                bool replyToAll = false;
//                message.From = new Microsoft.Exchange.WebServices.Data.EmailAddress("software.support@aztelekom.az");
//                var responseMessage = message.CreateReply(replyToAll);
//                responseMessage.BodyPrefix = _body;
//                EmailMessage reply = responseMessage.Save();
//                reply.Subject = _subject;
//                reply.From = new Microsoft.Exchange.WebServices.Data.EmailAddress("software.support@aztelekom.az");
//                reply.Update(ConflictResolutionMode.AutoResolve);
//                reply.SendAndSaveCopy();

//                return true;
//                //MailSender.Send(_to, _subject, _body, "notification@aztelekom.az", "");


//            }
//            catch (Exception ex)
//            {
//                while (ex.InnerException != null)
//                    ex = ex?.InnerException;
//                MailSender.Send("ismayil.ismayilov@aztelekom.az", "nis error", ex.Message, "notification@aztelekom.az", "");
//                return false;
//            }

//        }

//        public void Stop()
//        {
//            _th.Abort();
//            _listener.Stop();
//        }


//        private void ProcessIncomingMessages()
//        {
//            for (; ; )
//            {
//                CSHTTPRequest csHttpRequest = new CSHTTPRequest(_listener.AcceptTcpClient(), this);

//                csHttpRequest.Process();
//            }
//        }
//    }
//}
