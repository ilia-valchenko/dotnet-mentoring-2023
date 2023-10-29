using Microsoft.AspNetCore.Mvc;

namespace RestfulWebApi.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
