using System.Threading.Tasks;
using EventApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LogController : ControllerBase
    {
        private readonly IApiLogService _apiLogService;

        public LogController(IApiLogService apiLogService)
        {
            _apiLogService = apiLogService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetRequestLogEntries()
        {
            var entries = _apiLogService.GetEntries();
            return Ok(entries);
        }
    }

}