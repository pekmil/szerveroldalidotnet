using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.Models.Communication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EventApp.Services {

    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole<int>> roleManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public async Task InitAsync()
        {
            var role = await _roleManager.FindByNameAsync("Administrator");
            if(role == null){
                var result = await _roleManager.CreateAsync(new IdentityRole<int>("Administrator"));
                if(result.Succeeded){
                    ApplicationUser admin = new ApplicationUser(){
                        Email = "admin@mik.uni-pannon.hu",
                        Enabled = true,
                        UserName = "admin",
                        EmailConfirmed = true,
                        DoB = new DateTime(1988, 5, 19)
                    };
                    result = await _userManager.CreateAsync(admin, "Admin_123");
                    if(result.Succeeded){
                        await _userManager.AddToRoleAsync(admin, "Administrator");
                    }
                }
            }

            role = await _roleManager.FindByNameAsync("User");
            if(role == null){
                var result = await _roleManager.CreateAsync(new IdentityRole<int>("User"));
                if(result.Succeeded){
                    ApplicationUser user = new ApplicationUser(){
                        Email = "user@mik.uni-pannon.hu",
                        Enabled = true,
                        UserName = "user",
                        EmailConfirmed = true,
                        DoB = new DateTime(2005, 10, 17)
                    };
                    result = await _userManager.CreateAsync(user, "User_123");
                    if(result.Succeeded){
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                }
            }

            var adminUser = await _userManager.FindByNameAsync("admin");
            if(adminUser != null){
                await _userManager.AddToRoleAsync(adminUser, "User");
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest data)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == data.UserName);
            if (user != null)
            {
                if (user.Enabled)
                {
                    var result = await _signInManager.PasswordSignInAsync(data.UserName, data.Password, false, true);
                    if (result.Succeeded)
                    {
                        var token = await GenerateJwtToken(user);
                        return new LoginResponse(){ Token = token };
                    }
                    else
                    {
                        throw new ApplicationException("LOGIN_FAILED");
                    }
                }
                else
                {
                    throw new ApplicationException("USER_DISABLED");
                }
            }
            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");

        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey_123456"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(30));

            var id = await GetClaimsIdentity(user);
            var token = new JwtSecurityToken("https://mik.uni-pannon.hu", "https://mik.uni-pannon.hu", id.Claims, expires: expires, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.DoB.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString(CultureInfo.InvariantCulture))
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
           
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            return claimsIdentity;
        }
    }

}