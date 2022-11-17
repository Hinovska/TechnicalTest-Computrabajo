using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Redarbor.WebApp.Authentication;
using System.Net;
using sfEntities = Redarbor.SystemFramework.Entities;

namespace Redarbor.WebApp.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger<HealthCheckController> _logger;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public HealthCheckController(IJwtAuthenticationManager jwtAuthenticationManager, ILogger<HealthCheckController> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET: api/v1/<HealthCheckController>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            sfEntities.OperationAPI<string[]> objResponse = new sfEntities.OperationAPI<string[]>();
            objResponse.Data = new string[] { "ApiStatus", "OK" };
            objResponse.Message = "Online";
            objResponse.StatusCode = HttpStatusCode.OK.ToString();
            return Ok(objResponse);
        }

        // GET: api/v1/<HealthCheckController>
        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] APIAuthUser appCredentials)
        {
            if (appCredentials == null || appCredentials == new APIAuthUser())
                return BadRequest();

            var token = _jwtAuthenticationManager.Authenticate(appCredentials.userName, appCredentials.password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
