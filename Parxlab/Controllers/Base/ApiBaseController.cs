using Microsoft.AspNetCore.Mvc;

namespace Parxlab.Controllers.Base
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]

    public class ApiBaseController : ControllerBase
    {
      
    }
}