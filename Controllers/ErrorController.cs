using System;
using System.Threading.Tasks;
using EventApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers {
    [ApiController]
    [Route("[controller]/[action]")]
    public class ErrorController : ControllerBase
    {

        public ErrorController()
        {
            
        }

        [AllowAnonymous]
        public IActionResult Error(){
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return BadRequest(exceptionHandlerPathFeature?.Error.Message);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Exception()
        {
            throw new ApplicationException("Application error!");
        }
    }

}