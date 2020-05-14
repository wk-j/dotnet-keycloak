using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Connect31.Controllers {
    [Route("[controller]")]
    // [Authorize(AuthenticationSchemes = "Bearer")]
    // [Authorize]
    [Authorize(AuthenticationSchemes = "OpenIdConnect")]
    [ApiController]
    public class HelloController : ControllerBase {
        [HttpGet]
        public string Get() {
            return "Hello, world!";
        }
    }
}
