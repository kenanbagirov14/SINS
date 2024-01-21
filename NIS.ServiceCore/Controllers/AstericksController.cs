using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NIS.BLCore.DTO;
using NIS.BLCore.Hubs;
using NIS.BLCore.Logics;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.MainTask;
using NIS.BLCore.Models.SignalR;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.SignalR;
using HybridModelBinding;
using Microsoft.Extensions.Configuration;
using NIS.BLCore.Models.Region;
using NIS.BLCore;
using NIS.ServiceCore.Helper;
using NIS.UtilsCore;
using NIS.UtilsCore.Enums;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AstericksController : BaseController
    {
       // private readonly CustomerRequestLogic _customerRequestLogic;
        private readonly RegionLogic _regionLogic;
        private readonly IHubContext<NisHub> _hubContext;
        private IHttpContextAccessor _accessor;
        private IConfiguration _configuration;

        public AstericksController(IHttpContextAccessor accessor, IHubContext<NisHub> hubContext, IConfiguration configuration)
        {
           // _customerRequestLogic = new CustomerRequestLogic(accessor.HttpContext.User, hubContext, configuration: configuration);
            _regionLogic = new RegionLogic(accessor.HttpContext.User);
            _hubContext = hubContext;
            _accessor = accessor;
            _configuration = configuration;
        }

      


        [HttpPost("Add")]
        public IActionResult Add([FromBody] AstericksCall[] calss)
        {


            LogicResult<List<int?>> ListResponsData = new LogicResult<List<int?>>();
            ListResponsData.Output = new List<int?>();
            LogicResult<RegionViewModel> RegionRespons = new LogicResult<RegionViewModel>();

            List<RequestCreateModel> entities = new List<RequestCreateModel>();
        
            foreach (var call in calss)
            {
                RegionViewModel region = new RegionViewModel();

                var first = call.CustomerNumber.ToString().Substring(0, 2);
                var second = call.CustomerNumber.ToString().Substring(0, 4);
                RegionFindModel AstericksRegion = new RegionFindModel();

                if (first != "12" && first != "18")
                {
                    AstericksRegion.RegionPrefix = int.Parse(second); ;
                    RegionRespons = _regionLogic.Find(AstericksRegion);

                }
                else
                {
                    AstericksRegion.RegionPrefix = int.Parse(first); ;
                    RegionRespons = _regionLogic.Find(AstericksRegion);

                }

                if (RegionRespons.IsSuccess == true && RegionRespons.Output!=null)
                {
                    RequestCreateModel entity = new RequestCreateModel();

                    entity.CustomerNumber = call.CustomerNumber;
                    entity.RegionId = RegionRespons.Output.Id;
                    entity.ContactNumber = call.ContactNumber;
                    entity.CustomerName = "Astericks-" + call.CustomerNumber;
                    entity.SourceTypeId = 2;
                    if (call.SourceCode == 122)
                    {
                        //telefon problemi
                        entity.CustomerRequestTypeId = 160;
                    }
                    else if (call.SourceCode == 170)
                    {
                        //internet problemi
                        entity.CustomerRequestTypeId = 178;
                    }
                    entity.StartDate = System.DateTime.Now;
                    CustomerRequestLogic CustomerRequestLogic = new CustomerRequestLogic(_accessor.HttpContext.User, _hubContext, configuration: _configuration);
                    CustomerRequestLogic.GeneralUserId = 81;
                    var RequestReturnedData = CustomerRequestLogic.Add(entity);
                  
                    if (RequestReturnedData.IsSuccess == true)
                    {
                        ListResponsData.Output.Add(RequestReturnedData.Output.CustomerNumber);
                        CustomerRequestLogic.Dispose();
                    }
                    else
                    {
                        ListResponsData.ErrorList.Add(new Error() { Code = "500", Type = OperationResultCode.Error, CustomerNumber = call.CustomerNumber, Text = call.CustomerNumber.ToString() + " -Bu nömrə üçün sorğu əlavə edilmədi 4 saat sonra yene yoxla" });
                        CustomerRequestLogic.Dispose();
                    }
                }
                else
                {
                    ListResponsData.ErrorList.Add(new Error() { Code = "404", Type = OperationResultCode.Error, CustomerNumber = call.CustomerNumber, Text = call.CustomerNumber.ToString() + " -Nömrəsi düzgün qeyd edilməyib bu nömrə heç bir regiona aid deyil.Nömrə Tapılnmadı" });
                }
            }
            ObjectResult result = Result(ListResponsData);
            _regionLogic.Dispose();
            return result;

        }

    }



}