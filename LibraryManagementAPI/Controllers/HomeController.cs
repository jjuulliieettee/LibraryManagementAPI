using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.WebApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "Hello world! Library Manager is up and running ;)" });
        }
    }
}
