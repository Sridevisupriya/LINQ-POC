using LearnStudentAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace LearnStudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<string> RegisterUser(RegisterUser user , string role)
        {
            var userExists = await _userManager.FindByEmailAsync(user.Email);
            if(userExists != null)
            {
                return "User Already Exists";
            }
            else
            {
                IdentityUser identityUser = new IdentityUser()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                if(await _roleManager.RoleExistsAsync(role))
                {
                    var createUser = await _userManager.CreateAsync(identityUser, user.Password);
                    if (createUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(identityUser, role);
                        return "User Created Success fully";
                    }
                    else
                    {
                        return "User not created";
                    }
                }
                else
                {
                    return "Role does not exist cannot create user";
                }
               
            }
        }
    }
}
