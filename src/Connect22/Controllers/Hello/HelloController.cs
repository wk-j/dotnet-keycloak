using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Connect.Controllers.Hello {
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class HelloController : ControllerBase {

        [HttpGet]
        public string Hello() => "Hello";

    }
}