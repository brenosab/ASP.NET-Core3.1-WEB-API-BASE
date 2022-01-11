using Microsoft.AspNetCore.Mvc;
using System;

namespace APIorm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Application = Environment.GetEnvironmentVariable("APPLICATION"),
                Version = Environment.GetEnvironmentVariable("VERSION"),
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            });
        }
    }
}