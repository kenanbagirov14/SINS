using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using log4net;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NIS.BLCore.Extensions;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.RealTimeConnection;
using NIS.BLCore.Models.SignalR;
using NIS.DALCore.Context;

namespace NIS.BLCore.Hubs
{
    public class NisHub : Hub
    {
        private RealTimeConnectionLogic _realTimeConnectionLogic;

        public NisHub()
        {

        }
        public override async Task OnConnectedAsync()
        {
            var isauthenticate = Context.UserIdentifier;
            _realTimeConnectionLogic = new RealTimeConnectionLogic(Context.User);
            //var at=HttpContext.Current.GetOwinContext().Authentication.User.GetUserId();

            var identity = (ClaimsIdentity)Context.User.Identity;
            var tmp = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (Context.User.GetUserId() != 0)
            {
                var userId = Context.User.GetUserId();

                var result = _realTimeConnectionLogic.Add(new RealTimeConnectionCreateModel
                {
                    ConnectionId = Context.ConnectionId,
                    UserId = userId,
                    Description = "qosuldu"
                });
            }
            //var identity = (ClaimsIdentity)Context.User.Identity;
            //var tmp = identity.FindFirst(ClaimTypes.NameIdentifier);
            //if (Context.GetHttpContext().Request.HttpContext.Request..QueryString["UserId"] != null)
            //{
            //    var userId = int.Parse(Context.QueryString["UserId"]);

            //    Clients.Caller.KeepAlive("UserId:" + userId + " Id: " + Context.ConnectionId);
            //    var result = _realTimeConnectionLogic.Add(new RealTimeConnectionCreateModel
            //    {
            //        ConnectionId = Context.ConnectionId,
            //        UserId = userId,
            //        Description = "qosuldu"
            //    });

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _realTimeConnectionLogic = new RealTimeConnectionLogic(Context.User);
            var result = _realTimeConnectionLogic.Remove(new RealTimeConnectionDeleteModel
            {
                ConnectionId = Context.ConnectionId
            });
            await base.OnDisconnectedAsync(exception);
        }


        #region Methods

        public void Remoterequestadd(RequestViewModel returnedData, List<RealTimeConnection> users)
        {
            _realTimeConnectionLogic = new RealTimeConnectionLogic(Context.User);
            var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            SignalrReturneddata<RequestViewModel> data = new SignalrReturneddata<RequestViewModel>()
            {
                NewData = returnedData
            };
            var signalrAddeddata = JObject.Parse(JsonConvert.SerializeObject(data,
          new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            logger.Info(
              "Post signalR process started with this parameters: " + signalrAddeddata +
              "Post signalR process started with this parameters: " + string.Join(",", users.Select(s => s.ConnectionId).ToArray())
             + $"{Environment.NewLine}"
             + " - Executed method : Remoterequestadd");
            Clients.Clients(users.Select(s => s.ConnectionId).ToList()).SendAsync("remoterequestadd", signalrAddeddata);
        }


        #endregion
    }
}
