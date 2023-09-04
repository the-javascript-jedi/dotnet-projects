using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")] // /api/users - (UsersController)
// ControllerBase is a base class which provides certain api functionality
public class BaseApiController : ControllerBase
{

}
