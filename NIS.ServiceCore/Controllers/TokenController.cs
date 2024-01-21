using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NIS.BLCore.Models.Core;
using NIS.Security;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _configuration;

        public TokenController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        [HttpPost]
        public JWTResult Post([FromForm]UserLoginData data)
        {
            JWTToken tokenGenerator = new JWTToken(_configuration,User);
            return tokenGenerator.SingIn(data);
        }
    }
}