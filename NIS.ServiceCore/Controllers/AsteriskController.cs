using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using NIS.BLCore.Hubs;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.SignalR;
using NIS.BLCore.Models.User;

namespace NIS.ServiceCore.Controllers
{
  public class AsteriskController : ApiController
  {
    #region Properties

    private readonly UserLogic _userLogic;

    #endregion

    #region Constructor

    public AsteriskController()
    {
      _userLogic = new UserLogic();
    }
    #endregion

    [HttpGet]
    public IHttpActionResult NewCall([FromUri]CallDetails call)
    {
      var hub = GlobalHost.ConnectionManager.GetHubContext<NisHub>();
      var user = _userLogic.FindAll(new UserFindModel { PhoneNumber = call.DestinationNumber });
      user.Output.ToList().ForEach(x => hub.Clients.Client(x.ConnectedId).remoteopenrequestdialog(call));

      return Ok(call);
    }
  }
}
