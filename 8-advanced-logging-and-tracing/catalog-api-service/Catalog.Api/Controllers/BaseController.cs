﻿using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        //protected const int DefaultPageNumber = 1;
        //protected const int DefaultPageSize = 5;
    }
}
