using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NIS.BLCore.Hubs;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.MainTask;
using NIS.BLCore.Models.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using NIS.DALCore.Context;
using Microsoft.AspNetCore.SignalR;
using NIS.BLCore.Models.Comment;

namespace NIS.BLCore.Helpers
{
    public static class SignalRHelper
    {
        //Send Request Add event
        public static void Remoterequestadd(RequestViewModel returnedData, List<RealTimeConnection> users, IHubContext<NisHub> hubContext)
        {
            // create a logger 
            var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            SignalrReturneddata<RequestViewModel> data = new SignalrReturneddata<RequestViewModel>()
            {
                NewData = returnedData
            };
            var signalrAddeddata = JObject.Parse(JsonConvert.SerializeObject(data,
          new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            //logger.Info(
            //  "Post signalR process started with this parameters: " + signalrAddeddata +
            //  "Post signalR process started with this parameters: " + string.Join(",", users.Select(s => s.ConnectionId).ToArray())
            // + $"{Environment.NewLine}"
            // + " - Executed method : Remoterequestadd");
            hubContext.Clients.Clients(users.Select(s => s.ConnectionId).ToList()).SendAsync("remoterequestadd", signalrAddeddata);
        }

        //Send Request Update event
        public static void Remoterequestupdate(RequestViewModel returnedData, List<RealTimeConnection> users, IHubContext<NisHub> hubContext)
        {
            SignalrReturneddata<RequestViewModel> data = new SignalrReturneddata<RequestViewModel>()
            {
                NewData = returnedData
            };
            var signalrAddeddata = JObject.Parse(JsonConvert.SerializeObject(data,
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            hubContext.Clients.Clients(users.Select(s => s.ConnectionId).ToList()).SendAsync("remoterequestupdate", signalrAddeddata);
        }

        //Send Task Add event
        public static void RemoteTaskadd(TaskViewModel returnedData, List<RealTimeConnection> users, IHubContext<NisHub> hubContext)
        {
            SignalrReturneddata<TaskViewModel> data = new SignalrReturneddata<TaskViewModel>()
            {
                NewData = returnedData
            };
            var signalrAddeddata = JObject.Parse(JsonConvert.SerializeObject(data,
          new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            hubContext.Clients.Clients(users.Select(s => s.ConnectionId).ToList()).SendAsync("remoteTaskadd", signalrAddeddata);
        }

        //Send Commenr Add event
        public static void RemoteCommentadd(CommentViewModel returnedData, IHubContext<NisHub> hubContext)
        {
            SignalrReturneddata<CommentViewModel> data = new SignalrReturneddata<CommentViewModel>()
            {
                NewData = returnedData
            };
            var signalrAddeddata = JObject.Parse(JsonConvert.SerializeObject(data,
          new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            hubContext.Clients.All.SendAsync("emoteCommentadd", "at");
        }
    }
}
