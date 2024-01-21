using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.LawProcess;
using System.Xml;



using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HtmlAgilityPack;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class LawProcessController : BaseController
    {
        #region Properties

        private readonly LawProcessLogic _LawProcessLogic;

        #endregion

        #region Constructor

        public LawProcessController(IHttpContextAccessor accessor)
        {
            _LawProcessLogic = new LawProcessLogic(accessor.HttpContext.User);
        }

        #endregion

        #region LawProcess Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_LawProcessLogic.GetAll(filter));
            _LawProcessLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_LawProcessLogic.GetById(id));
            _LawProcessLogic.Dispose();
            return result;
        }

        #endregion

        #region LawProcess Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]LawProcessFindModel parameter)
        {
            var result = Result(_LawProcessLogic.Find(parameter));
            _LawProcessLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]LawProcessFindModel parameter)
        {
            var result = Result(_LawProcessLogic.FindAll(parameter));
            _LawProcessLogic.Dispose();
            return result;
        }

        #endregion

        #region LawProcess Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]LawProcessCreateModel entity)
        {
            var result = Result(_LawProcessLogic.Add(entity));
            _LawProcessLogic.Dispose();
            return result;
        }

        #endregion

        #region LawProcess Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]LawProcessUpdateModel entity)
        {
            var result = Result(_LawProcessLogic.Update(entity));
            _LawProcessLogic.Dispose();
            return result;
        }

        #endregion

        #region LawProcess Remove



        #endregion


        [HttpGet("getCourt")]
        public IActionResult Court()
        {
            List<object> result = new List<object>();
            var html = @"https://e-mehkeme.gov.az/Public/CaseSearch?";
            HtmlWeb web = new HtmlWeb();

            try
            {
                var htmlDoc = web.Load(html);

                var node = htmlDoc.DocumentNode.SelectNodes("//select[@id='CourtId']/option");
                foreach (var el in node)
                {
                    if (el.Attributes["value"].Value != string.Empty)
                        result.Add(new { id = el.Attributes["value"].Value, name = el.InnerText.Replace("&#246;", "ö").Replace("&#231;", "ç") });
                }
            }
            catch
            {

            }
            return Ok(new { output = result });
        }

        [HttpGet("getJudge")]
        public IActionResult Judge([FromUri] int id)
        {
            List<object> result = new List<object>();
            var html = @"https://e-mehkeme.gov.az/Public/CaseSearch?CourtId="+id;
            HtmlWeb web = new HtmlWeb();      
            try
            {
                var htmlDoc = web.Load(html);

                var node = htmlDoc.DocumentNode.SelectNodes("//select[@id='JudgeIds']/option");
                foreach (var el in node)
                {
                    if (el.Attributes["value"].Value != string.Empty)
                        result.Add(new { id = el.Attributes["value"].Value, name = el.InnerText.Replace("&#246;", "ö").Replace("&#231;", "ç").Replace("&#252;","ü") });
                }
            }
            catch
            {

            }
            return Ok(new { output = result });
        }
    }
}
