using Microsoft.AspNetCore.Mvc;

namespace API.Gateway.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from Home Controller !!");
    }
}
