using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EventApp.Controllers {

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CacheController : ControllerBase
    {

        const string usersCacheKey = "USERS";

        private readonly IMemoryCache _cache;
        private readonly IUserService _userService;

        public CacheController(IMemoryCache cache, IUserService userService)
        {
            _cache = cache;
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult GetUsers(){
            if(!_cache.TryGetValue(usersCacheKey, out var users)){
                users = _userService.GetUsers().ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _cache.Set(usersCacheKey, users, cacheEntryOptions);
            }
            return Ok(users as List<ApplicationUser>);
        }

        
    }

}