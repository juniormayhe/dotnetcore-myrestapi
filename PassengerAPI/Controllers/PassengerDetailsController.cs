using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using static System.Diagnostics.Trace;
namespace PassengerAPI.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class PassengerDetailsController : ControllerBase
    {
        private IHttpContextAccessor _accessor;

        public PassengerDetailsController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "identityserver")] string ipIdentiyServer)
        {
            WriteLine($"- ipIdentityServer: {ipIdentiyServer}");
            WriteLine($"- {_accessor.HttpContext.Connection.RemoteIpAddress.ToString()}:{_accessor.HttpContext.Connection.RemotePort.ToString()} -> {_accessor.HttpContext.Connection.LocalIpAddress.ToString()}:{_accessor.HttpContext.Connection.LocalPort.ToString()}");
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
