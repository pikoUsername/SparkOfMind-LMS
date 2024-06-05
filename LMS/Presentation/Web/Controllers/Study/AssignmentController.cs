using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Presentation.Web.Controllers.Study
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AssignmentController : ControllerBase
    {
    }
}
