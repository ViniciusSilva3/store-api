using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[ApiController]
[Route("catalog")]
public class HealthcheckController : ControllerBase
{
    [HttpGet]
    [Route("healthcheck")]
    public ActionResult Healthcheck()
    {
        return Ok();
    }
 
}
