using Microsoft.AspNetCore.Mvc;

namespace RestfulWebApi.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
