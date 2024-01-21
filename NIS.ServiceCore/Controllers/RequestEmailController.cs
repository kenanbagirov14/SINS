using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.RequestEmail;
using NIS.BLCore.Models.Core;
using NIS.ServiceCore.MailOperations;
using NIS.BLCore;
using NIS.UtilsCore;
using NIS.UtilsCore.Enums;
using Microsoft.AspNetCore.Mvc;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{

    public class RequestEmailController : BaseController
    {
        #region Properties

        private readonly RequestEmailLogic _RequestEmailLogic;

        #endregion

        #region Constructor

        public RequestEmailController(IHttpContextAccessor accessor)
        {
            _RequestEmailLogic = new RequestEmailLogic(accessor.HttpContext.User);
        }

        #endregion

        #region RequestEmail Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            return Result(_RequestEmailLogic.GetAll(filter));
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Result(_RequestEmailLogic.GetById(id));
        }

        #endregion

        //#region RequestEmail Find

        //[HttpPost]
        //public IActionResult Find([FromBody]RequestEmailFindModel parameter)
        //{
        //    return Result(_RequestEmailLogic.Find(parameter));
        //}

        //[HttpPost]
        //public async Task<RequestEmailViewModel> FindAsync(RequestEmailFindModel parameter)
        //{
        //    return await _RequestEmailLogic.FindAsync(parameter);
        //}

        //[HttpPost]
        //public IActionResult FindAll([FromBody]RequestEmailFindModel parameter)
        //{
        //    return Result(_RequestEmailLogic.FindAll(parameter));
        //}

        //[HttpPost]
        //public async Task<ICollection<RequestEmailViewModel>> FindAllAsync(RequestEmailFindModel parameter)
        //{
        //    return await _RequestEmailLogic.FindAllAsync(parameter);
        //}

        //#endregion

        #region RequestEmail Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]RequestEmailCreateModel entity)
        {
           // Notify notify = new Notify(User);
            //if (MailSender.Send(entity.Email, entity.Subject, entity.Message, "software.support@aztelekom.az", "software.support@aztelekom.az"))
            //if (notify.SendMessage(entity.UniqueId, entity.Email, entity.Subject, entity.Message, "software.support@aztelekom.az", "software.support@aztelekom.az"))
            //    return Result(_RequestEmailLogic.Add(entity));

            var at = new LogicResult<RequestEmailViewModel>
            {
                ErrorList = new List<Error>()
            };
            at.ErrorList.Add(new Error
            {
                Code = "email send error",
                Text = "Email Gonderilmedi",
                Type = OperationResultCode.Exception
            });
            return Result(at);
        }

        #endregion

        //#region RequestEmail Update

        //[HttpPost]
        //public IActionResult Update([FromBody]RequestEmailUpdateModel entity)
        //{
        //    return Result(_RequestEmailLogic.Update(entity));
        //}

        //[HttpPost]
        //public async Task<RequestEmailViewModel> UpdateAsync(RequestEmailUpdateModel entity)
        //{
        //    return await _RequestEmailLogic.UpdateAsync(entity);
        //}
        //#endregion

        //#region RequestEmail Remove



        //#endregion
    }
}
