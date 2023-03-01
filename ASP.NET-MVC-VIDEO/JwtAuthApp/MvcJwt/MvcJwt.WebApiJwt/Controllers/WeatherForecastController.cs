using Microsoft.AspNetCore.Mvc;

namespace MvcJwt.WebApiJwt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        //[HttpGet(Name = "GetToken")]
        public string Get(string UserName, string Password)
        {
            if (UserName == "admin" && Password == "admin")
            {
                string token = TokenManager.GenerateToken(UserName);
                return token;
            }
            return "not ok";
        }


    }
}