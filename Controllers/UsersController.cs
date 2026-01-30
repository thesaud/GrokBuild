using Microsoft.AspNetCore.Mvc;

namespace Grok30.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = new[]
            {
                new { Id = 1, Name = "Saud" },
                new { Id = 2, Name = "Ali" }
            };

            return Ok(users);
        }
    }
}
