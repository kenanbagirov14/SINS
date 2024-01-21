using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NIS.BLCore.Logics;
using NIS.DALCore.Context;
using Microsoft.AspNetCore.Authorization;

namespace NIS.ServiceCore.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly NISContext _context;
        public ValuesController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        // GET api/values
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var cl = new InjuryTypeLogic(User);
            var it = cl.GetAll(new BLCore.Models.Core.Filter());
           // var dict = new Dictionary<string, string>();
           // HttpContext.User.Claims.ToList()
           //.ForEach(item => dict.Add(item.Type, item.Value));
           // var arealogic = new  InjuryTypeLogic();
           // var at = arealogic.GetAll(new BLCore.Models.Core.Filter()).Output;
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var at = User;
            return "value";
        }

        // POST api/values
        [HttpPost("FindAll")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
