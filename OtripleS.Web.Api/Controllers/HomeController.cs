using Microsoft.AspNetCore.Mvc;

namespace OtripleS.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public ActionResult<string> Get() =>
            Ok("Hello");
    }
}
